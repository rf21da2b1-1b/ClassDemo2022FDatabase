using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using BilLib.model;

namespace BilDatabaseApp
{
    public class AWorker : IBilService
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public AWorker()
        {

        }

        public void Start()
        {
            Console.WriteLine("Alle biler");
            List<Car> cars = GetAll();
            foreach (Car c in cars)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("Bil med reg nummer DD22334");
            Console.WriteLine( GetByRegNr("DD22334"));

            

            // opret bil
            Console.WriteLine("Oprettet bil");
            Car nyBil = new Car("ZZ22334", "Blå", false, "11223344");
            Car c1 = Create(nyBil);
            Console.WriteLine(c1);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // modify
            Console.WriteLine("Ændret bil");
            nyBil.Colour = "Meget sort";
            Car c2 = Modify(nyBil.RegNr, nyBil);
            Console.WriteLine(c2);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();

            // slet bil
            Console.WriteLine("Slettet bil");
            Car c3 = Delete(nyBil.RegNr);
            Console.WriteLine(c3);
            Console.Write("Tryk return for at fortsætte ");
            Console.ReadLine();



        }


        public List<Car> GetAll()
        {
            List<Car> liste = new List<Car>();
            String sql = "select * from Car";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                                        // altid ved select
                SqlDataReader reader = cmd.ExecuteReader();

                // læser alle rækker
                while (reader.Read())
                {
                    Car car = ReadCar(reader);
                    liste.Add(car);
                }
            }

            return liste;
        }

        private Car ReadCar(SqlDataReader reader)
        {
            Car car = new Car();

            car.RegNr = reader.GetString(0);
            car.Colour = reader.GetString(1);
            car.El = reader.GetBoolean(2);
            car.Ejer = reader.GetString(3);


            return car;
        }

        public Car GetByRegNr(string regNr)
        {
            String sql = "select * from Car where RegNr=@RegNr";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);
                // indsæt værdierne
                cmd.Parameters.AddWithValue("@RegNr", regNr);

                // altid ved select
                SqlDataReader reader = cmd.ExecuteReader();

                // læser alle rækker
                while (reader.Read())
                {
                    Car car = ReadCar(reader);
                    return car;
                }
            }

            return null; // eller throw new KeyNotFoundException();
        }

        public Car Create(Car newCar)
        {
            String sql = "insert into Car values(@RegNr, @Colour, @El, @Ejer)";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                // indsæt værdierne
                cmd.Parameters.AddWithValue("@RegNr", newCar.RegNr);
                cmd.Parameters.AddWithValue("@Colour", newCar.Colour);
                cmd.Parameters.AddWithValue("@El", newCar.El);
                cmd.Parameters.AddWithValue("@Ejer", newCar.Ejer);


                           // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return newCar;
            }

        }

        public Car Delete(string regNr)
        {
            Car deletedCar = GetByRegNr(regNr);

            String sql = "delete from Car where RegNr=@RegNr";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                // indsæt værdierne
                cmd.Parameters.AddWithValue("@RegNr", regNr);
     

                // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return deletedCar;
            }
        }

        public Car Modify(string regNr, Car modifiedCar)
        {
            String sql = "update Car set RegNr=@RegNr, Colour=@Colour, El=@El, Ejer=@Ejer where RegNr=@UpdateRegNr";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                // indsæt værdierne
                cmd.Parameters.AddWithValue("@RegNr", modifiedCar.RegNr);
                cmd.Parameters.AddWithValue("@Colour", modifiedCar.Colour);
                cmd.Parameters.AddWithValue("@El", modifiedCar.El);
                cmd.Parameters.AddWithValue("@Ejer", modifiedCar.Ejer);
                cmd.Parameters.AddWithValue("@UpdateRegNr", regNr);


                // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return modifiedCar;
            }


        }
    }
}