using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using StudentskaSlužba.src.sqlutills;

namespace StudentskaSlužba.src.model
{
    public class IspitnaPrijava
    {
        private static int? BrojacIspitnihPrijava = -1;
        public int? Id { get; set; }

        public int? BodoviTeorija { get; set; }
        public int? BodoviZadaci { get; set; }

        public int? OcenaTeorija { get; set; }
        public int? OcenaZadaci { get; set; }

        public int? StudentId { get; set; }
        public int? PredmetId { get; set; }
        public int? IspitniRokId { get; set; }

        #region Konstruktori
        public IspitnaPrijava()
        {
            BrojacIspitnihPrijava++;
            this.Id = BrojacIspitnihPrijava;
        }

        public IspitnaPrijava(int BodoviTeorija, int BodoviZadaci)
        {
            BrojacIspitnihPrijava++;
            this.Id = BrojacIspitnihPrijava;

            this.BodoviTeorija = BodoviTeorija;
            this.BodoviZadaci = BodoviZadaci;
        }

        public IspitnaPrijava(int BodoviTeorija, int BodoviZadaci, int StudentId, int PredmetId, int IspitniRokId, int Id = -1)
        {
            if (Id != -1 && Id > BrojacIspitnihPrijava && Id > 0)
            {
                BrojacIspitnihPrijava = Id;
                this.Id = BrojacIspitnihPrijava;
            }
            else
            {
                BrojacIspitnihPrijava++;
                this.Id = BrojacIspitnihPrijava;
            }

            this.BodoviTeorija = BodoviTeorija;
            this.BodoviZadaci = BodoviZadaci;

            this.StudentId = StudentId;
            this.PredmetId = PredmetId;
            this.IspitniRokId = IspitniRokId;
        }
        #endregion

