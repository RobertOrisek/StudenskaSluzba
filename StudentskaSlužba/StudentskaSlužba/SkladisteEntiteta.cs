using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSlužba.src.model;

namespace StudentskaSlužba
{
    public static class SkladisteEntiteta
    {
        public static List<Predmet> ListaPredmeta = new();

        public static List<Nastavnik> ListaNastavnika = new();

        public static List<Student> ListaStudenata = new();

        public static List<IspitniRok> ListaIspitnihRokova = new();

        public static List<IspitnaPrijava> ListaIspitnihPrijava = new();

        public static List<Pohađa> ListaPohađa = new();

        public static List<Predaje> ListaPredaje = new();
    }
}
