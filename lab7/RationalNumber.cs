using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace lab7
{
    class RationalNumber : IEquatable<RationalNumber>, IComparable
    {
        readonly int m;
        readonly int n;
        public RationalNumber(int n, int m)
        {
            if (m <= 0)
            {
                throw new ArgumentException(m.ToString(), "Must be over 0");
            }

            this.m = m;
            this.n = n;
        }
        private static int NOD(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                    a = a - b;
                else
                    b = a - a;
            }
            return a;
        }
        public int CompareTo(object num)
        {
            RationalNumber number = (RationalNumber)num;
            if (number == null)
            {
                throw new ArgumentException("Not comparabble");
            }
            return this == number ? 0 : this > number ? 1 : -1;

        }
        bool IEquatable<RationalNumber>.Equals(RationalNumber num)
        {
            int denominator = this.n * num.n / NOD(this.m, num.m);
            int numeratorA = denominator / this.m * this.n;
            int numeratorB = denominator / num.m * num.n;
            if (numeratorA == numeratorB)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            int denominator = a.m * b.m / NOD(a.m, b.m);
            int numeratorA = denominator / a.m * a.n;
            int numeratorB = denominator / b.m * b.n;
            return numeratorA > numeratorB;
        }
        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            int denominator = a.m * b.m / NOD(a.m, b.m);
            int numeratorA = denominator / a.m * a.n;
            int numeratorB = denominator / b.m * b.n;
            return numeratorA < numeratorB;
        }
        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            int denominator = a.m * b.m / NOD(a.m, b.m);
            int numeratorA = denominator / a.m * a.n;
            int numeratorB = denominator / b.m * b.n;
            return numeratorA <= numeratorB;
        }
        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            int denominator = a.m * b.m / NOD(a.m, b.m);
            int numeratorA = denominator / a.m * a.n;
            int numeratorB = denominator / b.m * b.n;
            return numeratorA >= numeratorB;
        }
        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            int denominator = a.m * b.m / NOD(a.m, b.m);
            int numeratorA = denominator / a.m * a.n;
            int numeratorB = denominator / b.m * b.n;
            return new RationalNumber(numeratorA + numeratorB, denominator);
        }
        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            int denominator = a.m * b.m / NOD(a.m, b.m);
            int numeratorA = denominator / a.m * a.n;
            int numeratorB = denominator / b.m * b.n;
            return new RationalNumber(numeratorA - numeratorB, denominator);
        }
        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {

            return new RationalNumber(a.n * b.n, a.m * b.m);
        }
        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {

            return new RationalNumber(a.n * b.m, a.m * b.n);
        }
        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(RationalNumber a, RationalNumber b)
        {

            return !a.Equals(b);
        }
        public static explicit operator double(RationalNumber num)
        {
            return (double)num.n / (double)num.m;
        }
        public static implicit operator RationalNumber(int num)
        {
            return new RationalNumber(num, 1);
        }
        // may lose data
        public static explicit operator int(RationalNumber num)
        {
            return num.n / num.m;
        }
        /*
        public static implicit operator RationalNumber(string num)
        {

            if (Regex.IsMatch(num, @"\d+\s?/|:\s?\d+"))
            {
                List<int> numberList = new List<int>();

                var numbers = Regex.Matches(num, @"\d+");
                foreach (Match match in numbers)
                {

                    numberList.Add(int.Parse(match.Value));
                }
                return new RationalNumber((int)numberList[0], (int)numberList[1]);
            }
            throw new ArgumentException("No matching regex expressions");

        }
        */
        public static RationalNumber Parse(string num)
        {

            if (Regex.IsMatch(num, @"\d+\s?/|:\s?\d+"))
            {
                List<int> numberList = new List<int>();

                var numbers = Regex.Matches(num, @"\d+");
                foreach (Match match in numbers)
                {

                    numberList.Add(int.Parse(match.Value));
                }
                return new RationalNumber((int)numberList[0], (int)numberList[1]);
            }
            throw new ArgumentException("No matching regex expressions");
        }
        // r- underscore, l - /
        public string Print(char option)
        {
            if (option == 'l')
            {
                return n.ToString() + "/" + m.ToString();
            }
            else if (option == 'r')
            {
                return ("\x1B[4m" + n.ToString() + "\x1B[0m" + "\n" + m.ToString());
            }
            throw new ArgumentException(option.ToString(), " is not an option");

        }
    }
}
