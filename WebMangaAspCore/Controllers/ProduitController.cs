using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebmangaAspCore.Models.Persistance;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMangaAspCore.Controllers
{
    public class ProduitController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AugmenterPrix()
        {

            string prix = Request.Form["prix"];
            float p = Int32.Parse(prix);
            prix = p.ToString();
            String param = "val;" + prix.Replace(',', '.');
            ArrayList allParams = new ArrayList();
            ViewBag.ValueAugm = Math.Round(p * 100, 1, MidpointRounding.ToEven).ToString();
            allParams.Add(param);

            DBInterface.Procedure_Stockee("augmenterPrix", allParams);

            return RedirectToAction("Index", "Manga"); ;
        }
    }
}
