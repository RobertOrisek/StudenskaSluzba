using StudentskaSlužba.src.model;
using StudentskaSlužba.src.UI;

namespace StudentskaSlužba
{
    internal static class PregledEntiteta
    {

        public static void IspisPredmeta()
        {
            Predmet.IspisSvihPredmeta(SkladisteEntiteta.ListaPredmeta);
        }

        public static void IspisNastavnika()
        {
            Nastavnik.IspisSvihNastavnika(SkladisteEntiteta.ListaNastavnika);
        }

        public static void IspisStudenata()
        {
            Student.IspisSvihStudenata(SkladisteEntiteta.ListaStudenata);
        }

        public static void IspisIspitnihRokova()
        {
            IspitniRok.IspisSvihIspitnihRokova(SkladisteEntiteta.ListaIspitnihRokova);
        }

        public static void IspisIspitnihPrijava()
        {
            IspitnaPrijava.IspisSvihIspitnihPrijava(SkladisteEntiteta.ListaIspitnihPrijava, SkladisteEntiteta.ListaStudenata, SkladisteEntiteta.ListaPredmeta, SkladisteEntiteta.ListaIspitnihRokova);
        }

        public static void IspisPredaje()
        {
            Predaje.IspisSvhiPredaje(SkladisteEntiteta.ListaPredaje, SkladisteEntiteta.ListaNastavnika, SkladisteEntiteta.ListaPredmeta);
        }

        public static void IspisPohađa()
        {
            Pohađa.IspisSvihPohađa(SkladisteEntiteta.ListaPohađa, SkladisteEntiteta.ListaStudenata, SkladisteEntiteta.ListaPredmeta);
        }

        public static void MeniPregledEntiteta()
        {
            Meni m = new("Pregled entiteta u sistemu.");
            m.DodajOpciju("Pregled svih nastavnika.", IspisNastavnika);
            m.DodajOpciju("Pregled svih predmeta.", IspisPredmeta);
            m.DodajOpciju("Pregled svih studenata.", IspisStudenata);
            m.DodajOpciju("Pregled svhi ispitnih rokova.", IspisIspitnihRokova);
            m.DodajOpciju("Pregled svih ispitnih prijava.", IspisIspitnihPrijava);
            m.DodajOpciju("Pregled svih entiteta 'Predaje'.", IspisPredaje);
            m.DodajOpciju("Pregled svih entiteta 'Pohađa'.", IspisPohadja);

            m.PokreniMeni();
        }
    }
}
