﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using WebmangaAspCore.Models.MesExceptions;
using WebmangaAspCore.Models.Metier;
using WebmangaAspCore.Models.Dao;
using Microsoft.AspNetCore.Http;

namespace WebmangaAspCore.Controllers
{
    public class MangaController : Controller
    {
        // GET: Manga
        // GET: Client
        public ActionResult Index()
        {
            if (Utilisateur.IsConnected())
            {
                System.Data.DataTable mesMangas = null;

                try
                {

                    mesMangas = ServiceManga.GetTousLesManga();
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
                }

                return View(mesMangas);
            }
            else
            {
                return RedirectToAction("Index", "Connexion");
            }
        }

        // GET: Commande/Edit/5
        public ActionResult Modifier(string id)
        {
            Manga unManga = null;
            try
            {
                unManga = ServiceManga.GetunManga(id);
                ViewData["Role"] = HttpContext.Session.GetString("Role");

                return View(unManga);
            }
            catch (MonException e)
            {
                return StatusCode(418);
            }
        }

        [HttpPost]
        public ActionResult Modifier(Manga unM)
        {
            try
            {
                
                unM.Id_dessinateur = int.Parse(Request.Form["Id_dessinateur"].ToString());
                unM.Id_scenariste = int.Parse(Request.Form["Id_scenariste"].ToString());
                unM.Id_genre = int.Parse(Request.Form["Id_genre"].ToString());
                unM.Couverture = Request.Form["Couverture"].ToString();


                ServiceManga.UpdateManga(unM);

                return RedirectToAction("Index", "Manga");
            }
            catch (MonException e)
            {
                return StatusCode(418);
            }
        }

        public void Supprimer(String id)
        {
            try
            {
                ServiceManga.SupprimerManga(id);
                Response.Redirect("/Manga/Index");
                
            }
            catch (MonException e)
            {
                //return StatusCode(418);
            }
        }

    }
}