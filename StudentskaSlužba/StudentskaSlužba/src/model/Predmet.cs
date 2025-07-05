using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;

namespace StudentskaSlužba.src.model
{
    public class Predmet
    {
        private static int? BrojacPredmeta = -1;
        public int? Id { get; set; }
        public string? Naziv { get; set; }

        public List<Student>? ListaStudnata { get; set; }

        #region Konstruktori
        public Predmet()
        {
            BrojacPredmeta++;
            this.Id = BrojacPredmeta;

            this.Naziv = null;

            this.ListaStudnata = new();
        }

        public Predmet(string Naziv)
        {
            BrojacPredmeta++;
            this.Id = BrojacPredmeta;

            this.Naziv = Naziv;
        }
        
        public Predmet(string Naziv, int Id = -1)
        {
            if(Id != -1 && Id > BrojacPredmeta && Id > 0)
            {
                BrojacPredmeta = Id;
                this.Id = BrojacPredmeta;
            }
            else
            {
                BrojacPredmeta++;
                this.Id = BrojacPredmeta;
            }

            this.Naziv = Naziv;
        }
        #endregion

        ~Predmet()
        {
            if (BrojacPredmeta > -1)
            {
                BrojacPredmeta--;
            }
            else
            {
                BrojacPredmeta = null;
            }
            this.Id = null;

            this.Naziv = null;
            this.ListaStudnata = null;
        }
        #region Metode
        #region Nasleđene Metod
        public override string ToString()
        {
            return $"| Predmet: [{this.Id}] Naziv: [{this.Naziv}] |";
        }

        public override int GetHashCode()
        {
            if (this.Naziv is null)
                return this.Id.GetHashCode();
            return this.Id.GetHashCode() ^ this.Naziv.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Predmet || obj is null)
                return false;
            return ((Predmet)obj).GetHashCode() == this.GetHashCode();
        }
        #endregion
        #region Implementirane Metode
        public static List<Predmet> UcitajPredmete()
        {
            List<Predmet> ListaPredmeta = new();
            using SqlConnection c = sqlutills.SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = sqlutills.SqlUtills.SqlLoadTable("predmeti", c).ExecuteReader();
            if(rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int id = (int)rdr["id"];
                    string? naziv = rdr["naziv"].ToString();
                    if (naziv is null)
                        throw new Exception("Greška\nPodaci iz baze nisu validni.");
                    ListaPredmeta.Add(new(naziv, id));
                }
            }
            else
            {
                Console.WriteLine("Greška!\nNe postoji fajl predmeta u prosleđenoj putanji.");
            }
            return ListaPredmeta;
        }
        public static void IspisSvihPredmeta(List<Predmet> ListaPredmeta)
        {
            if (ListaPredmeta == null || ListaPredmeta.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema predmeta u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svhi predmeta. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Predmet p in ListaPredmeta)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }
        public static void IspisPredmetaPoIdentifikatoru(List<Predmet> ListaPredmeta, int Id)
        {
            if (ListaPredmeta == null || ListaPredmeta.Count <= 0)
                Console.WriteLine("Greška!\nNema predmeta u sistemu.");
            else
            {
                foreach(Predmet p in ListaPredmeta)
                {
                    if(p.Id == Id)
                        Console.WriteLine($"| ID: [{p.Id}], Predmet: [{p.Naziv}] |" + '\n');
                }
            }
        }
        #endregion
        #endregion
    }
}
