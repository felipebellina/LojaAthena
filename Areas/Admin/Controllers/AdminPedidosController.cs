using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaAthena.Data;
using LojaAthena.Models;
using Microsoft.AspNetCore.Authorization;
using LojaAthena.ViewsModels;

namespace LojaAthena.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminPedidosController : Controller
{
    private readonly BancoContext _context;

    public AdminPedidosController(BancoContext context)
    {
        _context = context;
    }

    public IActionResult PedidoRoupas(int? id)
    {
        var pedido = _context.Pedidos.Include(pd => pd.PedidoItens).ThenInclude(r => r.Roupa).FirstOrDefault(p => p.Id == id);

        if (pedido == null)
        {
            Response.StatusCode = 404;
            return View("PedidoNotFound", id.Value);
        }
        PedidoRoupaViewModel pedidoLanches = new PedidoRoupaViewModel()
        {
            Pedido = pedido,
            PedidoDetalhes = pedido.PedidoItens
        };
        return View(pedidoLanches);
    }


    // GET: Admin/AdminPedidos
    public async Task<IActionResult> Index()
    {
        return View(await _context.Pedidos.ToListAsync());
    }

    // GET: Admin/AdminPedidos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pedidoModel = await _context.Pedidos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pedidoModel == null)
        {
            return NotFound();
        }

        return View(pedidoModel);
    }

    // GET: Admin/AdminPedidos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/AdminPedidos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] PedidoModel pedidoModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pedidoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pedidoModel);
    }

    // GET: Admin/AdminPedidos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pedidoModel = await _context.Pedidos.FindAsync(id);
        if (pedidoModel == null)
        {
            return NotFound();
        }
        return View(pedidoModel);
    }

    // POST: Admin/AdminPedidos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] PedidoModel pedidoModel)
    {
        if (id != pedidoModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pedidoModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoModelExists(pedidoModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(pedidoModel);
    }

    // GET: Admin/AdminPedidos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pedidoModel = await _context.Pedidos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pedidoModel == null)
        {
            return NotFound();
        }

        return View(pedidoModel);
    }

    // POST: Admin/AdminPedidos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pedidoModel = await _context.Pedidos.FindAsync(id);
        if (pedidoModel != null)
        {
            _context.Pedidos.Remove(pedidoModel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PedidoModelExists(int id)
    {
        return _context.Pedidos.Any(e => e.Id == id);
    }
}
