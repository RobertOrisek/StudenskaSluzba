using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using StudentskaSlužba.src.sqlutills;

namespace StudentskaSlužba.src.model
{
    public class Nastavnik
    {
        private static int? BrojacNastavnika = -1;
        public int? Id { get; set; }

        public string? Ime { get; set; }
        public string? Prezime { get; set; }

        public string? Zvanje { get; set; }

        public List<Predmet>? ListaPredmeta { get; set; }

        #region Konstruktori
        public Nastavnik()
        {
            BrojacNastavnika++;
            this.Id = BrojacNastavnika;

            this.Ime = null;
            this.Prezime = null;

            this.Zvanje = null;

            this.ListaPredmeta = new();
        }

        public Nastavnik(List<Predmet> ListaPredmeta)
        {
            BrojacNastavnika++;
            this.Id = BrojacNastavnika;

            this.Ime = null;
            this.Prezime = null;

            this.Zvanje = null;

            this.ListaPredmeta = ListaPredmeta;
        }

        public Nastavnik(string Ime, string Prezime, string Zvanje)
        {
            BrojacNastavnika++;
            this.Id = BrojacNastavnika;

            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Zvanje = Zvanje;
        }
        
        public Nastavnik(string Ime, string Prezime, string Zvanje, int Id = -1)
        {
            if(Id != -1 && Id > BrojacNastavnika && Id > 0)
            {
                BrojacNastavnika = Id;
                this.Id = BrojacNastavnika;
            }
            else
            {
                BrojacNastavnika++;
                this.Id = BrojacNastavnika;
            }

            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Zvanje = Zvanje;
        }
        #endregion
        ~Nastavnik()
        {
            if(BrojacNastavnika > -1)
            {
                BrojacNastavnika--;
            }
            else
            {
                BrojacNastavnika = null;
            }
            this.Id = null;

            this.Ime = null;
            this.Prezime = null;
            this.Zvanje = null;
            this.ListaPredmeta = null;
        }

        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Nastavnik: [{this.Id}] Ime: [{this.Ime}] Prezime: [{this.Prezime}] Zvanje: [{this.Zvanje}] |";
        }

        public override int GetHashCode()
        {
            if (this.Ime is null || this.Prezime is null || this.Zvanje is null)
                return -1;
            return this.Id.GetHashCode() ^ this.Ime.GetHashCode() ^ this.Prezime.GetHashCode() ^ this.Zvanje.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Nastavnik || obj is null)
                return false;
            return ((Nastavnik)obj).GetHashCode() == this.GetHashCode();
        }
        #endregion
        #region Implementirane Metode
        public static List<Nastavnik> UcitajNastavnike()
        {
            List<Nastavnik> ListaNastavnika = new();
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = SqlUtills.SqlLoadTable("nastavnici", c).ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int id = (int)rdr["id"];
                    string? ime = rdr["ime"].ToString();
                    string? prezime = rdr["prezime"].ToString();
                    string? zvanje = rdr["zvanje"].ToString();
                    if (ime is null || prezime is null || zvanje is null)
                        throw new Exception("Greška\nPodaci iz baze nisu validni.");
                    ListaNastavnika.Add(new(ime, prezime, zvanje, id));
                }
            }
            else
            {
                Console.WriteLine("Greška!\nNe postoji fajl nastavnika u prosleđenoj putanji.");
            }
            return ListaNastavnika;
        }

        public static void IspisSvihNastavnika(List<Nastavnik> ListaNastavnika)
        {
            if (ListaNastavnika == null || ListaNastavnika.Count <= 0)
                Console.WriteLine("Greška!\nNema nastavnika u sistemu.");
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svih nastavnika. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Nastavnik n in ListaNastavnika)
                {
                    Console.WriteLine(n.ToString());
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisNastavinkaPoIdentifikatoru(List<Nastavnik> ListaNastavnika, int Id)
        {
            if (ListaNastavnika == null || ListaNastavnika.Count <= 0)
                Console.WriteLine("Greška!\nNema nastavnika u sistemu.");
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis nastavnika po identifikatoru [{Id}]. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Nastavnik n in ListaNastavnika)
                {
                    if (n.Id == Id)
                        Console.WriteLine($"| ID: [{n.Id}], Ime: [{n.Ime}], Prezime [{n.Prezime}], Zvanje: [{n.Zvanje}] |" + '\n');
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }
        #endregion
        #endregion
    }
}
