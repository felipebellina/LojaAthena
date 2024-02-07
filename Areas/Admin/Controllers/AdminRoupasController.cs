using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaAthena.Data;
using LojaAthena.Models;
using Microsoft.AspNetCore.Authorization;

namespace LojaAthena.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminRoupasController : Controller
{
    private readonly BancoContext _context;

    public AdminRoupasController(BancoContext context)
    {
        _context = context;
    }

    // GET: Admin/AdminRoupas
    public async Task<IActionResult> Index()
    {
        var bancoContext = _context.Roupas.Include(r => r.Categoria);
        return View(await bancoContext.ToListAsync());
    }

    // GET: Admin/AdminRoupas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var roupaModel = await _context.Roupas
            .Include(r => r.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (roupaModel == null)
        {
            return NotFound();
        }

        return View(roupaModel);
    }

    // GET: Admin/AdminRoupas/Create
    public IActionResult Create()
    {
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao");
        return View();
    }

    // POST: Admin/AdminRoupas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,ImagemUrl,ImagemThumbnailUrl,RoupaPreferida,Estoque,CategoriaId")] RoupaModel roupaModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(roupaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", roupaModel.CategoriaId);
        return View(roupaModel);
    }

    // GET: Admin/AdminRoupas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var roupaModel = await _context.Roupas.FindAsync(id);
        if (roupaModel == null)
        {
            return NotFound();
        }
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", roupaModel.CategoriaId);
        return View(roupaModel);
    }

    // POST: Admin/AdminRoupas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,ImagemUrl,ImagemThumbnailUrl,RoupaPreferida,Estoque,CategoriaId")] RoupaModel roupaModel)
    {
        if (id != roupaModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(roupaModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoupaModelExists(roupaModel.Id))
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
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", roupaModel.CategoriaId);
        return View(roupaModel);
    }

    // GET: Admin/AdminRoupas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var roupaModel = await _context.Roupas
            .Include(r => r.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (roupaModel == null)
        {
            return NotFound();
        }

        return View(roupaModel);
    }

    // POST: Admin/AdminRoupas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var roupaModel = await _context.Roupas.FindAsync(id);
        if (roupaModel != null)
        {
            _context.Roupas.Remove(roupaModel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RoupaModelExists(int id)
    {
        return _context.Roupas.Any(e => e.Id == id);
    }
}
