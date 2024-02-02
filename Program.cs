using LojaAthena.Data;
using LojaAthena.Models;
using LojaAthena.Repositories;
using LojaAthena.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IRoupaRepository, RoupaRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); 
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped(sp => CarrinhoCompraModel.GetCarrinho(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connString = builder.Configuration.GetConnectionString("DataBase");
builder.Services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(connString));

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Roupa/{action}/{categoria?}",
    defaults: new { Controller = "Roupa", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
