using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebmangaAspCore.Models.MesExceptions;
using WebmangaAspCore.Models.Metier;
using WebmangaAspCore.Models.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMangaAspCore.Controllers
{
    public class ClientController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ajouter(Manga unM)
        {

            try
            {
                unM.Id_dessinateur = int.Parse(Request.Form["Id_dessinateur"].ToString());
                unM.Id_scenariste = int.Parse(Request.Form["Id_scenariste"].ToString());
                unM.Id_genre = int.Parse(Request.Form["Id_genre"].ToString());
                unM.Couverture = Request.Form["Couverture"].ToString();

                ServiceManga.AddManga(unM);
                return RedirectToAction("Index", "Manga");
            }
            catch (MonException e)
            {
                return StatusCode(418);
            }
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.Combine(System.IO.Path.GetFullPath("./wwwroot/images"), formFile.FileName); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("Index", "Client");
        }

    }
}
