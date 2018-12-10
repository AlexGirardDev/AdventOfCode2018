using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
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

namespace AdventOfCode2018.Puzzles.Day10
{
    public static class Day10
    {

        public static void Solve()
        {
            var puzzleInput = File.ReadAllLines("../../../Input/Day10.txt");
            Console.WriteLine($"===Day 10===");
            List<InputItem> inputs = new List<InputItem>();
            Dictionary<int, State> state = new Dictionary<int, State>();
            int id = 0;
            foreach (var i in puzzleInput)
            {
                var stringg = i.Replace("position=<", "").Replace("> velocity=", "").Replace(">", "").Split("<");
                var inputItem = new InputItem
                {
                    Id = id,
                    X = int.Parse(stringg.First().Split(",").First().Trim()),
                    Y = int.Parse(stringg.First().Split(",").Last().Trim()),
                    VelX = int.Parse(stringg.Last().Split(",").First().Trim()),
                    VelY = int.Parse(stringg.Last().Split(",").Last().Trim())
                };
                inputs.Add(inputItem);
                state.Add(id, new State {X = inputItem.X, Y = inputItem.Y});
                id++;
            }

            int maxDistance = int.MaxValue;
            bool isClosest = true;
            var index = 0;
            var states = new List<string>();
            Dictionary<int, State> previousState;
            while (true)
            {
                previousState = new Dictionary<int, State>();
                foreach (var state1 in state)
                {
                    previousState.Add(state1.Key, new State {X = state1.Value.X, Y = state1.Value.Y});
                }

                foreach (var s in inputs)
                {
                    state[s.Id].X += s.VelX;
                    state[s.Id].Y += s.VelY;
                }
                var delta = (state.Values.Max(x => x.X) - state.Values.Min(x => x.X)) +
                            (state.Values.Max(x => x.Y) - state.Values.Min(x => x.Y));

                if (maxDistance > delta)
                {
                    maxDistance = delta;
                }
                else
                {
                    Console.WriteLine($"Part 1:");
                    for (int j = previousState.Values.Min(x => x.Y); j < previousState.Values.Max(x => x.Y)+1; j++)
                    {
                        for (int i = previousState.Values.Min(x => x.X); i < previousState.Values.Max(x => x.X)+1; i++)
                        {
                            Console.Write((previousState.Values.FirstOrDefault(x => x.X == i && x.Y == j) != null
                                ? "#"
                                : "."));
                        }
                        Console.WriteLine();
                    }

                    return;
                }
                index++;
            }
            Console.WriteLine($"Part 2:{index}");
        }

        private static string PrintDictionary(Dictionary<int, State> state, int iteration)
        {
            var xy = state.Values;
            var sb = new StringBuilder();
            for (int i = 0; i < xy.Count; i++)
            {
                for (int j = 0; j < xy.Count; j++)
                {
                    sb.Append((xy.FirstOrDefault(x => x.X == j && x.Y == i) != null ? "#" : "") + ", ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }


    public class InputItem
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int VelX { get; set; }
        public int VelY { get; set; }
    }

    public class State
    {
        public int X { get; set; }
        public int Y { get; set; }

    }
}

