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

namespace AdventOfCode2018.Puzzles.Day11
{
    public static class Day11
    {

        public static void Solve()
        {
            int puzzleInput = 3613;
            Console.WriteLine($"===Day 11===");
            var gridSize = 300;
            var levels = new int[gridSize, gridSize];

            for (var x = 0; x < gridSize; x++)
            for (var y = 0; y < gridSize; y++)
            {
                var rackId = x + 10;
                var powerlevel = rackId * y;
                powerlevel += puzzleInput;
                powerlevel = powerlevel * rackId;
                powerlevel = powerlevel >= 100 ? Math.Abs(powerlevel / 100 % 10) : 0;
                powerlevel -= 5;
                levels[x, y] = powerlevel;
            }

            {//Part1
                var winningX = 0;
                var winningY = 0;
                var largestValue = int.MinValue;
                var scanSize = 3;
               
                    for (var x = 0; x < gridSize - scanSize; x++)
                    for (var y = 0; y < gridSize - scanSize; y++)
                    {
                        var tempInt = 0;
                        for (var xScan = x; xScan < scanSize + x; xScan++)
                        for (var yScan = y; yScan < scanSize + y; yScan++)
                        {
                            tempInt += levels[xScan, yScan];
                        }

                        if (tempInt <= largestValue) continue;
                        largestValue = tempInt;
                        winningX = x;
                        winningY = y;
                    }
                
                Console.WriteLine($"Part 1:{winningX},{winningY}");
            }
            {
                var largestValue = int.MinValue;
                var winningX = 0;
                var winningY = 0;
                var winningScanSize = 0;
                for (var scanSize = 0; scanSize < gridSize; scanSize++)
                {
                    for (var x = 0; x < gridSize - scanSize; x++)
                    for (var y = 0; y < gridSize - scanSize; y++)
                    {
                        var tempInt = 0;
                        for (var xScan = x; xScan < scanSize + x; xScan++)
                        for (var yScan = y; yScan < scanSize + y; yScan++)
                        {
                            tempInt += levels[xScan, yScan];
                        }

                        if (tempInt <= largestValue) continue;
                        largestValue = tempInt;
                        winningX = x;
                        winningY = y;
                        winningScanSize = scanSize;
                    }
                }

                Console.WriteLine($"Part 2:{winningX},{winningY},{winningScanSize}");
            }
        }
    }
}

