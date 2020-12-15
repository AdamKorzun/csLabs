using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LogAccessLayer
{

    public class LogAccess
    {
        public string ConnectionString = "Data Source=HP8440P;Initial Catalog=LogDB; Integrated Security=true;";
        public void AddError(Exception e, DateTime time)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            var commands = new SqlCommand("AddError", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            var errorTextParam = new SqlParameter
            {
                ParameterName = "@errorText",
                Value = e.Message
            };
            var timeParam = new SqlParameter
            {
                ParameterName = "@timest",
                Value = time.TimeOfDay
            };
            
            commands.Parameters.Add(errorTextParam);
            commands.Parameters.Add(timeParam);
            commands.ExecuteNonQuery();
            Console.WriteLine("true");

        }
    }
}
