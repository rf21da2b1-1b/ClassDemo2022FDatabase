using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilLib.model;

namespace BilDatabaseApp
{
    public class Car2Manager2 : IService<Car2>
    {
        private const string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly CarOwnerManager ownerMgr = new CarOwnerManager();

        public List<Car2> GetAll()
        {
            List<Car2> liste = new List<Car2>();
            String sql = "select * from Car c join BilEjer o on c.Ejer = o.Phone";

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
                    Car2 car = ReadCar(reader);
                    liste.Add(car);
                }
            }

            return liste;
        }

        public Car2 GetByString(string txt)
        {
            String sql = "select * from Car c join BilEjer o on c.Ejer = o.Phone where RegNr=@RegNr";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);
                // indsæt værdierne
                cmd.Parameters.AddWithValue("@RegNr", txt);

                // altid ved select
                SqlDataReader reader = cmd.ExecuteReader();

                // læser alle rækker
                while (reader.Read())
                {
                    Car2 car = ReadCar(reader);
                    return car;
                }
            }

            return null; // eller throw new KeyNotFoundException();
        }

        public Car2 Create(Car2 newItem)
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
                cmd.Parameters.AddWithValue("@RegNr", newItem.RegNr);
                cmd.Parameters.AddWithValue("@Colour", newItem.Colour);
                cmd.Parameters.AddWithValue("@El", newItem.El);
                // kun telefon fra ejer 
                cmd.Parameters.AddWithValue("@Ejer", newItem.Ejer.Phone);


                // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return newItem;
            }
        }

        public Car2 Delete(string regNr)
        {
            Car2 deletedCar = GetByString(regNr);

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

        public Car2 Modify(string regNr, Car2 modifiedCar)
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
                cmd.Parameters.AddWithValue("@Ejer", modifiedCar.Ejer.Phone);
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
        private Car2 ReadCar(SqlDataReader reader)
        {
            Car2 car = new Car2();

            car.RegNr = reader.GetString(0);
            car.Colour = reader.GetString(1);
            car.El = reader.GetBoolean(2);

            CarOwner owner = new CarOwner();
            owner.Phone = reader.GetString(4);
            owner.Name = reader.GetString(5);
            car.Ejer = owner;

            return car;
        }
    }
}
