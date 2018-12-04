using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day2
{
    public static class Day2
    {
        
        public static void Solve()
        {
            Console.WriteLine($"===Day 2===");
            var puzzleInput = File.ReadAllLines("../../../Input/Day2.txt");
       
            {//Part 1

                int[,] array= new int[10000,10000];
                List<Fabric> fabrics = new List<Fabric>();
                foreach (var s in puzzleInput)
                {
                    //#10 @ 322,450: 14x11
                    Fabric fabric = new Fabric();
                    fabric.Id = int.Parse(s.Split('@').First().Replace("#", "").Trim());
                    fabric.X = int.Parse(s.Split('@').Last().Split(':').First().Split(',').First());
                    fabric.Y = int.Parse(s.Split('@').Last().Split(':').First().Split(',').Last());
                    fabric.Width = int.Parse(s.Split('@').Last().Split(':').Last().Split('x').First());
                    fabric.Height = int.Parse(s.Split('@').Last().Split(':').Last().Split('x').Last());
                    fabrics.Add(fabric);
                }


                foreach (var fabric in fabrics)
                {
                    for (int i = 0; i < fabric.Width; i++)
                    {
                        for (int r = 0; r < fabric.Height; r++)
                        {
                            array[fabric.X + i,fabric.Y+r]++;
                        }
                    }
                    
                }

                foreach (var fabric in fabrics)
                {
                    bool lol = false;
                    for (int i = 0; i < fabric.Width; i++)
                    {
                        for (int r = 0; r < fabric.Height; r++)
                        {
                            if (array[fabric.X + i, fabric.Y + r] >1)
                                lol = true;
                        }
                    }

                    if (!lol)
                    {
                        var lsol = "";
                    }

                }

                int counter = 0;
                for (int k = 0; k < array.GetLength(0); k++)
                for (int l = 0; l < array.GetLength(1); l++)
                {
                    if (array[k, l] > 1) counter++;
                }
            }

            {//Part 2
              
            }
        }
    }

    public class Fabric
    {

        public int Id { get; set; } 
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
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

