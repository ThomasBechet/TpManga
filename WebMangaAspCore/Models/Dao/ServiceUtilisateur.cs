using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebmangaAspCore.Models.MesExceptions;
using WebmangaAspCore.Models.Metier;
using WebmangaAspCore.Models.Persistance;

namespace WebmangaAspCore.Models.Dao
{
    public class ServiceUtilisateur
    {

        public Utilisateur getUtilistateur(String nom)
        {
            DataTable dt;
            Utilisateur unUti = null;
            String mysql = "SELECT numUtil,nomUtil, MotPasse, salt, role  FROM utilisateur  " +
            " where nomUtil = " + "'" + nom + "'";
            Serreurs er = new Serreurs("Erreur sur recherche d'un utilisateur.", "Service.getUtilisateur");
            try
            {

                dt = DBInterface.Lecture(mysql, er);
                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    unUti = new Utilisateur();
                    // il faut redecouper la liste pour retrouver les lignes
                    DataRow dataRow = dt.Rows[0];
                    unUti.NumUtil = Int16.Parse(dataRow[0].ToString());
                    unUti.NomUtil = dataRow[1].ToString();
                    unUti.MotPasse = dataRow[2].ToString();
                    unUti.Salt = dataRow[3].ToString();
                    unUti.Role = dataRow[4].ToString();
                }
                return unUti;
            }
            catch (MonException e)
            {
                throw e;
            }
            catch (Exception exc)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), exc.Message);
            }
        }

        public static DataRowCollection GetAllDessinateurs()
        {
            DataTable dt;
            String mysql = "SELECT id_dessinateur, nom_dessinateur, prenom_dessinateur FROM dessinateur";
            Serreurs er = new Serreurs("Erreur sur recherche d'un dessinateur.", "Service.getUtilisateur");
            dt = DBInterface.Lecture(mysql, er);

            return dt.Rows;
        }

        public static DataRowCollection GetAllScenaristes()
        {
            DataTable dt;
            String mysql = "SELECT * FROM scenariste";
            Serreurs er = new Serreurs("Erreur sur recherche d'un scenariste.", "Service.getUtilisateur");
            dt = DBInterface.Lecture(mysql, er);

            return dt.Rows;
        }

        public static DataRowCollection GetAllGenres()
        {
            DataTable dt;
            String mysql = "SELECT * FROM genre";
            Serreurs er = new Serreurs("Erreur sur recherche d'un genre.", "Service.getUtilisateur");
            dt = DBInterface.Lecture(mysql, er);

            return dt.Rows;
        }
    }
}