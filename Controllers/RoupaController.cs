using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;
public class RoupaController : Controller
{
    private readonly IRoupaRepository _roupaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public RoupaController(IRoupaRepository repository, ICategoriaRepository categoriaRepository)
    {
        _roupaRepository = repository;
        _categoriaRepository = categoriaRepository;
    }

    public IActionResult List(string categoria)
    {
        IEnumerable<RoupaModel> roupas;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            roupas = _roupaRepository.Roupas.OrderBy(x => x.Nome);
            categoriaAtual = "Todas as roupas";
        }
        else
        {
            roupas = _roupaRepository.Roupas.Where(x => x.Categoria.Nome.Equals(categoria)).OrderBy(x => x.Nome);

            categoriaAtual = categoria;
        }
        var roupasListViewModel = new RoupaListViewModel
        {
            Roupas = roupas,
            CategoriaAtual = categoriaAtual,
            Categorias = _categoriaRepository.Categorias
        };

        return View(roupasListViewModel);
    }

    public IActionResult Details(int id)
    {
        var roupa = _roupaRepository.Roupas.FirstOrDefault(x => x.Id == id);
        return View(roupa);
    }

    public IActionResult Search(string searchString)
    {
        IEnumerable<RoupaModel> roupas;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            roupas = _roupaRepository.Roupas.OrderBy(x => x.Nome);
            categoriaAtual = "Todas as Roupas";
        }
        else
        {
            roupas = _roupaRepository.Roupas.Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));

            if (roupas.Any())
            {
                categoriaAtual = "Roupas";
            }
            else
            {
                categoriaAtual = "Nenhuma roupa foi encontrada";
            }
        }
        
        return View("~/Views/Roupa/List.cshtml", new RoupaListViewModel
        {
            Roupas = roupas,
            CategoriaAtual= categoriaAtual,
            Categorias = _categoriaRepository.Categorias
        });
    }
}
