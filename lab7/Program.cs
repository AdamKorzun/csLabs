using System;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            
            RationalNumber num1 = "3 : 2";
            RationalNumber num2 = "4 / 2";
            Console.WriteLine(num1.Print('r')+"\n");
            Console.WriteLine(num2.Print('l') + "\n");
            Console.WriteLine((num1 + num2).Print('r') + "\n");
            Console.WriteLine(num2 == num1);
        }
    }
}
