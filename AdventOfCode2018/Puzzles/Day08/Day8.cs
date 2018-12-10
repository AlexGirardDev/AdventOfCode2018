using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day08
{
    public static class Day8
    {

        public static void Solve()
        {
            Console.WriteLine($"===Day 8===");

            var puzzleInput = File.ReadAllText("../../../Input/Day8.txt").Split(null).Select(x => int.Parse(x)).ToList();
            Console.WriteLine($"Part 1: {RecurseEverythingP1(puzzleInput)}");
             puzzleInput = File.ReadAllText("../../../Input/Day8.txt").Split(null).Select(x => int.Parse(x)).ToList();
            Console.WriteLine($"Part 2: {RecurseEverythingP2(puzzleInput)}");


        }


        public static int RecurseEverythingP1(List<int> input)
        {
            var count = 0;
            var children = input.First();
            input.RemoveAt(0);
            var metaCount = input.First();
            input.RemoveAt(0);

            for (var i = 0; i < children; i++)
            {
                count += RecurseEverythingP1(input);

            }
            for (var i = 0; i < metaCount; i++)
            {
                count += input.First();
                input.RemoveAt(0);
            }
            return count;
        }

        public static int RecurseEverythingP2(List<int> input)
        {
            var count = 0;
            var children = 0;
            var metaCount = 0;
            children = input.First();
            input.RemoveAt(0);
            metaCount = input.First();
            input.RemoveAt(0);

            var counts = new List<int>();
            for (int i = 0; i < children; i++)
            {
                counts.Add(RecurseEverythingP2(input));
            }
            var metas = new List<int>();
            for (int i = 0; i < metaCount; i++)
            {
                metas.Add(input.First());
                input.RemoveAt(0);
            }
            if (children > 0)
            {
                foreach (var t in metas)
                {
                    if (t - 1 < counts.Count)
                        count += counts[t - 1];
                }
            }
            else
            {
                count += metas.Sum();
            }

            return count;
        }

    }
}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Runtime.CompilerServices;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;

//namespace AdventOfCode2018.Puzzles.Day8
//{
//    public static class Day8
//    {

//        public static void Solve()
//        {
//            Console.WriteLine($"===Day 7===");

//            var puzzleInput = File.ReadAllText("../../../Input/Day8.txt").Split(null).Select(x => int.Parse(x));

//            var returnasd = RecurseEverythingP1(puzzleInput.ToList());


//        }

//        public static int RecurseEverythingP1(List<int> input)
//        {
//            int count = 0;
//            bool readMeta = false;
//            int children = 0;
//            int metaCount = 0;
//            if (!readMeta)
//            {

//                children = input.First();
//                input.RemoveAt(0);
//                metaCount = input.First();
//                input.RemoveAt(0);

//            }

//            for (int i = 0; i < children; i++)
//            {
//                count += RecurseEverythingP1(input);

//            }

//            for (int i = 0; i < metaCount; i++)
//            {
//                count += input.First();
//                input.RemoveAt(0);
//            }





//            return count;
//        }

//    }
//}