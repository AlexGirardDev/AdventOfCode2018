using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day06
{
    public static class Day6
    {

        public static void Solve()
        {
            Console.WriteLine($"===Day 6===");
            {
                int gridSize = 2000;
                var puzzleInput = File.ReadAllLines("../../../Input/Day6.txt");
                List<List<Dictionary<int, int>>> mainModel = new List<List<Dictionary<int, int>>>();

                List<point> points = new List<point>();
                for (int i = 0; i < gridSize; i++)
                {

                    mainModel.Add(new List<Dictionary<int, int>>());
                    for (int j = 0; j < gridSize; j++)
                    {
                        mainModel[i].Add(new Dictionary<int, int>());
                    }
                }


                int id = 0;
                foreach (var s in puzzleInput)
                {
                    var split = s.Split(',');
                    var point = new point
                    {
                        X = int.Parse(split.First().Trim()) + gridSize/2,
                        Y = int.Parse(split.Last().Trim()) + gridSize/2,
                        Id = id,
                        dead = false
                    };
                    points.Add(point);
                    id++;
                }


                for (int index = 0; index < gridSize / 4; index++)
                {
                    foreach (var point in points)
                    {
                        if (point.dead) continue;

                        if (index > 0)
                            point.dead = true;
                        //X+ Y+
                        for (int z = 0; z < index + 1; z++)
                        {
                            int x = index - z;
                            int y = index - x;
                            DoStuff(mainModel, x + point.X, y + point.Y, point, index);
                        }
                        //X+ Y-
                        for (int z = 0; z < index + 1; z++)
                        {
                            int x = index - z;
                            int y = index - x;
                            DoStuff(mainModel, x + point.X, point.Y - y, point, index);
                        }
                        ////X- Y+
                        for (int z = 0; z < index + 1; z++)
                        {
                            int x = index - z;
                            int y = index - x;
                            DoStuff(mainModel, point.X - x, point.Y + y, point, index);
                        }
                        ////X- Y+
                        for (int z = 0; z < index + 1; z++)
                        {
                            int x = index - z;
                            int y = index - x;
                            DoStuff(mainModel, point.X - x, point.Y - y, point, index);
                        }
                    }
                   
                }

                Dictionary<int, int> nonDeadIds = new Dictionary<int, int>();
                List<int> tempIds = new List<int>();// the runtime was complainging i was editing the list i was iterating
                foreach (var point in points)
                {
                    if (point.dead)
                    {
                        nonDeadIds.Add(point.Id, 0);
                        tempIds.Add(point.Id);
                    }
                }

                for (int j = 0; j < mainModel.Count; j++)
                for (int i = 0; i < mainModel[j].Count; i++)
                    foreach (var id1 in tempIds)
                    {

                        if (mainModel[j][i].ContainsKey(id1) && mainModel[j][i][id1] >= 0) nonDeadIds[id1]++;
                    }

                Console.WriteLine($"Part 1: {nonDeadIds.Max(x => x.Value)}"); 
                //I dont know where my code for part 2 went,i did part 2 the easier way though/
                //where i just picked a large grid size and calculated the manhattan distance of each cell

            }
        }

        private static void DoStuff(List<List<Dictionary<int, int>>> mainModel, int x, int y, point point, int i)
        {
            if (mainModel[x][y].ContainsKey(point.Id) || mainModel[x][y].Any(r => r.Value < i) ||
                mainModel[x][y].Any(r => r.Value == -1)) return;



            if (mainModel[x][y].Any(r => r.Value > i))
            {
                mainModel[x][y].Clear();
            }

            var tempList = mainModel[x][y].Where(r => r.Value != point.Id).ToList();
            if (tempList.Any(v => v.Value == i) && tempList.Any())
            {
                var tempDict = tempList.ToDictionary(p => p.Key, p => p.Value).Keys;
                foreach (var key in tempDict)
                {
                    mainModel[x][y][key] = -1;
                    if (!mainModel[x][y].ContainsKey(point.Id))
                        mainModel[x][y].Add(point.Id, -1);
                }
            }
            else
            {
                mainModel[x][y].Add(point.Id, i);
                point.dead = false;
            }

            if (mainModel[x][y].Any(z => z.Value == -1))
            {
                var keys = mainModel[x][y].Keys.Select(h => h).ToList();

                foreach (var key in keys)
                {
                    mainModel[x][y][key] = -1;
                }
            }

            if (mainModel[x][y].GroupBy(r => r.Value).Any(r => r.Count() > 1))
            {
                var keys = mainModel[x][y].Keys.Select(h => h).ToList();

                foreach (var key in keys)
                {
                    mainModel[x][y][key] = -1;
                }
            }
        }
    }

    public class point
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool dead { get; set; }
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

