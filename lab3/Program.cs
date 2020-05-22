using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using lab6;
namespace lab3
{
    public class Human : IInfo, IEquatable<Object>
    {
        private static bool exists = false;
        private string name;
        private int age;
        protected Human.GenderEnum gender;
        //private string gender;
        private string password;

        public enum GenderEnum   
        {
            Male = 1,
            Female = 2
        }
        public Human.GenderEnum Gender
        {
            set { gender = value; }
            get { return gender; }
        }
        public Human()
        {
            exists = true;
        }
        public Human(string name, int age, string gender, string password)
        {
            this.name = name;
            this.age = age;
            //this.gender = gender;
            this.password = password;
            exists = true;
        }
        // getters / setters;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value > 0)
                {
                    age = value;
                }
            }
        }
        
        
        
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public bool CanBeStudent()
        {
            if (exists)
            {
                return (age > 16);
            }
            return false;


        }
        public bool IsOlder(int averageAge)
        {
            return (age > averageAge);
        }
        public string Info()
        {
            StringBuilder info = new StringBuilder();
            info.Append("Name: " + name);
            info.Append(". Age: " + age);
            //info.Append(". Gender: " + gender);
            info.Append(". Password: " + password);
            return info.ToString();
        }
        bool Equals(Object x)
        {
            return Equals(x);
        }

    }
    class Program
    {
        static void AddPerson(List<Human> personDb)
        {

            personDb.Add(new Human());
            Human person = personDb.Last();
            Console.Write("Name: ");
            person.Name = Console.ReadLine();
            Console.Write("Age: ");
            person.Age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Gender: ");
            //person.Gender = Console.ReadLine();
            Console.Write("Password: ");
            person.Password = Console.ReadLine();
            /*
            byte[] bytePass = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            var hashedPassword = sha1.ComputeHash(bytePass);
            var hp = Encoding.ASCII.GetString(hashedPassword);
            Console.WriteLine(hp);
            */
            //personDb.Add(new Human(name, age, gender, password));

        }
        static void SignInAccout(List<Human> personDb)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            foreach (var person in personDb)
            {
                if (person.Name == name && person.Password == password)
                {
                    Console.WriteLine(person.Info());
                }

            }
        }
        static void PrintMenu()
        {
            Console.WriteLine("1 - add new accout");
            Console.WriteLine("2 - sign in");
            Console.WriteLine("3 - can be student?");
            Console.WriteLine("4 - is older?");
            Console.WriteLine("5 - exit");
            Console.Write("Option: ");
        }

        static void Main(string[] args)
        {
            List<Human> personDb = new List<Human>();
            while (true)
            {
                PrintMenu();
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.D1)
                {

                    AddPerson(personDb);
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    SignInAccout(personDb);
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine();
                    foreach (var person in personDb)
                    {
                        if (person.Name == name)
                        {
                            Console.WriteLine("Can be student: " + person.CanBeStudent());
                        }
                    }
                }
                else if (key.Key == ConsoleKey.D4)
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine();
                    int averageAge = 0;
                    foreach (var person in personDb)
                    {
                        averageAge += person.Age;
                    }
                    averageAge /= personDb.Count();

                    foreach (var person in personDb)
                    {
                        if (person.Name == name)
                        {
                            Console.WriteLine("Is older than average: " + person.IsOlder(averageAge));
                        }
                    }
                }
                else if (key.Key == ConsoleKey.D5)
                {
                    break;
                }
                key = Console.ReadKey();
                Console.Clear();
            }

        }

    }
}
