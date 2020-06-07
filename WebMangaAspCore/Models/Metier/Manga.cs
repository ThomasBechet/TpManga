using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using WebmangaAspCore.Models.Dao;

namespace WebmangaAspCore.Models.Metier
{
    public class Manga
    {
        private int id_manga;
        private int id_dessinateur;
        private int id_scenariste;
        private Double prix;
        private String titre;
        private DateTime dateParution;
        private String couverture;
        private int id_genre;

        public static System.Collections.Generic.List<SelectListItem> GetCouvertures()
        {
            System.Collections.Generic.List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (string imageFileName in System.IO.Directory.GetFiles(System.IO.Path.GetFullPath("./wwwroot/images")))
            {
                if (System.IO.Path.GetExtension(imageFileName).Contains("jpg"))
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = System.IO.Path.GetFileName(imageFileName),
                        Value = System.IO.Path.GetFileName(imageFileName)
                    });
                }
            }

            return listItems;
        }

        public int Id_manga { get => id_manga; set => id_manga = value; }
        public int Id_dessinateur { get => id_dessinateur; set => id_dessinateur = value; }
        public int Id_scenariste { get => id_scenariste; set => id_scenariste = value; }
        public double Prix
        {
            get
            {
                return prix;
            }
            set
            {
                prix = Convert.ToDouble(value.ToString().Replace('.', ','));
            }
        }
        public string Titre { get => titre; set => titre = value; }
        public string Couverture { get => couverture; set => couverture = value; }
        public int Id_genre { get => id_genre; set => id_genre = value; }
        public DateTime DateParution { get => dateParution; set => dateParution = value; }
    }
}
