using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Challenges
{
    public static class DayFiveChallenge
    {
        private enum ParameterMode
        {
            POSITION,
            IMMEDIATE
        }

        private static int ProcessIntCode(int[] intCode)
        {
            int i = 1;

            while(i < intCode.Length)
            {
                var fullInstruction = intCode[i];
                var instructionString = fullInstruction.ToString();
                var digits = new int[instructionString.Length];
                for (int j = 0; j < 6; j++)
                {
                    char myChar = instructionString[j];
                    digits[j] = int.Parse($"{myChar}");
                }

                int instruction = digits[^1];
                if(digits.Length > 1)
                {
                    instruction += digits[^2] * 10;
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
                            int pos1 = intCode[i + 1];
                            int pos2 = intCode[i + 2];
                            int pos3 = intCode[i + 3];
                            intCode[pos3] = intCode[pos1] + intCode[pos2];
                            i += 4;
                            break;
                        }
                    //Multiply
                    case 2:
                        {
                            int pos1 = intCode[i + 1];
                            int pos2 = intCode[i + 2];
                            int pos3 = intCode[i + 3];
                            intCode[pos3] = intCode[pos1] * intCode[pos2];
                            i += 4;
                            break;
                        }
                    case 3:
                        {
                            int pos1 = intCode[i + i];
                            intCode[pos1] = pos1;
                            i += 2;
                            break;
                        }
                    case 4:
                        {
                            int pos1 = intCode[i + 1];
                            Console.WriteLine(pos1);
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
    }
}
