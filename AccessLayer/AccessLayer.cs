using System;
using System.Data.SqlClient;

namespace AccessLayer
{
    public class AccessLayer
    {
        
      
    }
    public interface IReadableSales
    {
        void GetCreditCard(string connectionString, int id);
    }
    public interface IReadablePerson
    {
        void GetPersonName(string connectionString, int beid);
        void GetPersonPhone(string connectionString, int beid);
    }
    public interface IReadableProduction
    {
        void GetProductionProduct(string connectionString, int pid);
        void GetProductionProductInventory(string connectionString, int pid);
    }
}