        ~IspitnaPrijava()
        {
            if(BrojacIspitnihPrijava > -1)
            {
                BrojacIspitnihPrijava--;
            }
            else
            {
                BrojacIspitnihPrijava = null;
            }
            this.Id = null;

            this.BodoviTeorija = -1;
            this.BodoviZadaci = -1;
            this.OcenaTeorija = -1;
            this.OcenaZadaci = -1;
            this.StudentId = -1;
            this.PredmetId = -1;
            this.IspitniRokId = -1;
        }
        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            this.IzracunaOcenu();
            return $"| Ispitna prijava: [{this.Id}] Bodovi teorija: [{this.BodoviTeorija}] Bodovi zadaci: [{this.BodoviZadaci}] Ocena teorija: [{this.OcenaTeorija}] Ocena zadaci: [{this.OcenaZadaci}] Prosek: [{this.IzracunajProsek().ToString()}] |";
        } 

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.StudentId.GetHashCode() ^ this.PredmetId.GetHashCode() ^ this.IspitniRokId.GetHashCode() ^ this.BodoviTeorija.GetHashCode() ^ this.BodoviZadaci.GetHashCode() ^ this.OcenaTeorija.GetHashCode() ^ this.OcenaZadaci.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not IspitnaPrijava || obj is null)
                return false;
            return ((IspitnaPrijava)obj).GetHashCode() == this.GetHashCode();
        }
        #endregion
        #region Implementirane Metode
        public static List<IspitnaPrijava> UcitajIspitnePrijave()
        {
            List<IspitnaPrijava> ListaIspitnihPrijava = new();
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = SqlUtills.SqlLoadTable("ispitne_prijave", c).ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int student_id = (int)rdr["student_id"];
                    int predmet_id = (int)rdr["predmet_id"];
                    int ispitni_rok_id = (int)rdr["ispitni_rok_id"];
                    int teorija = (int)rdr["teorija"];
                    int zadaci = (int)rdr["zadaci"];

                    IspitnaPrijava ir = new(teorija, zadaci, student_id, predmet_id, ispitni_rok_id);

                    ListaIspitnihPrijava.Add(ir);
                }

            }
            else
            {
                Console.WriteLine("Greška!\nNe postoji fajl ispitnih prijava na prosleđenoj putanji.");
            }
            return ListaIspitnihPrijava;
        }

        public static void IspisSvihIspitnihPrijava(List<IspitnaPrijava> ListaIspitnihPrijava, List<Student> ListaStudenata, List<Predmet> ListaPredmeta, List<IspitniRok> ListaIspitnihRokova)
        {
            if(ListaIspitnihPrijava == null || ListaIspitnihPrijava.Count == 0)
            {
                Console.WriteLine("Greška!\nLista ispitnih prijava nije validna.");
            }
            else if(ListaStudenata == null || ListaStudenata.Count == 0)
            {
                Console.WriteLine("Greška!\nLista studenata nije validna.");
            }
            else if(ListaPredmeta == null || ListaPredmeta.Count == 0)
            {
                Console.WriteLine("Greška!\nLista predmeta nije validna.");
            }
            else if(ListaIspitnihRokova == null || ListaIspitnihRokova.Count == 0)
            {
                Console.WriteLine("Greška!\nLista ispitnih rokova nije validna.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svih ispitnih prijava. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach(IspitnaPrijava ip in ListaIspitnihPrijava)
                {
                    Console.WriteLine("\n\n" + ip.ToString());
                    foreach(Student s in ListaStudenata)
                    {
                        if (s.Id == ip.StudentId)
                            Console.WriteLine('\n' + s.ToString());
                    }
                    foreach(Predmet p in ListaPredmeta)
                    {
                        if(p.Id == ip.PredmetId)
                            Console.WriteLine('\n' + p.ToString());
                    }
                    foreach(IspitniRok ir in ListaIspitnihRokova)
                    {
                        if(ir.Id == ip.IspitniRokId)
                            Console.WriteLine('\n' + ir.ToString());
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisIspitnihPrijavaPoIdentifikatoru(List<IspitnaPrijava> ListaIspitnihPrijava, int Id)
        {
            if (ListaIspitnihPrijava == null || ListaIspitnihPrijava.Count == 0)
            {
                Console.WriteLine("Greška!\nLista ispitnih prijava nije validna.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis svih ispitnih prijava po identifikatoru [{Id}]. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (IspitnaPrijava ip in ListaIspitnihPrijava)
                {
                    if(ip.Id == Id)
                    {
                        Console.WriteLine(ip.ToString());
                    }
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public int IzracunaOcenu()
        {
            //ocena teorija
            if (BodoviTeorija >= 91)
            {
                this.OcenaTeorija = 10;
            }
            else if (BodoviTeorija >= 81)
            {
                this.OcenaTeorija = 9;
            }
            else if (BodoviTeorija >= 71)
            {
                this.OcenaTeorija = 8;
            }
            else if (BodoviTeorija >= 61)
            {
                this.OcenaTeorija = 7;
            }
            else if (BodoviTeorija >= 51)
            {
                this.OcenaTeorija = 6;
            }
            else
                this.OcenaTeorija = 5;
            // ocena zadaci
            if (BodoviZadaci >= 91)
            {
                this.OcenaZadaci = 10;
            }
            else if (BodoviZadaci >= 81)
            {
                this.OcenaZadaci = 9;
            }
            else if (BodoviZadaci >= 71)
            {
                this.OcenaZadaci = 8;
            }
            else if (BodoviZadaci >= 61)
            {
                this.OcenaZadaci = 7;
            }
            else if (BodoviZadaci >= 51)
            {
                this.OcenaZadaci = 6;
            }
            else
                this.OcenaZadaci = 5;
            return this is not null ? ((int)this.OcenaTeorija + (int)this.OcenaZadaci) / 2 : -1;
        }

        public int IzracunajProsek() 
        {
            if (BodoviTeorija is null || BodoviZadaci is null)
                return -1;
            return ((int)BodoviTeorija + (int)BodoviZadaci) / 2;
        }
        #endregion
        #endregion
    }
}
