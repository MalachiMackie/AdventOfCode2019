using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Challenges
{
    public static class DayFourChallenge
    {
        private static bool MeetsCriteria(int password)
        {
            var digits = new int[6];
            for(int i = 0; i < 6; i++)
            {
                char myChar = password.ToString()[i];
                digits[i] = int.Parse($"{myChar}");
            }

            var digitsFound = new Dictionary<int, int>();
            int previousDigit = 0;
            for (int i = 0; i < 6; i++)
            {
                int digit = digits[i];
                if(i != 0)
                {
                    if(digit < previousDigit)
                    {
                        return false;
                    }
                    if(digit == previousDigit)
                    {
                        if(digitsFound.ContainsKey(digit))
                        {
                            var num = digitsFound[digit];
                            digitsFound.Remove(digit);
                            digitsFound.Add(digit, num + 1);
                        }
                        else
                        {
                            digitsFound.Add(digit, 2);
                        }
                    }
                }

                previousDigit = digit;
            }
            return digitsFound.Any(x => x.Value == 2);
        }

        public static void RunChallenge()
        {
            var valid = new List<int>();
            for(int i = 128392; i <= 643281; i++)
            {
                if(MeetsCriteria(i))
                {
                    valid.Add(i);
                }
            }
            Console.WriteLine(valid.Count.ToString());
        }
    }
}
