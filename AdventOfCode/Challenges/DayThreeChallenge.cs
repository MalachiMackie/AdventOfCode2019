using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Challenges
{
    public static class DayThreeChallenge
    {
        private static string[][] GetInput()
        {
            string[][] output = new string[2][];
            string[] text = File.ReadAllLines("input/dayThree.txt");
            for (int i = 0; i < 2; i++)
            {
                output[i] = text[i].Split(',');
            }
            return output;
        }

        private static List<(int X, int Y)> ProcessWire(string[] wire)
        {
            var currentPos = (X: 0, Y: 0);
            var wirePositions = new List<(int X, int Y)>();

            foreach (string dir in wire)
            {
                int length = int.Parse(dir.Substring(1));
                int xChange = 0;
                int yChange = 0;
                switch(dir[0])
                {
                    case 'R':
                        {
                            xChange = 1;
                            break;
                        }
                    case 'U':
                        {
                            yChange = 1;
                            break;
                        }
                    case 'L':
                        {
                            xChange = -1;
                            break;
                        }
                    case 'D':
                        {
                            yChange = -1;
                            break;
                        }
                }
                for(int i = 0; i < length; i++)
                {
                    currentPos.X += xChange;
                    currentPos.Y += yChange;
                    wirePositions.Add(currentPos);
                }
            }
            return wirePositions;
        }

        private static int CalculateDistance((int X, int Y) pos)
        {
            return Math.Abs(0 - pos.X) + Math.Abs(0 - pos.Y);
        }

        public static void RunChallenge1()
        {
            string[][] input = GetInput();
            var wire1 = new HashSet<(int X, int Y)>(ProcessWire(input[0]));
            var wire2 = new HashSet<(int X, int Y)>(ProcessWire(input[1]));

            var intersections = wire1.Where(x => wire2.Contains(x)).ToList();

            Console.WriteLine(string.Join('\n', intersections.Select(x => $"x: {x.X}, y: {x.Y} = {CalculateDistance((x.X, x.Y))}")));
            Console.WriteLine($"Minimum: {intersections.Select(x => CalculateDistance((x.X, x.Y))).Min()}");

        }

        public static void RunChallenge2()
        {
            string[][] input = GetInput();

            var wire1 = ProcessWire(input[0]);
            var wire2 = ProcessWire(input[1]);

            var hash1 = new HashSet<(int X, int Y)>(wire1);
            var hash2 = new HashSet<(int X, int Y)>(wire2);

            var intersections = hash1.Where(x => hash2.Contains(x)).ToList();
            int distance = int.MaxValue;

            foreach((int X, int Y) intersect in intersections)
            {
                for(int i = 0; i < wire1.Count; i++)
                {
                    var wire1Pos = wire1[i];
                    if(intersect == wire1Pos)
                    {
                        var intersectDist = (i + 1) + wire2.IndexOf(wire1Pos) + 1;
                        if(intersectDist < distance)
                        {
                            distance = intersectDist;
                        }
                    }
                }
            }

            Console.WriteLine(distance);
        }
    }
}
