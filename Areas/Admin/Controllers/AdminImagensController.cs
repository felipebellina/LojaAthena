using LojaAthena.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LojaAthena.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminImagensController : Controller
{
    private readonly ConfigurationImagensModel? _myConfig;
    private readonly IWebHostEnvironment? _environment;

    public AdminImagensController(IOptions<ConfigurationImagensModel> myConfiguration, IWebHostEnvironment? environment)
    {
        _myConfig = myConfiguration.Value;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if(files == null || files.Count == 0)
        {
            ViewData["Error"] = "Error: Arquivo(s) não selecionado(s)";
            return View(ViewData);
        }

        //if(files.Count > 10)
        //{
        //    ViewData["Error"] = "Error: Quantidade de arquivos excedeu o limite";
        //    return View(ViewData);
        //}

        long size = files.Sum(f => f.Length);

        var filePathsName = new List<string>();

        var filePath = Path.Combine(_environment.WebRootPath, _myConfig.NomePastaImagensProdutos);

        foreach(var formFile in files)
        {
            if (formFile.FileName.Contains(".jpeg") || formFile.FileName.Contains(".gif") || formFile.FileName.Contains(".png") || formFile.FileName.Contains(".jpg"))
            {
                var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                filePathsName.Add(fileNameWithPath);

                using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, com tamanho total de: {size} bytes";

        ViewBag.Arquivos = filePathsName;

        return View(ViewData);

    }
 

    public IActionResult GetImagens()
    {
        FileManagerModel model = new FileManagerModel();

        var userImagesPath = Path.Combine(_environment.WebRootPath, _myConfig.NomePastaImagensProdutos);

        DirectoryInfo dir = new DirectoryInfo(userImagesPath);

        FileInfo[] files = dir.GetFiles();

        model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

        if (files.Length == 0)
        {
            ViewData["Error"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
        }


        model.Files = files;

        return View(model);

    }

    public IActionResult DeleteFiles(string fname)
    {
        string imagemDeleta = Path.Combine(_environment.WebRootPath, _myConfig.NomePastaImagensProdutos + "\\", fname);

        if (System.IO.File.Exists(imagemDeleta))
        {
            System.IO.File.Delete(imagemDeleta);

            ViewData["Deletado"] = $"Arquivo(s) {imagemDeleta} deletado com sucesso";
        }
        return View("index");

    }

}
