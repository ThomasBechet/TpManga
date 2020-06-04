using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using WebmangaAspCore.Models.Dao;

namespace WebmangaAspCore.Models.Metier
{
    public class Genre {

        private int id_genre;
        private String lib_genre;

        public Genre(int id_genre, string lib_genre)
        {
            this.id_genre = id_genre;
            this.lib_genre = lib_genre;
        }

        public int Id_genre { get => id_genre; set => id_genre = value; }
        public string Lib_genre { get => lib_genre; set => lib_genre = value; }

        public static System.Collections.Generic.List<SelectListItem> GetList()
        {
            System.Collections.Generic.List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (DataRow row in ServiceUtilisateur.GetAllGenres())
            {
                listItems.Add(new SelectListItem
                {
                    Text = row[1].ToString(),
                    Value = row[0].ToString()
                });
            }

            return listItems;
        }
    }

}