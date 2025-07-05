using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSlužba.src.model;

namespace StudentskaSlužba
{
    internal static class UcitavanjeEntiteta
    {
        public static void UcitajNastavnike()
        {
            SkladisteEntiteta.ListaNastavnika = Nastavnik.UcitajNastavnike();
        }

        public static void UcitajPredmete()
        {
            SkladisteEntiteta.ListaPredmeta = Predmet.UcitajPredmete();
        }

        public static void UcitajStudente()
        {
            SkladisteEntiteta.ListaStudenata = Student.UcitajStudnete();
        }

        public static void UcitajIspitneRokove()
        {
            SkladisteEntiteta.ListaIspitnihRokova = IspitniRok.UcitajIspitneRokove();
        }

        public static void UcitajIspitnePrijave()
        {
            SkladisteEntiteta.ListaIspitnihPrijava = IspitnaPrijava.UcitajIspitnePrijave();
        }

        public static void UcitajPohađa()
        {
            SkladisteEntiteta.ListaPohađa = Pohađa.UcitajPohađa();
        }

        public static void UcitajPredaje()
        {
            SkladisteEntiteta.ListaPredaje = Predaje.UcitajPredaje();
        }

        public static void UcitajSve()
        {
            UcitajNastavnike();
            UcitajPredmete();
            UcitajStudente();
            UcitajIspitneRokove();
            UcitajIspitnePrijave();
            UcitajPredaje();
            UcitajPohađa();
        }
    }
}
