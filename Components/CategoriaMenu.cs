﻿using LojaAthena.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaAthena.Components;

public class CategoriaMenu : ViewComponent
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaMenu(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public IViewComponentResult Invoke()
    {
        var categorias = _categoriaRepository.Categorias.OrderBy(c => c.Nome);
        return View(categorias);
    }
}