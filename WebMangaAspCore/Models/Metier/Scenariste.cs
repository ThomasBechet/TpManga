using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using WebmangaAspCore.Models.Dao;

namespace WebmangaAspCore.Models.Metier
{
    public class Scenariste {



        private int id_scenariste;
        private String nom_scenariste;
        private String  prenom_scenariste ;

        public Scenariste(int id_scenariste, string nom_scenariste, string prenom_scenariste)
        {
            this.id_scenariste = id_scenariste;
            this.nom_scenariste = nom_scenariste;
            this.prenom_scenariste = prenom_scenariste;
        }

        public int Id_scenariste { get => id_scenariste; set => id_scenariste = value; }
        public string Nom_scenariste { get => nom_scenariste; set => nom_scenariste = value; }
        public string Prenom_scenariste { get => prenom_scenariste; set => prenom_scenariste = value; }

        public static System.Collections.Generic.List<SelectListItem> GetList()
        {
            System.Collections.Generic.List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (DataRow row in ServiceUtilisateur.GetAllScenaristes())
            {
                listItems.Add(new SelectListItem
                {
                    Text = row[1] + " " + row[2],
                    Value = row[0].ToString()
                });
            }

            return listItems;
        }
    }
}