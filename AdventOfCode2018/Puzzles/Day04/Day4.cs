using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace AdventOfCode2018.Puzzles.Day04
{
    public static class Day4
    {

        public static void Solve()
        {
            Console.WriteLine($"===Day 4===");
            var puzzleInput = File.ReadAllLines("../../../Input/Day4.txt");

            {
                List<Event> events = new List<Event>();
                foreach (var s in puzzleInput)
                {
                    int minute = 0;
                    var dateTime =
                        DateTime.Parse(s.Replace("[", "").Split("]").First());
                    if (dateTime.Hour == 23)
                    {
                        minute = 0;
                        try
                        {
                            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day + 1);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            dateTime = new DateTime(dateTime.Year, dateTime.Month + 1, 1);
                        }
                    }
                    else
                    {
                        minute = dateTime.Minute;
                    }

                    if (s.Contains("wakes up"))
                    {
                        events.Add(new Event
                        {
                            Action = Action.WakeUp,
                            EventDateTime = dateTime,
                            eventTime = minute,
                            RawString = s

                        });
                    }
                    else if (s.Contains("falls asleep"))
                    {
                        events.Add(new Event
                        {
                            Action = Action.FallAsleep,
                            EventDateTime = dateTime,
                            eventTime = minute,
                            RawString = s

                        });
                    }
                    else
                    {
                        events.Add(new Event
                        {
                            Action = Action.Start,
                            EventDateTime = dateTime,
                            eventTime = minute,
                            GuardID = int.Parse(s.Split(null).First(x => x.Contains("#")).Replace("#", "")),
                            RawString = s

                        });
                    }

                }
                var eventTimes = events.OrderBy(x => x.EventDateTime).ThenBy(x => x.eventTime).ToList();
                var sleepingEvents = new List<SleepingEvent>();
                SleepingEvent sleepingEvent = null;
                foreach (var eventt in eventTimes)
                {
                    if (eventt.Action == Action.Start)
                    {
                        sleepingEvent = new SleepingEvent
                        {
                            GuardID = eventt.GuardID.Value,
                            Sleeping = new bool[60]
                        };

                    }
                    else if (eventt.Action == Action.FallAsleep)
                    {
                        sleepingEvent.startIndex = eventt.eventTime;
                    }
                    else
                    {
                        for (int j = sleepingEvent.startIndex; j < eventt.eventTime; j++)
                        {
                            sleepingEvent.Sleeping[j] = true;
                        }

                        sleepingEvents.Add(sleepingEvent);
                        int gaurdID = sleepingEvent.GuardID;
                        sleepingEvent = new SleepingEvent();
                        sleepingEvent.GuardID = gaurdID;
                        sleepingEvent.Sleeping = new bool[60];

                    }
                }

                var guardId = 0;
                var favoriteMinute = 0;
                var amountOfTimesAsleepOnThatMinute = 0;
                foreach (var se in sleepingEvents.GroupBy(x => x.GuardID))
                {
                    for (var i = 0; i < 60; i++)
                    {
                        var tempMinute = se.Count(eventItem => eventItem.Sleeping[i]);
                        if (tempMinute <= amountOfTimesAsleepOnThatMinute) continue;
                        amountOfTimesAsleepOnThatMinute = tempMinute;
                        favoriteMinute = i;
                        guardId = se.First().GuardID;
                    }
 
                }
                Console.WriteLine($"Part 2: {favoriteMinute * guardId}");
                //I removed my code for part 1 while doing part 2 maybe ill come back and add my part1 code back
            }

        }
    }

    public class Event
    {

        public int? GuardID { get; set; }
        public int eventTime { get; set; }
        public DateTime EventDateTime { get; set; }
        public Action Action { get; set; }
        public string RawString { get; set; }
    }

    public class SleepingEvent
    {
        public int GuardID { get; set; }
        public bool[] Sleeping { get; set; }
        public int startIndex { get; set; }


    }


    public enum Action
    {
        WakeUp,
        FallAsleep,
        Start
    }
}