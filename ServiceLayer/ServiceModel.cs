using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ServiceModel
    {
        public string ConnectionString { get; set; }
        public ServiceModel(string connectionString)
        {
            ConnectionString = connectionString;
        } 
        public ServiceModel() { }
     
       
    }
}
