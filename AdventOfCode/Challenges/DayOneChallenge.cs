using System;
using System.IO;

namespace AdventOfCode.Challenges
{
    public static class DayOneChallenge
    {
        public static void RunChallenge()
        {
            int sum = 0;

            foreach (string row in File.ReadAllLines("input/dayOne.txt"))
            {
                int mass = int.Parse(row);
                sum += CalculateFuelForMass(mass);
            }

            Console.WriteLine(sum);
        }

        private static int CalculateFuelForMass(int mass)
        {
            var output = mass / 3;
            output -= 2;

            if(output <= 0)
            {
                return 0;
            }

            output += CalculateFuelForMass(output);

            return output;
        }
    }
}
