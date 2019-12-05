using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        private static List<(int X, int Y, int distance)> ProcessWire(string[] wire)
        {
            var currentPos = (X: 0, Y: 0, distance: 0);
            var wirePositions = new List<(int X, int Y, int distance)>();

            int counter = 0;
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
                    counter++;
                    currentPos.X += xChange;
                    currentPos.Y += yChange;
                    currentPos.distance = counter;
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
            var wire1 = new HashSet<(int X, int Y, int distance)>(ProcessWire(input[0]));
            var wire2 = new HashSet<(int X, int Y, int distance)>(ProcessWire(input[1]));

            var intersections = wire1.Where(x => wire2.Contains(x)).ToList();

            Console.WriteLine(string.Join('\n', intersections.Select(x => $"x: {x.X}, y: {x.Y} = {CalculateDistance((x.X, x.Y))}")));
            Console.WriteLine($"Minimum: {intersections.Select(x => CalculateDistance((x.X, x.Y))).Min()}");

        }

        public static async Task RunChallenge2()
        {
            string[][] input = GetInput();


            var wire1 = ProcessWire(input[0]);
            var wire2 = ProcessWire(input[0]);

            var hash1 = new HashSet<(int X, int Y, int distance)>(wire1);
            var hash2 = new HashSet<(int X, int Y, int distance)>(wire2);

            var intersections = hash1.Where(x => hash2.Contains(x)).ToList();

            var tasks = intersections.Select(x =>
            {
                return Task.Run(() =>
                {
                    foreach((int X, int Y, int distance) pos in wire1)
                    {
                        if(x.X == pos.X && x.Y == pos.Y)
                        {
                            return hash1.First(y => y.X == pos.X && y.Y == pos.Y).distance + hash2.First(y => y.X == pos.X && y.Y == pos.Y).distance;
                        }
                    }
                    return int.MaxValue;
                });
            }).ToList();

            await Task.WhenAll(tasks);

            int distance = int.MaxValue;

            foreach(Task<int> task in tasks)
            {
                int dist = await task;
                if(dist < distance && dist != 0)
                {
                    distance = dist;
                }
            }

            Console.WriteLine(distance);
        }
    }
}
