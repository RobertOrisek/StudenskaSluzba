using System;
using System.IO;
using System.Collections.Generic;
using StudentskaSlužba.src.model;
using StudentskaSlužba.src.UI;

namespace StudentskaSlužba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("\n\n*********************************************");
            Console.WriteLine("Početak programa.");
            Console.WriteLine("\n\n*********************************************");
            //ucitavanje entiteta
            UcitavanjeEntiteta.UcitajSve();

            //pregled entitetea
            Meni m = new("Pregled podataka u sistemu.");
            m.DodajOpciju("Pregled svih entiteta u sistemu.", PregledEntiteta.MeniPregledEntiteta);

            //rukovanje entitetima
            m.DodajOpciju("Rukovanje entitetima.", RukovanjeEntitetima.MeniRukovanjeEntitetima);

            //pokretanje menija

            m.PokreniMeni();
            Console.WriteLine("\n\n*********************************************");
            Console.WriteLine("Kraj programa.");
            Console.WriteLine("\n\n*********************************************");
        }
    }
}
