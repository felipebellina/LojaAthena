using LojaAthena.Areas.Admin.Services;
using LojaAthena.Data;
using LojaAthena.Models;
using LojaAthena.Repositories;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connString = builder.Configuration.GetConnectionString("DataBase");
builder.Services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(connString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BancoContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddTransient<IRoupaRepository, RoupaRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); 
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<RelatorioVendasService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", politica =>
    {
        politica.RequireRole("Admin");
    });
});

builder.Services.AddScoped(sp => CarrinhoCompraModel.GetCarrinho(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<ConfigurationImagensModel>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

CriarPerfilUsuario(app);

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Roupa/{action}/{categoria?}",
    defaults: new { Controller = "Roupa", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void CriarPerfilUsuario(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedRoles();
        service.SeedUsers();
    }
}