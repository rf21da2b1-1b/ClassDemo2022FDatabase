using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using BilLib.model;

namespace BilDatabaseApp
{
    public class AWorker 
    {
        public AWorker()
        {

        }

        public void Start()
        {
            //DoCar();
            //DoOwner();
            DoCar2();
        }

        private static void DoCar()
        {
            CarManager mgr = new CarManager();

            Console.WriteLine("Alle biler");
            List<Car> cars = mgr.GetAll();
            foreach (Car c in cars)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("Bil med reg nummer DD22334");
            Console.WriteLine(mgr.GetByRegNr("DD22334"));


            // opret bil
            Console.WriteLine("Oprettet bil");
            Car nyBil = new Car("ZZ22334", "Blå", false, "11223344");
            Car c1 = mgr.Create(nyBil);
            Console.WriteLine(c1);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // modify
            Console.WriteLine("Ændret bil");
            nyBil.Colour = "Meget sort";
            Car c2 = mgr.Modify(nyBil.RegNr, nyBil);
            Console.WriteLine(c2);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // slet bil
            Console.WriteLine("Slettet bil");
            Car c3 = mgr.Delete(nyBil.RegNr);
            Console.WriteLine(c3);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();
        }

        private static void DoOwner()
        {
            CarOwnerManager mgr = new CarOwnerManager();

            Console.WriteLine("Alle ejere");
            List<CarOwner> carowners = mgr.GetAll();
            foreach (CarOwner c in carowners)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("Ejer med tlf 11223344");
            Console.WriteLine(mgr.GetByString("11223344"));


            // opret bilEjer
            Console.WriteLine("Oprettet Ejer");
            CarOwner nyEjer = new CarOwner("99887766", "Per");
            CarOwner c1 = mgr.Create(nyEjer);
            Console.WriteLine(c1);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // modify
            Console.WriteLine("Ændret Ejer");
            nyEjer.Name = "Hr Per";
            CarOwner c2 = mgr.Modify(nyEjer.Phone, nyEjer);
            Console.WriteLine(c2);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // slet bil
            Console.WriteLine("Slettet Ejer");
            CarOwner c3 = mgr.Delete(nyEjer.Phone);
            Console.WriteLine(c3);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();
        }

        /*
         * Hvor relation car <-> owner er objekter
         */
        private static void DoCar2()
        {
            Car2Manager2 mgr = new Car2Manager2();

            Console.WriteLine("Alle biler");
            List<Car2> cars = mgr.GetAll();
            foreach (Car2 c in cars)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("Bil med reg nummer DD22334");
            Console.WriteLine(mgr.GetByString("DD22334"));


            // opret bil
            Console.WriteLine("Oprettet bil");
            // NEED a real owner i.e. an object
            CarOwner owner = new CarOwner("43658791", "Charlotte");
            CarOwnerManager ownerMgr = new CarOwnerManager();
            ownerMgr.Create(owner); // also in the database

            Car2 nyBil = new Car2("ZZ22334", "Blå", false, owner);
            Car2 c1 = mgr.Create(nyBil);
            Console.WriteLine(c1);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // modify
            Console.WriteLine("Ændret bil");
            nyBil.Colour = "Meget sort";
            Car2 c2 = mgr.Modify(nyBil.RegNr, nyBil);
            Console.WriteLine(c2);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // slet bil
            Console.WriteLine("Slettet bil");
            Car2 c3 = mgr.Delete(nyBil.RegNr);
            Console.WriteLine(c3);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // sletter lige owner 
            ownerMgr.Delete(owner.Phone);
        }
    }
}