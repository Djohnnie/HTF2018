using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Example.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HTF2018.Backend.Example
{
    class Program
    {
        private static Dictionary<String, IChallenge> _challenges = new Dictionary<String, IChallenge>();

        static Program()
        {
            _challenges.Add("http://localhost:52100/Challenges", new Challenge01());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("HTF2018 Example Solution");
            Console.WriteLine("------------------------");
            Console.WriteLine();

            Task running = Task.Run(async () => { await HackAway(); });
            Console.Write("Press the ESC key to shut down this console! ");

            while (!running.IsCompleted)
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        private static async Task HackAway()
        {
            while (_challenges.Values.Any(x => x.Status == Status.Unsuccessful))
            {
                foreach (var challengeKey in _challenges.Keys)
                {
                    var challenge = _challenges[challengeKey];
                    await challenge.Fetch(challengeKey);
                    await challenge.Solve();
                }
            }
        }
    }
}