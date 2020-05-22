using System.Collections.Generic;
using System.Text;
using lab3;
using lab6;
namespace lab5

{
    class Teacher : Human, IInfo
    {
        private List<List<string>> schedule = new List<List<string>>();
        private string subject;
        public Teacher()
        {

        }
        public Teacher(List<List<string>> schedule, string name, string subject, int age, Human.GenderEnum gender)
        {
            this.schedule = schedule;
            this.Name = name;
            this.subject = subject;
            this.Age = age;
            this.Gender = gender;
        }
        public List<List<string>> Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Info()
        {
            StringBuilder info = new StringBuilder();
            info.Append("Name: " + Name);
            info.Append(". Age: " + Age);

            info.Append(". Password: " + Password);
            info.Append(". Subject:" + subject);
            
            return info.ToString();
        }
    }

}
