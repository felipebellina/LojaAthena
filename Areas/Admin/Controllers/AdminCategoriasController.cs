using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaAthena.Data;
using LojaAthena.Models;
using Microsoft.AspNetCore.Authorization;

namespace LojaAthena.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminCategoriasController : Controller
{
    private readonly BancoContext _context;

    public AdminCategoriasController(BancoContext context)
    {
        _context = context;
    }

    // GET: Admin/AdminCategorias
    public async Task<IActionResult> Index()
    {
        return View(await _context.Categorias.ToListAsync());
    }

    // GET: Admin/AdminCategorias/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoriaModel = await _context.Categorias
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoriaModel == null)
        {
            return NotFound();
        }

        return View(categoriaModel);
    }

    // GET: Admin/AdminCategorias/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/AdminCategorias/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaModel categoriaModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(categoriaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoriaModel);
    }

    // GET: Admin/AdminCategorias/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoriaModel = await _context.Categorias.FindAsync(id);
        if (categoriaModel == null)
        {
            return NotFound();
        }
        return View(categoriaModel);
    }

    // POST: Admin/AdminCategorias/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] CategoriaModel categoriaModel)
    {
        if (id != categoriaModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(categoriaModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaModelExists(categoriaModel.Id))
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
        return View(categoriaModel);
    }

    // GET: Admin/AdminCategorias/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoriaModel = await _context.Categorias
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoriaModel == null)
        {
            return NotFound();
        }

        return View(categoriaModel);
    }

    // POST: Admin/AdminCategorias/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoriaModel = await _context.Categorias.FindAsync(id);
        if (categoriaModel != null)
        {
            _context.Categorias.Remove(categoriaModel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoriaModelExists(int id)
    {
        return _context.Categorias.Any(e => e.Id == id);
    }
}
