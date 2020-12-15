using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessLayer;
using System.Data.SqlClient;
namespace Models
{
    public class Production : IReadableProduction
    {
        int Quantity { get; set; }
        string Name { get; set; }
        public Production(string connectionString, int pid)
        {
            GetProductionProductInventory(connectionString, pid);
            GetProductionProduct(connectionString, pid);
        }
        public Production() { }
        public void GetProductionProductInventory(string connectionString, int pid)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand commands = new SqlCommand("GetProductInventoryByID", connection); // GetNameByBEIDF, GetPhoneByBEIDF
            commands.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter pidParam = new SqlParameter
            {
                ParameterName = "@pid",
                Value = pid
            };

            commands.Parameters.Add(pidParam);
            var reader = commands.ExecuteReader();
            while (reader.Read())
            {
                this.Quantity = reader.GetInt32(0);
            }
        }
        public void GetProductionProduct(string connectionString, int pid)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand commands = new SqlCommand("GetProductNameByID", connection); // GetNameByBEIDF, GetPhoneByBEIDF
            commands.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter pidParam = new SqlParameter
            {
                ParameterName = "@pid",
                Value = pid
            };

            commands.Parameters.Add(pidParam);
            var reader = commands.ExecuteReader();
            while (reader.Read())
            {
                this.Name = reader.GetString(0);
            }
        }
    }
}
