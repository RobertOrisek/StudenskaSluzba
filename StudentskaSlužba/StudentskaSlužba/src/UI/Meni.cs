using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSlužba.src.UI
{
    public class Meni
    {
        private static int? BrojacMenija = -1;
        public int? Id { get; set; }

        public string? Naziv { get; set; }

        public List<Opcija>? ListaOpcija { get; set; }

        #region Konstruktori
        public Meni()
        {
            this.Naziv = "Novi Meni";
            this.ListaOpcija = new();
        }

        public Meni(String Naziv)
        {
            this.Naziv = Naziv;
            this.ListaOpcija = new();
        }

        public Meni(String Naziv, List<Opcija> ListaOpcija)
        {
            this.Naziv = Naziv;
            this.ListaOpcija = ListaOpcija;
        }
        #endregion

        ~Meni()
        {
            if (BrojacMenija > -1)
            {
                BrojacMenija--;
            }
            else
            {
                BrojacMenija = null;
            }
            this.Id = null;

            this.Naziv = null;
            this.ListaOpcija = null;
        }

        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Naziv menija: [{this.Naziv}] |";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Meni || obj is null)
                return false;
            return ((Meni)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            if (this.Naziv is null || this.ListaOpcija is null)
                return this.Id.GetHashCode();
            return this.Naziv.GetHashCode() ^ this.ListaOpcija.GetHashCode();
        }

        #endregion
        #region Implementirane Metode
        public void IspisMenija()
        {
            if(this.ListaOpcija == null || this.ListaOpcija.Count <= 0)
                Console.WriteLine(this.ToString());
            else
            {
                Console.WriteLine();
                Console.WriteLine($"| Meni [{this.Naziv}] Opcije: |");
                foreach (Opcija o in this.ListaOpcija)
                {
                    Console.WriteLine(o.ToString());
                }
                Console.WriteLine("| Unesite broj [0] za izlaz. |");
                Console.WriteLine();
            }
        }

        public int UnosSaKonzole()
        {
            Console.WriteLine("Unesite opciju [0 - {0}] :", this.ListaOpcija?.Count);

            string? unos = Console.ReadLine();
            Console.WriteLine();

            int OdabranaOpcija;
            bool DaLiJeBroj = int.TryParse(unos, out OdabranaOpcija);

            if (!DaLiJeBroj)
            {
                Console.WriteLine("Greška\nMožete uneti jedino brojne vrednosti.");
                return -1;
            }
            if(OdabranaOpcija < 0 || OdabranaOpcija > this.ListaOpcija?.Count)
            {
                Console.WriteLine("Greška!\nUneta opcija ne postoji na listi opcija!");
                return -1;
            }
            return OdabranaOpcija;
        }

        public void DodajOpciju(string NazivOpcije, MetodaOpcije Metoda)
        {
            if (Metoda is not null && NazivOpcije is not null)
            {
                if (this.ListaOpcija == null)
                    this.ListaOpcija = new();

                this.ListaOpcija.Add(new(this, NazivOpcije, Metoda));
                Console.WriteLine($"Opcija [{NazivOpcije}] uspešno dodata meniju [{ToString()}]");
            }
        }

        public void IzbrisiOpciju(int Index)
        {
            if (this.ListaOpcija == null)
            {
                Console.WriteLine($"Ovaj meni [{this.ToString()}] nema nijednu opciju.");
            }
            else
            {
                if (ListaOpcija[Index] is not null)
                {
                    Console.WriteLine($"Opcija [{ListaOpcija[Index].Naziv}] uspešno uklonjena iz menija [{this.ToString()}].");
                    ListaOpcija.RemoveAt(Index);
                }
                else
                {
                    Console.WriteLine($"Greška!\nMeni [{this.ToString()}] ne sadrži opciju [{Index}]");
                }
            }
        }

        public void PokreniMeni()
        {
            int OdabranaOpcija = 0;
            do
            {
                IspisMenija();
                OdabranaOpcija = this.UnosSaKonzole();
                IzvrsiOpciju(OdabranaOpcija);
            } while (OdabranaOpcija > 0 && OdabranaOpcija <= this.ListaOpcija?.Count);
        }

        public void IzvrsiOpciju(int OdabranaOpcija)
        {
            if(OdabranaOpcija > 0 && OdabranaOpcija <= ListaOpcija?.Count)
            {
                if(ListaOpcija[OdabranaOpcija -1].Metoda is not null)
                    ListaOpcija[OdabranaOpcija - 1].Metoda();
            }
            Console.WriteLine();
        }
        #endregion
        #endregion
    }
}
