using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentskaSlužba.src.sqlutills;

namespace StudentskaSlužba.src.model
{
    public class Student
    {
        private static int? BrojacStudnata = -1;
        public int? Id { get; set; }
        public string Index { get; set; }

        public string? Ime { get; set; }
        public string? Prezime { get; set; }

        public string? Grad { get; set; }

        public List<Predmet>? ListaPredmeta { get; set; }

        #region Konstruktori
        public Student()
        {
            BrojacStudnata++;
            this.Id = BrojacStudnata;

            this.Index = String.Empty;
            this.Ime = null;
            this.Prezime = null;

            this.Grad = null;

            this.ListaPredmeta = new();
        }
        public Student(List<Predmet> ListaPredmeta)
        {
            BrojacStudnata++;
            this.Id = BrojacStudnata;

            this.Index = String.Empty;
            this.Ime = null;
            this.Prezime = null;

            this.Grad = null;

            this.ListaPredmeta = ListaPredmeta;
        }
        public Student(string Ime, string Prezime, string Grad, string Index, int Id = -1)
        {
            if(Id != -1 && Id > BrojacStudnata && Id > 0)
            {
                BrojacStudnata = Id;
                this.Id = BrojacStudnata;
            }
            else
            {
                BrojacStudnata++;
                this.Id = BrojacStudnata;
            }
            this.Index = Index;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Grad = Grad;
        }
        #endregion

        ~Student()
        {
            if (BrojacStudnata > -1)
            {
                BrojacStudnata--;
            }
            else
            {
                BrojacStudnata = null;
            }
            this.Id = null;

            this.Index = String.Empty;
            this.Ime = null;
            this.Prezime = null;
            this.Grad = null;
            this.ListaPredmeta = null;
        }
        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Student: [{this.Id}] Index: [{this.Index}] Ime: [{this.Ime}] Prezime: [{this.Prezime}] Grad: [{this.Grad}] |";
        }

        public override int GetHashCode()
        {
            if (this.Index is null || this.Ime is null || this.Prezime is null || this.Grad is null)
                return this.Id.GetHashCode();
            return this.Id.GetHashCode() ^ this.Index.GetHashCode() ^ this.Ime.GetHashCode() ^ this.Prezime.GetHashCode() ^ this.Grad.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Student || obj is null)
                return false;
            return ((Student)obj).GetHashCode() == this.GetHashCode();
        }
        #endregion
        #region Implementirane Metode
        public static List<Student> UcitajStudnete()
        {
            List<Student> ListaStudnata = new();
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = SqlUtills.SqlLoadTable("studenti", c).ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int id = (int)rdr["id"];
                    string? index = rdr["studentski_index"].ToString();
                    string? ime = rdr["ime"].ToString();
                    string? prezime = rdr["prezime"].ToString();
                    string? grad = rdr["grad"].ToString();
                    if (index is null || ime is null || prezime is null || grad is null)
                        throw new Exception("Greška\nPodaci iz baze nisu validni.");
                    ListaStudnata.Add(new(ime, prezime, grad, index,id));
                }
                return ListaStudnata;
            }
            else
            {
                Console.WriteLine("Greška!\nNe postoji fajl studenata u prosleđenoj putanji.");
            }
            return ListaStudnata;
        }

        public static void IspisSvihStudenata(List<Student> ListaStudenata)
        {
            if (ListaStudenata == null || ListaStudenata.Count <= 0)
                Console.WriteLine("Greška!\nNema studnata u sistemu.");
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svih studenata. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Student s in ListaStudenata)
                {
                    Console.WriteLine(s.ToString());
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisPoIdentifikatoru(List<Student> ListaStudenata, int Id)
        {
            if (ListaStudenata == null || ListaStudenata.Count <= 0)
                Console.WriteLine("Greška!\nNema studnata u sistemu.");
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis studnata po identifikatoru: [{Id}] |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Student s in ListaStudenata)
                {
                    if (s.Id == Id)
                        Console.WriteLine(s.ToString() + '\n');
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisStudenataPoSmeru(List<Student> ListaStudenata, string smer)
        {
            if (ListaStudenata is null || ListaStudenata.Count <= 0)
                Console.WriteLine("Greška!\nNema studnata u sistemu.");
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis studnata po smeru: [{smer}] |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Student s in ListaStudenata)
                {
                    if (s.Index.Contains(smer))
                        Console.WriteLine(s.ToString());
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisStudenataPoGodiniUpisa(List<Student> ListaStudenata)
        {
            if (ListaStudenata == null || ListaStudenata.Count <= 0)
            {
                Console.WriteLine("Greška!\nNema studnata u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Statistika upisanih studenata po goidini upisa. |");
                Console.WriteLine("|-------------------------------------------------|");
                int godina2013 = 0, godina2015 = 0, godina2016 = 0;
                if(ListaStudenata.Count > 0)
                {
                    foreach (Student s in ListaStudenata)
                    {
                        if (s.Index.Contains("2013"))
                            godina2013++;
                        else if (s.Index.Contains("2015"))
                            godina2015++;
                        else
                            godina2016++;
                    }
                    Console.WriteLine($"| 2013. godine upisalo se [{godina2013}] studenata. |");
                    Console.WriteLine($"| 2015. godine upisalo se [{godina2015}] studenata. |");
                    Console.WriteLine($"| 2016. godine upisalo se [{godina2016}] studenata. |");
                    Console.WriteLine("|-------------------------------------------------|");
                }
                else
                    Console.WriteLine("Greška\nLista studenata je prazna.");
            }
        }
        #endregion
        #endregion
    }
}
