using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using StudentskaSlužba.src.sqlutills;

namespace StudentskaSlužba.src.model
{
    public class Pohađa
    {
        private static int? BrojacPohađa = -1;
        public int? Id { get; set; }

        public int? StudentId { get; set; }
        public int? PredmetId { get; set; }

        #region Konstruktori
        public Pohađa()
        {
            BrojacPohađa++;
            this.Id = BrojacPohađa;

            this.StudentId = -1;
            this.PredmetId = -1;
        }

        public Pohađa(int StudentId, int PredmetId)
        {
            BrojacPohađa++;
            this.Id = BrojacPohađa;

            this.StudentId = StudentId;
            this.PredmetId = PredmetId;
        }
        #endregion
        ~Pohađa()
        {
            if (BrojacPohađa > -1)
            {
                BrojacPohađa--;
            }
            else
            {
                BrojacPohađa = null;
            }
            this.Id = -1;

            this.StudentId = 0;
            this.PredmetId = 0;
        }
        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Pohađa: [{this.Id}] |";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Pohađa || obj is null)
                return false;
            return ((Pohađa)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.StudentId.GetHashCode() ^ this.PredmetId.GetHashCode();
        }
        #endregion
        #region Implementirane Metode
        public static List<Pohađa> UcitajPohađa()
        {
            List<Pohađa> ListaPohađa = new();
            using SqlConnection c = SqlUtills.OpenSqlConnection();
            using SqlDataReader rdr = SqlUtills.SqlLoadTable("pohadja", c).ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int student_id = (int)rdr["student_id"];
                    int predmet_id = (int)rdr["predmet_id"];

                    ListaPohađa.Add(new(student_id, predmet_id));
                }
            }
            else
            {
                Console.WriteLine("Greška!\nProsleđena putanja nije validna.");
            }
            return ListaPohađa;
        }

        public static void IspisSvihPohađa(List<Pohađa> ListaPohađa, List<Student> ListaStudenata, List<Predmet> ListaPredmeta)
        {
            if(ListaPohađa == null || ListaPohađa.Count == 0)
            {
                Console.WriteLine("Greška!\nNema entiteta 'Pohađa' u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine("| Ispis svih entiteta 'Pohađa'. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Pohađa po in ListaPohađa)
                {
                    Console.WriteLine("\n\n" + po.ToString());
                    foreach(Student s in ListaStudenata)
                    {
                        if(s.Id == po.StudentId)
                            Console.WriteLine(s.ToString());
                    }
                    foreach(Predmet p in ListaPredmeta)
                    {
                        if(p.Id == po.PredmetId)
                            Console.WriteLine(p.ToString());
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }

        public static void IspisPohađaPoIdentifikatoru(List<Pohađa> ListaPohađa, int Id)
        {
            if (ListaPohađa == null || ListaPohađa.Count == 0)
            {
                Console.WriteLine("Greška!\nNema entiteta 'Pohađa' u sistemu.");
            }
            else
            {
                Console.WriteLine("|-------------------------------------------------|");
                Console.WriteLine($"| Ispis svih entiteta 'Pohađa' po identifikatoru [{Id}]. |");
                Console.WriteLine("|-------------------------------------------------|");
                foreach (Pohađa po in ListaPohađa)
                {
                    if(po.Id == Id)
                    {
                        Console.WriteLine(po.ToString());
                    }
                }
                Console.WriteLine("|-------------------------------------------------|");
            }
        }
        #endregion
        #endregion
    }
}
