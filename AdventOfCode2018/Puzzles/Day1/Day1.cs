using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day1
{
    public static class Day1
    {
        
        public static void Solve()
        {
            Console.WriteLine($"===Day 1===");
            var puzzleInput = File.ReadAllLines("../../../Input/Day1.txt");
       
            {//Part 1
                Console.WriteLine($"Part 1: { puzzleInput.Sum(int.Parse)}");
            }

            {//Part 2
                List<int> states = new List<int>();
                states.Add(0);
                int freq = 0;
                while (true)
                {
                    foreach (var s in puzzleInput)
                    {
                        freq += int.Parse(s);
                        if (states.Contains(freq))
                        {
                            Console.WriteLine($"Part 2: {freq}");
                            return;
                        }
                        states.Add(freq);

                    }
                }

            }
        }
    }
}
