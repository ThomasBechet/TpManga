using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebmangaAspCore.Models.Utilitaires;
using WebmangaAspCore.Models.Dao;
using WebmangaAspCore.Models.Metier;
using WebmangaAspCore.Models.MesExceptions;
using Microsoft.AspNetCore.Http;


namespace WebmangaAspCore.Controllers
{
    public class ConnexionController : Controller
    {
        // GET: Connexion
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Controle()
        {
            try
            {
                // on récupère les données du formulaire
                string login = Request.Form["login"];
                string mdp = Request.Form["pwd"];
                try
                {
                    ServiceUtilisateur unService = new ServiceUtilisateur();
                    Utilisateur unUtilisateur = unService.getUtilistateur(login);
                    if (unUtilisateur != null)
                    {
                        try
                        {
                            Byte[] selmdp = MonMotPassHash.GenerateSalt();
                            Byte[] mdpByte = MonMotPassHash.PasswordHashe("secret", selmdp);
                            String mdpS = MonMotPassHash.BytesToString(mdpByte);
                            String saltS = MonMotPassHash.BytesToString(selmdp);
                            String sel = unUtilisateur.Salt;
                            // on récupère le sel 
                            Byte[] salt = MonMotPassHash.transformeEnBytes(unUtilisateur.Salt);
                            // on génère le mot de passe 
                            Byte[] tempo = MonMotPassHash.transformeEnBytes(unUtilisateur.MotPasse);
                            if (MonMotPassHash.VerifyPassword(salt, mdp, tempo) == false)
                            {
                                ModelState.AddModelError("Erreur", "Erreur lors du contrôle  du mot de passe pour : " + login);
                                return RedirectToAction("Index", "Connexion");
                            }
                            else
                            {
                                HttpContext.Session.SetString("Pseudo", unUtilisateur.NomUtil);
                                HttpContext.Session.SetString("Role", unUtilisateur.Role);
                                Utilisateur.SetConnected(true);
                                return RedirectToAction("Index","Home");
                            }
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("Erreur", "Erreur lors du contrôle : " + e.Message);
                            return RedirectToAction("Index", "Connexion");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Erreur", "Erreur  login erroné : " + login);
                        return RedirectToAction("Index", "Connexion");
                    }
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
                    return RedirectToAction("Index", "Connexion");
                }
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
                return RedirectToAction("Index", "Connexion");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            Utilisateur.SetConnected(false);
            return RedirectToAction("Index", "Connexion");
        }
    }
}