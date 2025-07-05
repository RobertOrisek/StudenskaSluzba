using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using StudentskaSlužba.src.sqlutills;

namespace StudentskaSlužba.src.model
{
    public class Predaje
    {
        private static int? BrojacPredaje = -1;
        public int? Id { get; set; }

        public int? NastavnikId { get; set; }
        public int? PredmetId { get; set; }

        #region Konstruktori
        public Predaje()
        {
            BrojacPredaje++;
            this.Id = BrojacPredaje;

            this.NastavnikId = -1;
            this.PredmetId = -1;
        }

        public Predaje(int NastavnikId, int PredmetId, int Id = -1)
        {
            if(Id != -1 && Id > BrojacPredaje && Id > 0)
            {
                BrojacPredaje = Id;
                this.Id = BrojacPredaje;
            }
            else
            {
                BrojacPredaje++;
                this.Id = BrojacPredaje;
            }

            this.NastavnikId = NastavnikId;
            this.PredmetId = PredmetId;
        }
        #endregion

        ~Predaje()
        {
            if (BrojacPredaje > -1)
            {
                BrojacPredaje--;
            }
            else
            {
                BrojacPredaje = null;
            }
            this.Id = null;

            this.NastavnikId = null;
            this.PredmetId = null;
        }
        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Predaje: [{this.Id}] |";
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.NastavnikId.GetHashCode() ^ this.PredmetId.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if(obj is not Pohađa || obj is null)
                return false;
            return ((Pohađa)obj).GetHashCode() == this.GetHashCode();
        }
        #endregion
        #region Implementirane Metode
        public static List<Predaje> UcitajPredaje()
        {
            List<Predaje> ListaPredaje = new();
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = SqlUtills.SqlLoadTable("predaje", c).ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int nastavnik_id = (int)rdr["nastavnik_id"];
                    int predmet_id = (int)rdr["predmet_id"];

                    ListaPredaje.Add(new(nastavnik_id, predmet_id));
                }
            }
            else
            {
                Console.WriteLine("Greška\nPutanja za 'predaje' nije validna.");
            }
            return ListaPredaje;
        }

        public static void IspisSvhiPredaje(List<Predaje> ListaPredaje, List<Nastavnik> ListaNastavnika, List<Predmet> ListaPredmeta)
        {
            if(ListaPredaje == null || ListaPredaje.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema 'predaje' entiteta u sistemu.");
            }
            else if(ListaNastavnika == null || ListaNastavnika.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema 'nastavnik' entiteta u sistemu.");
            }
            else if(ListaPredmeta == null || ListaPredmeta.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema 'predmet' entiteta u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svih entiteta 'predaje'. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Predaje pr in ListaPredaje)
                {
                    Console.WriteLine("\n\n" + pr.ToString());
                    foreach(Nastavnik n in ListaNastavnika)
                    {
                        if(pr.NastavnikId == n.Id)
                            Console.WriteLine('\n' + n.ToString());
                    }
                    foreach(Predmet p in ListaPredmeta)
                    {
                        if(pr.PredmetId == p.Id)
                            Console.WriteLine('\n' + p.ToString());
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisPredajePoIdentifikatoru(List<Predaje> ListaPredaje, int Id)
        {
            if (ListaPredaje == null || ListaPredaje.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema 'predaje' entiteta u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis svih entiteta 'predaje' po identifikatoru [{Id}]. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Predaje pr in ListaPredaje)
                {
                    if(pr.Id == Id)
                    {
                        Console.WriteLine(pr.ToString());
                    }
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }
        #endregion
        #endregion
    }
}
