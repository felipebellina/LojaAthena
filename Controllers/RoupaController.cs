using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Controllers;
public class RoupaController : Controller
{
    private readonly IRoupaRepository _roupaRepository;

    public RoupaController(IRoupaRepository repository)
    {
        _roupaRepository = repository;
    }

    public IActionResult List(string categoria)
    {
        IEnumerable<RoupaModel> roupas;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            roupas = _roupaRepository.Roupas.OrderBy(x => x.Id);
            categoriaAtual = "Todas as roupas";
        }
        else
        {
            roupas = _roupaRepository.Roupas.Where(x => x.Categoria.Nome.Equals(categoria)).OrderBy(x => x.Nome);

            categoriaAtual = categoria;
        }
        var lanchesListViewModel = new RoupaListViewModel
        {
            Roupas = roupas,
            CategoriaAtual = categoriaAtual
        };

        return View(lanchesListViewModel);
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
            roupas = _roupaRepository.Roupas.OrderBy(x => x.Id);
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
            CategoriaAtual= categoriaAtual
        });
    }
}
