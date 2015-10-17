using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Daily_204_Intermediate_CS
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(IncreaseNumber("10"));
            Console.WriteLine(IncreaseNumber("11"));
            Console.WriteLine(IncreaseNumber("12"));
            Console.WriteLine(IncreaseNumber("22"));
            bool isNumeric = false;

            int inputNumber = 0;
            while (!isNumeric)
            {
                Console.Write("Please input a number: ");
                string input = Console.ReadLine();
                isNumeric = (new Regex("^[0-9]*$").IsMatch(input) && (input.Length > 0));
                if (isNumeric)
                    inputNumber = int.Parse(input);
            }
            if (inputNumber == 0)
                Console.WriteLine("sorry no match");
            else if (inputNumber == 1)
            {
                Console.WriteLine("0");
                Console.WriteLine("1");
                Console.WriteLine("2");
            }
            else
            {
                // find max digits
                bool maxFound = false;
                string maxNumber = "1";
                while (!maxFound)
                {
                    if (Decompile(maxNumber) > inputNumber)
                    {
                        maxFound = true;
                    }
                    else
                    {
                        maxNumber += "0";
                    }
                }

                // try everything until max is reached
                var matches = new List<string>();
                string tryString = "1";

                while (tryString.Length < maxNumber.Length)
                {
                    if (Decompile(tryString).Equals(inputNumber))
                        matches.Add(tryString);
                    tryString = IncreaseNumber(tryString);
                }
                matches.ForEach(m => Console.WriteLine(m));
            }
            Console.ReadLine();
        }

        static int Decompile(string input)
        {
            double result = 0;
            var reversed = input.Reverse<char>().ToList<char>();
            for (int i = 0; i < input.Length; i++)
            {
                result += double.Parse(reversed[i].ToString()) * Math.Pow(2, i);
            }
            return Convert.ToInt32(result);
        }

        static string IncreaseNumber(string oldNumber)
        {
            int currPosition = oldNumber.Length - 1;
            while (!currPosition.Equals(-1))
            {
                if (oldNumber[currPosition].Equals('0'))
                {
                    return ReplaceWithCharAndMakeZeroBehind(oldNumber, currPosition, '1');
                }
                else if (oldNumber[currPosition].Equals('1'))
                {
                    return ReplaceWithCharAndMakeZeroBehind(oldNumber, currPosition, '2');
                }
                else { currPosition--; }
            }
            return ReplaceWithCharAndMakeZeroBehind(oldNumber, 0, '1') + "0";
        }

        static string ReplaceWithCharAndMakeZeroBehind(string input, int position, char replacementChar)
        {
            StringBuilder sb = new StringBuilder(input);
            sb[position] = replacementChar;
            for (int i = position + 1; i < input.Length; i++)
            { sb[i] = '0'; }
            return sb.ToString();

        }
    }
}
