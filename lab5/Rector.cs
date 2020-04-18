using System.Collections.Generic;
using lab3;
namespace lab5

{
    class Rector : Human
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
    }

}
