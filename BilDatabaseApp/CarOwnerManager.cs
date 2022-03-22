using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilLib.model;

namespace BilDatabaseApp
{
    public class CarOwnerManager : IService<CarOwner>
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<CarOwner> GetAll()
        {
            List<CarOwner> liste = new List<CarOwner>();
            String sql = "select * from BilEjer";

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
                    CarOwner owner = ReadCarOwner(reader);
                    liste.Add(owner);
                }
            }

            return liste;

        }

        public CarOwner GetByString(string txt)
        {
            String sql = "select * from BilEjer where Phone=@Phone";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);
                // indsæt værdierne
                cmd.Parameters.AddWithValue("@Phone", txt);

                // altid ved select
                SqlDataReader reader = cmd.ExecuteReader();

                // læser alle rækker
                while (reader.Read())
                {
                    CarOwner owner = ReadCarOwner(reader);
                    return owner;
                }
            }

            return null; // eller throw new KeyNotFoundException();

        }

        public CarOwner Create(CarOwner newItem)
        {
            String sql = "insert into BilEjer values(@Phone, @Name)";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                // indsæt værdierne
                cmd.Parameters.AddWithValue("@Phone", newItem.Phone);
                cmd.Parameters.AddWithValue("@Name", newItem.Name);
                


                // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return newItem;
            }

        }

        public CarOwner Delete(string txt)
        {
            CarOwner carOwner = GetByString(txt);

            String sql = "delete from BilEjer where Phone=@Phone";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                // indsæt værdierne
                cmd.Parameters.AddWithValue("@Phone", txt);


                // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return carOwner;
            }
        }

        public CarOwner Modify(string txt, CarOwner modifiedItem)
        {
            String sql = "update bilEjer set Phone=@Phone, Name=@Name where Phone=@UpdateOwner";

            // opret forbindelse
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // åbner forbindelsen
                connection.Open();

                // opretter sql query
                SqlCommand cmd = new SqlCommand(sql, connection);

                // indsæt værdierne
                cmd.Parameters.AddWithValue("@Phone", modifiedItem.Phone);
                cmd.Parameters.AddWithValue("@Name", modifiedItem.Name);
                cmd.Parameters.AddWithValue("@UpdateOwner", txt);


                // altid ved Insert, update, delete
                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    // fejl
                }

                return modifiedItem;
            }

        }



        private CarOwner ReadCarOwner(SqlDataReader reader)
        {
            CarOwner carowner = new CarOwner();

            carowner.Phone = reader.GetString(0);
            carowner.Name = reader.GetString(1);

            return carowner;
        }
    }
}
