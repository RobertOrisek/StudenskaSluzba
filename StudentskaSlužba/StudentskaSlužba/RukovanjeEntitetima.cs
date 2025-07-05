using StudentskaSlužba.src.model;
using StudentskaSlužba.src.UI;
using StudentskaSlužba.src.sqlutills;
using System.Data.SqlClient;

namespace StudentskaSlužba
{
    internal class RukovanjeEntitetima
    {
        public static void DodajNastavnika()
        {
            Console.WriteLine();
            Console.WriteLine();

            Nastavnik n = new();
            Console.WriteLine("Unesite ime nastavnika kojeg želite da dodate u sistem: ");
            n.Ime = Console.ReadLine();
            Console.WriteLine("Unesite prezime nastavnika kojeg želite da dodate u sistem: ");
            n.Prezime = Console.ReadLine();
            Console.WriteLine("Unesite zvanje nastavnika kojeg želite da dodate u sistem: ");
            n.Zvanje = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine();
            SkladisteEntiteta.ListaNastavnika.Add(n);
            Console.WriteLine($"Uspešno ste dodali nastavnika [{n.ToString()}] u sistem.");

            //ubacivanje nastavnika u bazu
            if (n.Ime is not null && n.Prezime is not null && n.Zvanje is not null)
            {
                using SqlConnection c = SqlUtills.OpenSqlConnection();
                string table, fields, values;

                table = "nastavnici";
                fields = "ime,prezime,zvanje";
                values = n.Ime + ',' + n.Prezime + ',' + n.Zvanje;

                SqlUtills.SqlInsertInto(table, fields, values, c);
                Console.WriteLine($"Uspešno ste dodali nastavnika [{n.ToString()}] u bazu podataka.");
            }
            else
                Console.WriteLine("Greška\nDa bi se student sačuvao u bazu mora imati vrednost.");
        }

        public static void DodajPredmet()
        {
            Console.WriteLine();
            Console.WriteLine();

            Predmet p = new();
            Console.WriteLine("Unesite naziv predmeta koji želite da dodate u sistem: ");

            p.Naziv = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();

            SkladisteEntiteta.ListaPredmeta.Add(p);

            Console.WriteLine($"Uspešno ste dodali predmet [{p.ToString()}] u sistem.");

            //ubacivanje predmeta u bazu
            if (p.Naziv is not null)
            {
                using SqlConnection c = SqlUtills.OpenSqlConnection();
                string table, fields, values;

                table = "predmeti";
                fields = "naziv";
                values = p.Naziv;

                SqlUtills.SqlInsertInto(table, fields, values, c);

                Console.WriteLine($"Uspešno ste dodali predmet [{p.ToString()}] u bazu podataka.");
            }
            else
                Console.WriteLine("Greška\nDa bi se predmet sačuvao u bazu mora imati vrednost.");
        }

        public static void DodajIspitniRok()
        {
            Console.WriteLine();
            Console.WriteLine();

            IspitniRok ir = new();

            Console.WriteLine("Unesite mesec naziv ispitnog roka kojeg želite da dodate u sistem: ");
            ir.Naziv = Console.ReadLine();

            Console.WriteLine("Unesite početak ispitnog roka kojeg želite da dodate u sistem: ");
            ir.Pocetak = Console.ReadLine();

            Console.WriteLine("Unesite kraj ispitnog roka kojeg želite da dodate u sistem: ");
            ir.Kraj = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine();

            SkladisteEntiteta.ListaIspitnihRokova.Add(ir);
            Console.WriteLine($"Uspešno ste dodali ispitni rok [{ir.ToString()}] u sistem.");

            //ubacivanje ispitnog roka u bazu podataka
            if (ir.Naziv is not null && ir.Pocetak is not null && ir.Kraj is not null)
            {
                using SqlConnection c = SqlUtills.OpenSqlConnection();
                string table = "ispitni_rokovi";
                string fields = "naziv,pocetak,kraj";
                string values = ir.Naziv + ',' + ir.Pocetak + ',' + ir.Kraj;

                SqlUtills.SqlInsertInto(table, fields, values, c);
                Console.WriteLine($"Uspešno ste dodali ispitni rok [{ir.ToString()}] u bazu podataka.");
            }
            else
                Console.WriteLine("Greška\nDa bi se ispitni rok sačuvao u bazu mora imati vrednost.");
        }

        public static void DodajIspitnuPrijavu()
        {
            Console.WriteLine();
            Console.WriteLine();

            IspitnaPrijava ip = new();

            Console.WriteLine("Unesite index studenta kojeg zelite da prijavite za ispit: ");

            string? Index = Console.ReadLine();
            foreach (Student s in SkladisteEntiteta.ListaStudenata)
            {
                if (s.Index == Index)
                {
                    ip.StudentId = s.Id;
                }
            }

            Console.WriteLine($"Unesite naziv predmeta na koji želite da prijavite studenta [{SkladisteEntiteta.ListaStudenata[(int)ip.StudentId].ToString()}]");

            string? NazivPredmeta = Console.ReadLine();
            foreach (Predmet p in SkladisteEntiteta.ListaPredmeta)
            {
                if (p.Naziv == NazivPredmeta)
                {
                    ip.PredmetId = p.Id;
                }
            }

            Console.WriteLine($"Unesite ispitni rok za koji želite da prijavte studenta [{SkladisteEntiteta.ListaIspitnihPrijava[(int)ip.StudentId].ToString()}]");

            string? NazivIspitnogRoka = Console.ReadLine();
            foreach(IspitniRok ir in SkladisteEntiteta.ListaIspitnihRokova)
            {
                if(ir.Naziv == NazivIspitnogRoka)
                {
                    ip.IspitniRokId = ir.Id;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            SkladisteEntiteta.ListaIspitnihPrijava.Add(ip);
            Console.WriteLine($"Uspešno ste dodali ispitnu prijavu [{ip.ToString}] u sistem.");

            //ubacivanje ispitne prijave u bazu
            if(ip.StudentId is not null && ip.PredmetId is not null)
            {
                using SqlConnection c = SqlUtills.OpenSqlConnection();
                string table = "ispitne_prijave";
                string fields = "student_id, predmet_id, ispitni_rok_id";
                string values = ip.StudentId.ToString() + ',' + ip.PredmetId + ','+ ip.IspitniRokId;

                SqlUtills.SqlInsertInto(table, fields, values, c);
                Console.WriteLine($"Uspešno ste dodali ispitnu prijavu [{ip.ToString}] u bazu podataka.");
            }
            else
                Console.WriteLine("Greška\nDa bi se ispitna prijava sačuvao u bazu mora imati vrednost.");
        }

        public static void MeniRukovanjeEntitetima()
        {
            Meni m = new("Rukovanje entitetima.");

            m.DodajOpciju("Dodavanje nastavnika u sistem.", DodajNastavnika);
            m.DodajOpciju("Dodavanje predmeta u sistem.", DodajPredmet);
            m.DodajOpciju("Dodavanje ispitnog rokova u sistem.", DodajIspitniRok);
            m.DodajOpciju("Dodavanje ispitne prijave u sistem.", DodajIspitnuPrijavu);

            m.PokreniMeni();
        }
    }
}
