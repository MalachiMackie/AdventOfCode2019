using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Challenges
{
    public static class DayTwoChallenge
    {
        private static int[] GetInput()
        {
            string text = File.ReadAllText("input/dayTwo.txt");
            return text.Split(',').Select(x => int.Parse(x)).ToArray();
        }

        private static int ProcessIntCode(int[] intCode, int noun, int verb)
        {
            intCode[1] = noun;
            intCode[2] = verb;
            for (int i = 0; i < intCode.Length; i += 4)
            {
                bool doBreak = false;
                switch (intCode[i])
                {
                    //Add
                    case 1:
                        {
                            int pos1 = intCode[i + 1];
                            int pos2 = intCode[i + 2];
                            int pos3 = intCode[i + 3];
                            intCode[pos3] = intCode[pos1] + intCode[pos2];
                            break;
                        }
                    //Multiply
                    case 2:
                        {
                            int pos1 = intCode[i + 1];
                            int pos2 = intCode[i + 2];
                            int pos3 = intCode[i + 3];
                            intCode[pos3] = intCode[pos1] * intCode[pos2];
                            break;
                        }
                    //Exit
                    case 99:
                        {
                            doBreak = true;
                            break;
                        }
                    default:
                        {
                            throw new InvalidOperationException($"{intCode[i]} is not an instruction");
                        }
                }
                if (doBreak)
                {
                    break;
                }
            }
            return intCode[0];
        }

        public static void RunChallenge1()
        {
            int[] intCode = GetInput();
            int output = ProcessIntCode(intCode, 12, 2);
            Console.WriteLine(output);
        }

        public static void RunChallenge2()
        {
            int[] intCode = GetInput();
            int noun = 0;
            int verb = 0;
            int[] clone = intCode.Clone() as int[];

            while (true)
            {
                if (ProcessIntCode(clone, noun, verb) == 19690720)
                {
                    break;
                }

                if (verb < 99)
                {
                    verb++;
                }
                else if (noun < 99)
                {
                    verb = 0;
                    noun++;
                }
                else
                {
                    throw new Exception("Did not find output 19690720");
                }

                clone = intCode.Clone() as int[];
            }
            Console.WriteLine($"100 * {noun} + {verb} = {100 * noun + verb}");
        }
    }
}
