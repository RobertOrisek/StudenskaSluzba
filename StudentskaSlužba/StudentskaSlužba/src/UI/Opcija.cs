using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSlužba.src.UI
{
    public delegate void MetodaOpcije();
    public class Opcija
    {
        public int? Id { get; set; }

        public string? Naziv { get; set; }

        public MetodaOpcije Metoda { get; set; }

        public Meni? PripadaMeniju { get; set; }

        #region Konstruktori
        public Opcija(Meni PripadaMeniju)
        {
            this.Id = PripadaMeniju.ListaOpcija.Count + 1;

            this.Naziv = "Nova opcija";

            MetodaOpcije MO = NovaMetoda;
            this.Metoda = MO;
        }
        public Opcija(Meni PripadaMeniju, String Naziv, MetodaOpcije Metoda)
        {
            this.Id = PripadaMeniju.ListaOpcija.Count + 1;

            this.Naziv = Naziv;
            this.Metoda = Metoda;
        }
        #endregion

        ~Opcija()
        {
            if(this.PripadaMeniju is not null)
            {
                this.PripadaMeniju.ListaOpcija.Remove(this);
                this.PripadaMeniju = null;
            }
            this.Id = null;

            this.Naziv = null;
        }

        #region Metode
        #region Nasleđene Metode
        public override string ToString()
        {
            return $"| Broj [{this.Id}] Naziv opcije: [{this.Naziv}] |";
        }

        public override int GetHashCode()
        {
            if (this.Id is null || this.Naziv is null || this.Metoda is null)
                return -1;
            return this.Id.GetHashCode() ^ this.Naziv.GetHashCode() ^ this.Metoda.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Opcija || obj is null)
                return false;
            return ((Opcija)obj).GetHashCode() == this.GetHashCode();
        }
        #endregion

        #region ImplementiraneMetode
        public static void NovaMetoda()
        {
            Console.WriteLine("Nova metoda nove opcije.");
        }
        #endregion
        #endregion
    }
}
