using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessLayer;
using System.Data.SqlClient;
namespace Models
{
    public class Person : IReadablePerson
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public Person(string connectionString, int beid)
        {
            GetPersonName(connectionString, beid);
            GetPersonPhone(connectionString, beid);
        }
        public Person() { }
        public void GetPersonName(string connectionString, int beid) 
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand commands = new SqlCommand("GetNameByBEIDF", connection); // GetNameByBEIDF, GetPhoneByBEIDF
            commands.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter beidParam = new SqlParameter
            {
                ParameterName = "@beid",
                Value = beid
            };

            commands.Parameters.Add(beidParam);
            var reader = commands.ExecuteReader();
            while (reader.Read())
            {
                this.Name = reader.GetString(0) + reader.GetString(1);
            }
        }
        public void GetPersonPhone(string connectionString, int beid)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand commands = new SqlCommand("GetPhoneByBEIDF", connection); // GetNameByBEIDF, GetPhoneByBEIDF
            commands.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter beidParam = new SqlParameter
            {
                ParameterName = "@beid",
                Value = beid
            };

            commands.Parameters.Add(beidParam);
            var reader = commands.ExecuteReader();
            while (reader.Read())
            {
                this.Phone = reader.GetString(0);
            }
            
        }

    }
}
