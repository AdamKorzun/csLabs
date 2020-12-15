using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3;
using ServiceLayer;
using System.Text.Json;
using System.IO;
using Models;
using XMLGenerator;
using LogAccessLayer;
namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonPath = @"C:\Users\admin\Desktop\c#sem3\labs\lab4\ServiceOptions.json";
            var manager = new ConfigManager(jsonPath);
            var serviceModel = manager.GetConfig<ServiceModel>();
            var connectionString = serviceModel.ConnectionString;
            int id = 1;
            var logger = new LogAccess();
            try
            {
                Person person = new Person(connectionString, id);
                Sales sales = new Sales(connectionString, id);
                Production production = new Production(connectionString, id);
                FileModel fm = new FileModel(person, null, null);
                XmlGenerator gen = new XmlGenerator();
                gen.ConvertToXmlFile(fm, @"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\SourceDirectory\file.xml");
            }
            catch (Exception  e){
                logger.AddError(e, DateTime.Now);
            }
            
            
            Console.Read();
           
        }
    }
}
