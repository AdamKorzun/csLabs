using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using lab3;
using lab8;
namespace lab5

{
    class Program
    {
        //delegate void AddLog(string data);
        //event AddLog NewLog;
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

                        while ((line != "Monday" && line != "Tuesday" && line != "Wednesday"
                            && line != "Thursday" && line != "Friday" && line != "Saturday" && line != "Sunday")
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
            Console.WriteLine("7 - Show schedule by day");
            Console.WriteLine("8 - Exit");
            Console.WriteLine("9 - Last log");
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
            Console.WriteLine(rector.Gender);

        }
        static List<Student> DeleteDuplicates(List<Student> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Equals(list[j]))
                    {
                        Console.WriteLine("Deleted" + list[j].Info());
                        list.RemoveAt(j);
                        j--;
                    }
                }
            }
            return list;
        }
        static List<Teacher> DeleteDuplicates(List<Teacher> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (!Human.Equals(list[i], list[j]))
                    {
                        list.RemoveAt(j);
                        j--;
                    }
                }
            }
            return list;
        }
        static List<Rector> DeleteDuplicates(List<Rector> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {

                    if (!Human.Equals(list[i], list[j]))
                    {

                        list.RemoveAt(j);
                        j--;
                    }
                }
            }
            return list;
        }
        static void Main(string[] args)
        {
            // lab 8
            Log logs = new Log();
            logs.NewLog += delegate (string data) 
            {
                Console.WriteLine("Added new log");
                string fileName = "C:\\Users\\admin\\Desktop\\C#\\lab2\\logs.txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }
                if (File.Exists(fileName))
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
                    {
                        file.WriteLine(data);
                    }
                }


            };
            
            logs.GetLog += delegate ()
            {
                string fileName = "C:\\Users\\admin\\Desktop\\C#\\lab2\\logs.txt";

                using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
                {
                    try
                    {
                        return System.IO.File.ReadLines(fileName).Last();
                    }
                    catch (InvalidOperationException)
                    {
                        return "File is empty";
                    }
                    
                }

            };
            

            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            List<Rector> rectors = new List<Rector>();

            students.Add(new Student(GetScheduleFromFile("Adam"), "Adam", 18, Human.GenderEnum.Male, 953505, "studentid"));
            students.Add(new Student(GetScheduleFromFile("Adam"), "Adam", 18, Human.GenderEnum.Male, 953505, "studentid"));

            students.Add(new Student(GetScheduleFromFile("Student2"), "Student2", 21, Human.GenderEnum.Male, 953505, "studentid"));
            teachers.Add(new Teacher(GetScheduleFromFile("Teacher"), "Teacher", "Math", 30, Human.GenderEnum.Male));
            rectors.Add(new Rector(GetScheduleFromFile("Rector"), "Rector", 40, Human.GenderEnum.Female));
            DeleteDuplicates(students);
            DeleteDuplicates(teachers);
            DeleteDuplicates(rectors);
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
                }
                else if (key.Key == ConsoleKey.D4)
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
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Day: ");
                    int day = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(string.Join(Environment.NewLine, (students.Find(x => x.Name.Contains(name)).GetScheduleByDay(day)).ToArray()));
                }
                else if (key.Key == ConsoleKey.D8)
                {
                    return;
                }
                else if (key.Key == ConsoleKey.D9)
                {
                    Console.WriteLine(logs.GetLastLog());
                }
                else
                {
                    
                }
                // lab 8
                logs.Add($"pressed {key.KeyChar.ToString()}");
               
                Console.ReadKey();
                Console.Clear();
            }

        }
    }

}
