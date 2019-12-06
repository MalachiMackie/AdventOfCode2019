using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Challenges
{
    public static class DayFiveChallenge
    {
        private enum ParameterMode
        {
            POSITION,
            IMMEDIATE
        }

        private static int[] GetInput()
        {
            string text = File.ReadAllText("input/dayFive.txt");
            return text.Split(',').Select(x => int.Parse(x)).ToArray();
        }

        private static void ProcessIntCode(int[] intCode)
        {
            int i = 0;

            while(i < intCode.Length)
            {
                var fullInstruction = intCode[i];
                var instructionString = fullInstruction.ToString();
                var digits = new int[instructionString.Length];
                for (int j = 0; j < digits.Length; j++)
                {
                    char myChar = instructionString[j];
                    digits[j] = int.Parse($"{myChar}");
                }

                int instruction = digits[^1];
                if(digits.Length > 1)
                {
                    var a = digits[^2];
                    instruction += (a * 10);
                }

                var modes = new List<ParameterMode>();

                for(int j = 3; j <= digits.Length; j++)
                {
                    var mode = digits[^j] == 0 ? ParameterMode.POSITION : ParameterMode.IMMEDIATE;
                    modes.Add(mode);
                }

                bool doBreak = false;
                switch (instruction)
                {
                    //Add
                    case 1:
                        {
                            int input1 = intCode[i + 1];
                            int input2 = intCode[i + 2];
                            int output = intCode[i + 3];

                            ParameterMode input1Mode = modes.Count >= 1 ? modes[0] : ParameterMode.POSITION;
                            ParameterMode input2Mode = modes.Count >= 2 ? modes[1] : ParameterMode.POSITION;

                            int inputValue1 = input1Mode == ParameterMode.IMMEDIATE ? input1 : intCode[input1];
                            int inputValue2 = input2Mode == ParameterMode.IMMEDIATE ? input2 : intCode[input2];

                            intCode[output] = inputValue1 + inputValue2;
                            i += 4;
                            break;
                        }
                    //Multiply
                    case 2:
                        {
                            int input1 = intCode[i + 1];
                            int input2 = intCode[i + 2];
                            int output = intCode[i + 3];

                            ParameterMode input1Mode = modes.Count >= 1 ? modes[0] : ParameterMode.POSITION;
                            ParameterMode input2Mode = modes.Count >= 2 ? modes[1] : ParameterMode.POSITION;

                            int inputValue1 = input1Mode == ParameterMode.IMMEDIATE ? input1 : intCode[input1];
                            int inputValue2 = input2Mode == ParameterMode.IMMEDIATE ? input2 : intCode[input2];

                            intCode[output] = inputValue1 * inputValue2;
                            i += 4;
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Please give input: ");
                            int input = int.Parse(Console.ReadLine());

                            intCode[intCode[i + 1]] = input;
                            i += 2;
                            break;
                        }
                    case 4:
                        {
                            var a = intCode[intCode[i + 1]];
                            Console.WriteLine(a);
                            i += 2;
                            break;
                        }
                    case 5:
                        {
                            int input1 = intCode[i + 1];
                            int input2 = intCode[i + 2];

                            ParameterMode input1Mode = modes.Count >= 1 ? modes[0] : ParameterMode.POSITION;
                            ParameterMode input2Mode = modes.Count >= 2 ? modes[1] : ParameterMode.POSITION;

                            int inputValue1 = input1Mode == ParameterMode.IMMEDIATE ? input1 : intCode[input1];
                            int inputValue2 = input2Mode == ParameterMode.IMMEDIATE ? input2 : intCode[input2];

                            if(inputValue1 != 0)
                            {
                                i = inputValue2;
                            }
                            else
                            {
                                i += 3;
                            }
                            break;
                        }
                    case 6:
                        {
                            int input1 = intCode[i + 1];
                            int input2 = intCode[i + 2];

                            ParameterMode input1Mode = modes.Count >= 1 ? modes[0] : ParameterMode.POSITION;
                            ParameterMode input2Mode = modes.Count >= 2 ? modes[1] : ParameterMode.POSITION;

                            int inputValue1 = input1Mode == ParameterMode.IMMEDIATE ? input1 : intCode[input1];
                            int inputValue2 = input2Mode == ParameterMode.IMMEDIATE ? input2 : intCode[input2];

                            if(inputValue1 == 0)
                            {
                                i = inputValue2;
                            }
                            else
                            {
                                i += 3;
                            }
                            break;
                        }
                    case 7:
                        {
                            int input1 = intCode[i + 1];
                            int input2 = intCode[i + 2];
                            int output = intCode[i + 3];

                            ParameterMode input1Mode = modes.Count >= 1 ? modes[0] : ParameterMode.POSITION;
                            ParameterMode input2Mode = modes.Count >= 2 ? modes[1] : ParameterMode.POSITION;

                            int inputValue1 = input1Mode == ParameterMode.IMMEDIATE ? input1 : intCode[input1];
                            int inputValue2 = input2Mode == ParameterMode.IMMEDIATE ? input2 : intCode[input2];

                            intCode[output] = inputValue1 < inputValue2 ? 1 : 0;
                            i += 4;
                            break;
                        }
                    case 8:
                        {
                            int input1 = intCode[i + 1];
                            int input2 = intCode[i + 2];
                            int output = intCode[i + 3];

                            ParameterMode input1Mode = modes.Count >= 1 ? modes[0] : ParameterMode.POSITION;
                            ParameterMode input2Mode = modes.Count >= 2 ? modes[1] : ParameterMode.POSITION;

                            int inputValue1 = input1Mode == ParameterMode.IMMEDIATE ? input1 : intCode[input1];
                            int inputValue2 = input2Mode == ParameterMode.IMMEDIATE ? input2 : intCode[input2];

                            intCode[output] = inputValue1 == inputValue2 ? 1 : 0;
                            i += 4;
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
                            throw new InvalidOperationException($"{instruction} is not an instruction");
                        }
                }
                if (doBreak)
                {
                    break;
                }
            }
        }

        public static void RunChallenge()
        {
            int[] input = GetInput();
            ProcessIntCode(input);
        }
    }
}
