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

namespace AdventOfCode2018.Puzzles.Day09
{
    public static class Day9
    {

        public static void Solve()//This is my brute force approach 
        {
            Console.WriteLine($"===Day 9===");

            var numberOfPlayers = 418;
            var finalMarbleScore = 71339*100;
            var marbles = new List<int>();
            var currentMarbleIndex = 0;
            var marbleValue = 1;
            marbles.Add(0);
            var scores = new long[numberOfPlayers];
            var iterations = 1;
            var currentPlayerIndex = 1;
            var listLength = 1;
            while (true)
            {

                if(iterations%500_000==0)
                    Console.WriteLine(iterations);
                if (marbleValue == finalMarbleScore)
                {
                    var lol = scores.Max();
                    Console.WriteLine($"Part 2: {lol}");
                    return;
                }

                if (marbleValue % 23 == 0)
                {

                    int score = 0;
                    score += marbleValue;
                    var tempMarbleIndex  = Wrap(currentMarbleIndex -7, listLength);
                    score += marbles[tempMarbleIndex];
                    marbles.RemoveAt(tempMarbleIndex);
                    listLength--;
                    currentMarbleIndex = tempMarbleIndex;
                    scores[currentPlayerIndex] += score;
                }
                else
                {
                    currentMarbleIndex = Wrap(currentMarbleIndex + 2, listLength);
                    marbles.Insert(currentMarbleIndex, marbleValue);
                    listLength++;
                }
                marbleValue++;
                currentPlayerIndex = ((currentPlayerIndex+1 % numberOfPlayers) + numberOfPlayers) % numberOfPlayers;
                iterations++;
            }
            
        }
        public static int Wrap(int index, int n)
        {
            var temp = ((index % n) + n) % n;
            if (temp == 0) temp = n;
            return temp;
        }

        public class Node
        {
            public int MarbleValue{ get; set; }
            public Node CounterClockwise { get; set; }
            public Node Clockwise { get; set; }
        }
    }
}

