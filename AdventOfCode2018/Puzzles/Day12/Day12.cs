using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day11
{
    public static class Day12
    {
       static Dictionary<string,bool> stateChanges = new Dictionary<string, bool>();
        public static void Solve()
        {
            var puzzleInput = File.ReadAllLines("../../../Input/Day11.txt");
            var startindex = 21;
            string state =
                ".....................#.#.#...#..##..###.##.#...#.##.#....#..#.#....##.#.##...###.#...#######.....##.###.####.#....#.#..##.....................";
            string newState = "";

            foreach (var s in puzzleInput)
            {
                stateChanges.Add(s.Split(">").First().Replace(" =", ""), s.Split(">").Last() == " #" );
            }

            for (long x = 0; x < 50000000000; x++)
            {

                newState = "";
                for (int i = 0; i < state.Length; i++)
                {
                    newState += CheckState(i, state) ? "#" : ".";
                }

                state = newState;
            }

            int counter = 0;
            for (var index = 0; index < state.Length; index++)
            {
                if(state[index].ToString().Equals("#"))
                counter+= index-startindex;
            }
        }

        public static bool CheckState(int index,string state )
        {
            index = index - 2;
            var key = "";
            state += "...";
            if (index == -2)
            {
                key+="..";
                key+= state.Substring(index+2, 3);
            }
            else if (index == -1)
            {
                key += ".";
                key += state.Substring(index+1, 4);
            }
            else
            {
                key = state.Substring(index, 5);
            }


            return stateChanges.ContainsKey(key) && stateChanges[key];
        }

    }


    public class StateChange
    {

        public string StateCondition { get; set; }
        public bool result { get; set; }
    }
}

