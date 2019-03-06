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
    public static class Day18
    {

       static int length = 50;
        public static void Solve()
        {
            var puzzleInput = File.ReadAllLines("../../../Input/Day18.txt");
            State[,] state = new State[50, 50];
            State[,] tempState = new State[50, 50];
            

            for (var x = 0; x < puzzleInput.Length; x++)
            {
                var s = puzzleInput[x];
                for (var y = 0; y < s.Length; y++)
                {
                    var c = s[y];

                    switch (c)
                    {
                        case '.':
                            state[x, y] = State.Empty;
                            break;
                        case '#':
                            state[x, y] = State.Lumber;
                            break;
                        case '|':
                            state[x, y] = State.Tree;
                            break;
                    }
                }
            }

            for (int i = 0; i < 1000000000; i++)
            {


                for (var x = 0; x < puzzleInput.Length; x++)
                {
                    var s = puzzleInput[x];
                    for (var y = 0; y < s.Length; y++)
                    {
                        var c = s[y];
                        tempState[x, y] = GetAdjacentCount(x, y, state);



                    }
                }
                if (state == tempState)
                    return;
                state = tempState.Clone() as State[,];
            }

            int lumber = 0;
            int trees = 0; 
            for (int x = 0; x < state.GetLength(0); x += 1)
            {
                for (int y = 0; y < state.GetLength(1); y += 1)
                {
                    if (state[x, y] == State.Tree)
                        trees++;
                    if (state[x, y] == State.Lumber)
                        lumber++;
                    
                }
            }
        }

        public static State GetAdjacentCount(int x, int y, State[,] state)
        {
            var dict = new Dictionary<State, int>();
            dict.Add(State.Empty, 0);
            dict.Add(State.Lumber, 0);
            dict.Add(State.Tree, 0);

            LookupState(x - 1, y - 1, state, dict);//1
            LookupState(x - 0, y - 1, state, dict);//2
            LookupState(x + 1, y - 1, state, dict);//3
            LookupState(x - 1, y + 0, state, dict);//4
            LookupState(x + 1, y + 0, state, dict);//5
            LookupState(x - 1, y + 1, state, dict);//6
            LookupState(x + 0, y + 1, state, dict);//7
            LookupState(x + 1, y + 1, state, dict);//8

            if (state[x, y] == State.Lumber)
                if (dict[State.Lumber] > 0 && dict[State.Tree] > 0)
                    return State.Lumber;
                else
                    return State.Empty;


            if (state[x, y] == State.Empty)
                if (dict[State.Tree] > 2)
                    return State.Tree;
                else
                    return State.Empty;

            if (dict[State.Lumber] > 2)

                return State.Lumber;
            else
                return State.Tree;
            
        }

        //123
        //405
        //678
        public static void LookupState(int x, int y, State[,] state, Dictionary<State, int> dict)
        {
            if (x < 0 || x > length - 1
                      || y < 0 || y > length - 1)
                return;
            dict[state[x, y]]++;
        }

        public enum State
        {

            Tree,
            Lumber,
            Empty
        }


    }

}