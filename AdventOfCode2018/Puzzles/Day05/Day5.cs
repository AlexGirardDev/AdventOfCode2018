using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day05
{
    public static class Day5
    {

        public static void Solve()
        {
            Console.WriteLine($"===Day 5===");


            {
                var puzzleInput = File.ReadAllText("../../../Input/Day5.txt");
                //Part 1
                Dictionary<string, string> keys = new Dictionary<string, string>();

                Console.WriteLine($"Part 1: {FullyReact(puzzleInput)}");
            }

            {

                var puzzleInput = File.ReadAllText("../../../Input/Day5.txt");
                int lowestCount = puzzleInput.Length;
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    int count = FullyReact(
                        puzzleInput.Replace(c.ToString(), "").Replace(Char.ToLower(c).ToString(), ""));
                    if (count < lowestCount)
                        lowestCount = count;
                }

                Console.WriteLine($"Part 2: {lowestCount}");
            }
        }


        public static int FullyReact(string puzzleInput)
        {
            try
            {


                while (true)
                {
                    for (int i = 0; i < puzzleInput.Length; i++)
                    {
                        if (Char.IsUpper(puzzleInput[i]) && puzzleInput[i + 1].Equals(Char.ToLower(puzzleInput[i])))
                        {
                            puzzleInput = puzzleInput.Remove(i, 2);
                            if (i < 2)
                                i = 0;
                            else
                                i--;
                            i--;
                        }

                        else if (Char.IsLower(puzzleInput[i]) &&
                                 puzzleInput[i + 1].Equals(Char.ToUpper(puzzleInput[i])))
                        {
                            puzzleInput = puzzleInput.Remove(i, 2);
                            if (i < 2)
                                i = 0;
                            else
                                i--;
                            i--;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return puzzleInput.Length;

        }
    }
}









//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace AdventOfCode2018.Puzzles.Day2
//{
//    public static class Day2
//    {

//        public static void Solve()
//        {
//            Console.WriteLine($"===Day 2===");
//            var puzzleInput = File.ReadAllLines("../../../Input/Day2.txt");

//            {//Part 1
//                int three = 0;
//                int two = 0;
//                foreach (var s in puzzleInput)
//                {
//                    if (s.GroupBy(x => x).Count(y => y.ToList().Count() == 2) > 0)
//                        two++;

//                    if (s.GroupBy(x => x).Count(y => y.ToList().Count() == 3) > 0)
//                        three++;
//                }


//                Console.WriteLine($"Part 2: { three * two}");
//            }

//            {//Part 2
//                string pair1 = "";
//                string pair2 = "";
//                int bestMatch = 100;
//                foreach (var s in puzzleInput)
//                {

//                    foreach (var s1 in puzzleInput)
//                    {

//                        if (s.Equals(s1)) continue;
//                        int tempBestWord = 0;
//                        for (int i = 0; i < s.Length; i++)
//                        {
//                            if (s[i] != s1[i])
//                                tempBestWord++;
//                        }

//                        if (tempBestWord < bestMatch)
//                        {
//                            bestMatch = tempBestWord;
//                            pair1 = s;
//                            pair2 = s1;
//                        }

//                    }


//                }

//                string output = "";
//                for (int i = 0; i < pair1.Length; i++)
//                {
//                    if (pair1[i] == pair2[i])
//                        output += pair1[i];
//                }
//                Console.WriteLine($"Part 2: {output}");

//            }
//        }
//    }
//}

