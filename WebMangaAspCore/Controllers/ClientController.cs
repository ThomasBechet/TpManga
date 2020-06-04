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

                ServiceManga.AddManga(unM);
                return RedirectToAction("Index", "Manga");
            }
            catch (MonException e)
            {
                return StatusCode(418);
            }
        }

    }
}
