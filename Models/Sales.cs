using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessLayer;
using System.Data.SqlClient;
namespace Models
{
    public class Sales : IReadableSales
    {
        public string CreditCardNumber { get; set; }
        public Sales() { }
        public Sales(string connectionString, int id)
        {
            GetCreditCard(connectionString, id);
        }
        public void GetCreditCard(string connectionString, int id)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand commands = new SqlCommand("GetCreditCardByID", connection); // GetNameByBEIDF, GetPhoneByBEIDF
            commands.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };

            commands.Parameters.Add(idParam);
            var reader = commands.ExecuteReader();
            while (reader.Read())
            {
                this.CreditCardNumber = reader.GetString(0);
            }
        }
    }
}
