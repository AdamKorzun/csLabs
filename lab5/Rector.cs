using System.Collections.Generic;
using System.Text;
using lab3;
using lab6;
namespace lab5

{
    class Rector : Human,IInfo
    {
        private List<List<string>> schedule = new List<List<string>>();
        public Rector()
        {

        }
        public Rector(List<List<string>> schedule, string name, int age, Human.GenderEnum gender)
        {
            this.schedule = schedule;
            this.Name = name;
            this.Age = age;
            this.gender = gender;
        }
        public List<List<string>> Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
        public string Info() {
            StringBuilder info = new StringBuilder();
            info.Append("Name: " + Name);
            info.Append(". Age: " + Age);
            
            info.Append(". Password: " + Password);
            return info.ToString();
        }
    }

}
