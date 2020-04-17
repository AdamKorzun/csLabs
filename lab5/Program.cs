using System;
using System.Collections.Generic;
using System.IO;
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
        public Student(List<List<string>> schedule, string name, int age, int gender, int group, string studentId)
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
    class Teacher : Human
    {
        private List<List<string>> schedule = new List<List<string>>();
        private string subject;
        public Teacher()
        {

        }
        public Teacher(List<List<string>> schedule, string name, string subject, int age, int gender)
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
    }
    class Rector : Human
    {
        private List<List<string>> schedule = new List<List<string>>();
        public Rector()
        {

        }
        public Rector(List<List<string>> schedule, string name, int age, int gender)
        {
            this.schedule = schedule;
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }
        public List<List<string>> Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
    }
    class Program
    {
        static List<List<string>> GetScheduleFromFile(string name)
        {
            try
            {
                using (StreamReader sr = new StreamReader("C:\\Users\\admin\\Desktop\\C#\\lab2\\lab5\\files\\" + name + ".txt"))
                {
                    sr.ReadLine();
                    List<List<string>> schedule = new List<List<string>>();
                    for (int i = 0; (sr.Peek() >= 0); i++)
                    {
                        string line = sr.ReadLine();
                        List<string> temp = new List<string>();
                        temp.Clear();

                        while ((line != "Monday" && line != "Tuesday" && line != "Wednesday" && line != "Thursday" && line != "Friday" && line != "Saturday" && line != "Sunday")
                            && sr.Peek() >= 0)
                        {

                            temp.Add(line);
                            line = sr.ReadLine();
                        }

                        schedule.Add(temp);


                    }
                    return schedule;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new List<List<string>>();
            }


        }
        static void PrintSchedule(List<List<string>> schedule, int day = -1)
        {
            string[] daysOfTheWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (day != -1)
            {
                Console.WriteLine(daysOfTheWeek[day - 1]);
                foreach (string lesson in schedule[day - 1])
                {
                    Console.WriteLine(lesson);

                }
                return;
            }
            int i = 0;
            foreach (List<string> element in schedule)
            {
                Console.WriteLine(daysOfTheWeek[i]);
                foreach (string lesson in element)
                {
                    Console.WriteLine(lesson);

                }
                Console.WriteLine();
                i++;

            }
        }
        static void PrintMenu()
        {
            Console.WriteLine("1 - Show teachers schedule");
            Console.WriteLine("2 - Show students schedule");
            Console.WriteLine("3 - Show rectors schedule");
            Console.WriteLine("4 - Show students info");
            Console.WriteLine("5 - Show teachers info");
            Console.WriteLine("6 - Show rectors info");
            Console.WriteLine("7 - Exit");
            Console.WriteLine("Option: ");
        }
        static void PrintInfo(Student student)
        {
            Console.WriteLine(student.Name);
            Console.WriteLine(student.Age);
            Console.WriteLine((Human.GenderEnum)student.Gender);
            Console.WriteLine(student.StudentId);
            Console.WriteLine(student.Group);
        }
        static void PrintInfo(Teacher teacher)
        {
            Console.WriteLine(teacher.Name);
            Console.WriteLine(teacher.Age);
            Console.WriteLine((Human.GenderEnum)teacher.Gender);
            Console.WriteLine(teacher.Subject);
        }
        static void PrintInfo(Rector rector)
        {
            Console.WriteLine(rector.Name);
            Console.WriteLine(rector.Age);
            Console.WriteLine((Human.GenderEnum)rector.Gender);
           
        }
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            List<Rector> rectors = new List<Rector>();

            students.Add(new Student(GetScheduleFromFile("Adam"), "Adam",18, (int)Human.GenderEnum.Male, 953505, "studentid"));
            students.Add(new Student(GetScheduleFromFile("Student2"), "Student2", 21, (int)Human.GenderEnum.Male, 953505, "studentid"));
            teachers.Add(new Teacher(GetScheduleFromFile("Teacher"), "Teacher", "Math", 30, (int)Human.GenderEnum.Male));
            rectors.Add(new Rector(GetScheduleFromFile("Rector"), "Rector", 40, (int)Human.GenderEnum.Female));
            //Teacher teacher = new Teacher(GetScheduleFromFile("Teacher"), "Teacher", "Math");
            //teacher.Gender = (int)Human.GenderEnum.Male;
            
            //PrintSchedule(schedule);
            //PrintSchedule(GetScheduleFromFile(teacher.Name));
            while (true)
            {
                PrintMenu();
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.D3)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    PrintSchedule(GetScheduleFromFile(name));

                    
                    //parts.Find(x => x.PartName.Contains("seat"))); ;
                }
                else if(key.Key == ConsoleKey.D4)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    PrintInfo(students.Find(x => x.Name.Contains(name)));

                }
                else if (key.Key == ConsoleKey.D5)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    PrintInfo(teachers.Find(x => x.Name.Contains(name)));

                }
                else if (key.Key == ConsoleKey.D6)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    PrintInfo(rectors.Find(x => x.Name.Contains(name)));

                }
                else if (key.Key == ConsoleKey.D7)
                {
                    return;
                }
                Console.ReadKey();
                Console.Clear();
            }

        }
    }

}
