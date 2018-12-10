using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day07
{
    public static class Day7
    {

        public static void Solve()
        {
            Console.WriteLine($"===Day 7===");

            var input = File.ReadAllLines("../../../Input/Day7.txt").ToList();

            var letterLookup = new Dictionary<char,int>();

            {
                var dependencies = input.Select(x => new InputMeme { Dependent = x.Split()[1][0], Node = x.Split()[7][0] })
                    .ToList();
                var letters = dependencies.Select(x => x.Node).ToList();
                letters.AddRange(dependencies.Select(x => x.Dependent).ToList());
                letters = letters.Distinct().OrderBy(x => x).ToList();
                var result = string.Empty;

                while (letters.Any())
                {
                    var valid = letters.First(s => dependencies.All(d => d.Node != s));
                    result += valid;
                    letters.Remove(valid);
                    dependencies.RemoveAll(d => d.Dependent == valid);
                }
                Console.WriteLine($"Part 1: {result}");
            }
            {
                var numberOfWorkers = 5;
                int index = 1;
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    letterLookup.Add(c, index);
                    index++;
                }

                var dependencies = new List<InputMeme>();

                dependencies = input.Select(x => new InputMeme {Dependent = x.Split()[1][0], Node = x.Split()[7][0]})
                    .ToList();

                var letters = dependencies.Select(x => x.Node).ToList();
                letters.AddRange(dependencies.Select(x => x.Dependent).ToList());
                letters = letters.Distinct().OrderBy(x => x).ToList();

                var workers = new List<Worker>();
                for (int i = 0; i < numberOfWorkers; i++)
                {
                    workers.Add(new Worker { TimeLeft = 0 });
                }

                var time = 0;
                var completedLetters = new List<char>();

                while (letters.Any() || workers.Any(x => x.TimeLeft > 0))
                {
                    workers.ForEach(x => x.TimeLeft--);
                    var validWorkers = workers.Where(x => x.TimeLeft < 1).ToList();

                    completedLetters.AddRange(validWorkers.Where(x => x.TimeLeft < 1 && x.Node != char.MinValue)
                        .Select(x => x.Node));
                    foreach (var completedLetter in completedLetters)
                    {
                        dependencies.RemoveAll(d => d.Dependent == completedLetter);
                    }

                    time++;
                    if (!validWorkers.Any()) continue;
                    validWorkers.Where(x => x.TimeLeft < 1).ToList().ForEach(x => x.Node = char.MinValue);

                    var valid = letters.Where(s =>
                        dependencies.All(d => d.Node != s) ||
                        dependencies.All(x => completedLetters.Contains(x.Dependent)));

                    if (!valid.Any())
                        continue;

                    foreach (var validChars in valid)
                    {
                        if (!validWorkers.Any()) continue;
                        var worker = validWorkers.First();

                        worker.Node = validChars;
                        worker.TimeLeft = letterLookup[validChars] + 60;
                        validWorkers.Remove(worker);
                        letters = letters.Where(x => x != validChars).ToList();
                    }
                }
                Console.WriteLine($"Part 2: {time-1}");
            }
        }
        public class Worker
        {
            public char Node { get; set; }
            public int TimeLeft { get; set; }
        }
        public class InputMeme
        {
            public char Node { get; set; }
            public char Dependent { get; set; }
        }
    }
}
