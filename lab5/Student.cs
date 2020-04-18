using System.Collections.Generic;
using lab3;
namespace lab5

{
    class Student : Human
    {
        private List<List<string>> schedule = new List<List<string>>();
        private string studentId;
        private int group;

        public Student()
        {

        }
        public Student(List<List<string>> schedule, string name)
        {
            this.schedule = schedule;
            this.Name = name;
        }
        public Student(List<List<string>> schedule, string name, int age, Human.GenderEnum gender, int group, string studentId)
        {
            this.schedule = schedule;
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
            this.group = group;
            this.studentId = studentId;
        }
        public List<List<string>> Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
        public List<string> GetScheduleByDay(int day)
        {
            string[] daysOfTheWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            return schedule[day - 1];
        }
        public string StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }
        public int Group
        {
            get { return group; }
            set { group = value; }
        }
    }

}
