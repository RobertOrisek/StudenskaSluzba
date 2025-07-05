using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using StudentskaSlužba.src.interfaces;
using StudentskaSlužba.src.sqlutills;

namespace StudentskaSlužba.src.model
{
    public class IspitniRok : ISavable
    {
        private static int? BrojacIspitnihRokova = -1;
        public int? Id { get; set; }

        public string? Naziv { get; set; }
        public string? Pocetak { get; set; }
        public string? Kraj { get; set; }

        #region Konstruktori
        public IspitniRok()
        {
            BrojacIspitnihRokova++;
            this.Id = BrojacIspitnihRokova;
        }

        public IspitniRok(string Naziv, string Pocetak, string Kraj)
        {
            BrojacIspitnihRokova++;
            this.Id = BrojacIspitnihRokova;

            this.Naziv = Naziv;
            this.Pocetak = Pocetak;
            this.Kraj = Kraj;
        }
        public IspitniRok(string Naziv, string Pocetak, string Kraj, int Id = -1)
        {
            if(Id != -1 && Id > BrojacIspitnihRokova && Id > 0)
            {
                BrojacIspitnihRokova = Id;
                this.Id = BrojacIspitnihRokova;
            }
            else
            {
                BrojacIspitnihRokova++;
                this.Id = BrojacIspitnihRokova;
            }

            this.Naziv = Naziv;
            this.Pocetak = Pocetak;
            this.Kraj = Kraj;
        }
        #endregion
        ~IspitniRok()
        {
            if(BrojacIspitnihRokova > -1)
            {
                BrojacIspitnihRokova--;
            }
            else
            {
                BrojacIspitnihRokova = null;
            }
            this.Id = null;

            this.Naziv = null;
            this.Pocetak = null;
            this.Kraj = null;
        }
        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Ispitni rok: [{this.Id}]  Naziv: [{this.Naziv}] Početak: [{this.Pocetak}] Kraj: [{this.Kraj}] |";
        }

        public override int GetHashCode()
        {
            if (this.Naziv is null || this.Pocetak is null || this.Kraj is null)
                return -1;
            return this.Id.GetHashCode() ^ this.Naziv.GetHashCode() ^ this.Pocetak.GetHashCode() ^ this.Kraj.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not IspitniRok || obj is null)
                return false;
            return ((IspitniRok)obj).GetHashCode() == this.GetHashCode();
        }
        //ISavable interface
        public void SacuvajUBazu()
        {
            string Table = "ispitni_rokovi";
            string Fields = "naziv, pocetak, kraj";
            string Values = $"{this.Naziv}, {this.Pocetak}, {this.Kraj}";
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            SqlUtills.SqlInsertInto(Table, Fields, Values, c);
        }
        #endregion
        #region Implementirane Metode
        public static List<IspitniRok> UcitajIspitneRokove()
        {
            List<IspitniRok> ListaIspitnihRokova = new();
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = SqlUtills.SqlLoadTable("ispitni_rokovi", c).ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int id = (int)rdr["id"];
                    string? naziv = rdr["naziv"].ToString();
                    string? pocetak = rdr["pocetak"].ToString();
                    string? kraj = rdr["kraj"].ToString();

                    if (naziv is null || pocetak is null || kraj is null)
                        throw new Exception("Greška!\nNema podataka u bazi.");
                    ListaIspitnihRokova.Add(new(naziv, pocetak, kraj, id));
                }
            }
            else
            {
                Console.WriteLine("Greška!\nNe postoji fajl ispitnih rokova na prosleđenoj putanji.");
            }
            return ListaIspitnihRokova;
        }

        public static void IspisSvihIspitnihRokova(List<IspitniRok> ListaIspitnihRokova)
        {
            if (ListaIspitnihRokova == null || ListaIspitnihRokova.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema ispitnih rokova u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svih ispitnih rokova. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (IspitniRok ir in ListaIspitnihRokova)
                {
                    Console.WriteLine(ir.ToString());
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisIspitnihRokovaPoIdentifikatoru(List<IspitniRok> ListaIspitnihRokova, int Id)
        {
            if (ListaIspitnihRokova == null || ListaIspitnihRokova.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema ispitnih rokova u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis ispitnih rokova po identifikatoru [{Id}]. |");
                Console.WriteLine("|-------------------------------------------------|");

                foreach (IspitniRok ir in ListaIspitnihRokova)
                {
                    if (ir.Id == Id)
                    {
                        Console.WriteLine(ir.ToString());
                    }
                }

                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public bool IzmenaIspitnogRoka(string Naziv, string Pocetak, string Kraj)
        {
            if(Naziv != null)
            {
                this.Naziv = Naziv;
            }
            if (Pocetak != null)
            {
                this.Pocetak = Pocetak;
            }
            if (Kraj != null)
            {
                this.Kraj = Kraj;
            }
            return true;
        }
        #endregion
        #endregion
    }
}
