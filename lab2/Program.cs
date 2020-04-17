using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace lab2
{
    class Program
    {
        private static void PrintDate(string cultureCode = "fr-FR")
        {
            DateTime localDate = DateTime.Now;
            CultureInfo ci = new CultureInfo(cultureCode);
            Console.Write(localDate.ToString("f", ci));
        }

        // Date in different languages
        private static void Assignment1()
        {
            List<string> cultureCodes = new List<string>();
            //(CultureInfo.GetCultures(CultureTypes.AllCultures)).ForEach(cui => cultureCodes.Add(cui.ToString()));

            foreach (CultureInfo cui in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                cultureCodes.Add(cui.ToString());
            }

            Console.Write("Culture code([N] to use french): ");
            string userCode = Console.ReadLine();

            if (userCode == "N" || userCode == "n")
            {

                PrintDate();
                return;
            }
            if (cultureCodes.Contains(userCode))
            {
                PrintDate(userCode);
                return;
            }
            Console.WriteLine("Culture code does not exist.");
            
        }
        //random string < 4 chars
        static void Assignment2() {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            Random randomInt = new Random();
            char[] stringChars = new char[randomInt.Next(1,4)];


            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[randomInt.Next(chars.Length)];
            }

            string finalString = new String(stringChars);
            Console.WriteLine(finalString);

        }
        //30 chars from string randomly
        static void Assignment3()
        {
            string charSet = "qwertyuiopasdfghjklzxcvbnm";
            Random randInt = new Random();
            char[] charsArr = new char[256];
            for (int i = 0; i < charsArr.Length; i++)
            {
                charsArr[i] = charSet[randInt.Next(0,charSet.Length)];
            }
            string charStr = new string(charsArr);
            Console.WriteLine("Original string: " + charStr);
            char[] randomArray = charsArr.OrderBy(x => randInt.Next()).ToArray();
            Console.WriteLine("Random string: ");
            for (int i = 0; i < 30; i++)
            {
                Console.Write(randomArray[i] + " ");
            }
        }
        static void Main(string[] args)
        {
           // Assignment1();
            Assignment2(); 
            //Assignment3();
        }
    
    }
}
