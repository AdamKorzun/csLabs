using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FileModel
    {
        public Person person { get; set; }
        public Production production{ get; set; }
        public Sales sales { get; set; }
        public FileModel() { }
        public FileModel(Person person, Production production, Sales sales) 
        {
            this.person = person;
            this.production = production;
            this.sales = sales;
        }
    }
}
