using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Manager.inherited;

namespace Manager
{
    public class SOlog : Divider
    {
        public List<string> Quizzes { get; set; } = new List<string>();
        public List<string> Events { get; set; } = new List<string>();
        public List<string> Team { get; set; } = new List<string>();
        public List<string> Login { get; set; } = new List<string>();
        public List<string> LoginMisc { get; set; } = new List<string>();
        public List<string> QuitMisc { get; set; } = new List<string>();
        
        public override Task DivideLine(string line)
        {
            Console.WriteLine($"{DateTime.Now} Thread {Thread.CurrentThread.ManagedThreadId} started job.");
            if (line.Contains(" LOGIN: ")) Login.Add(line);
            if (line.Contains(" LOGIN MISC: ")) LoginMisc.Add(line);
            if (line.Contains(" QUIT MISC: ")) QuitMisc.Add(line);
            if(line.Contains("TC: [Civilian Workers]")) Team.Add(line);
            if(line.Contains(" Hosted Civilian event; ")) Events.Add(line);
            if(line.Contains(" created a quiz of '")) Quizzes.Add(line);
            Console.WriteLine($"{DateTime.Now} Thread {Thread.CurrentThread.ManagedThreadId} ended job.");
            return Task.CompletedTask;
        }

        public string Format()
        {
            var result = $"[color={GetTitleColor()}][b][size=14pt]SO Review[/size][/b][/color][hr]";
            result += $"\n[b]Nick: [/b]{GetNick()}";
            result += $"\n[b]Account: [/b]{GetAccount()}";
            result += $"\n[b]Hours played: [/b]{GetPlaytime()}h";
            result += $"\n[b]TC Hits: [/b]{Team.Count}";
            result += $"\n[b]Event hits: [/b]{Quizzes.Count+Events.Count} ({Quizzes.Count} quizzes + {Events.Count} events)";
            result += $"\n[b]Advice: [/b]{GetAdvice()}";
            return result;
        }

        private string GetTitleColor()
        {
            if (GetStrikes() > 2) return "darkred";
            if (GetStrikes() > 1) return "red";
            if (GetStrikes() > 0) return "orange";
            return "black";
        }

        private string GetNick()
        {
            if (Login.Any())
            {
                string pattern = " TC: [Civilian Workers] (.*): ";
                var match = Regex.Match(Team.First(), pattern).Groups[1].Value;
                if (match.Length < 1)
                {
                    return "COULD_NOT_FIND";
                }

                return match.Replace(":", "").Replace(" TC: [Civilian Workers] ", "");
            }

            return "COULD_NOT_FIND";
        }

        private string GetAccount()
        {
            if (Login.Any())
            {
                string pattern = " LOGIN: (.*) logged into by";
                return Regex.Match(Login.First(), pattern).Groups[1].Value;
            }

            return "COULD_NOT_FIND";
        }

        private int GetStrikes()
        {
            var strikes = 0;
            if (Quizzes.Count + Events.Count < 15) strikes++;
            if (Team.Count < 70) strikes++;
            if (GetPlaytime() < 30) strikes++;
            return strikes;
        }
        
        private string GetAdvice()
        {
            var result = "";
            if (Quizzes.Count + Events.Count < 15) result += "\nShould host more events.";
            if (Team.Count < 70) result += "\nShould socialize more with the other civilians.";
            if (GetPlaytime() < 30) result += "\nShould boost playtime.";

            if (result.Length > 5) return result;
            return "No issues to report.";
        }

        private double GetPlaytime()
        {
            if (LoginMisc.Count > 1)
            {
                string pattern = "PlayTime: ([0-9]+) ";
                var first = Regex.Match(LoginMisc.First(), pattern);
                var last = Regex.Match(QuitMisc.Last(), pattern);
                return (int.Parse(last.Groups[1].Value) - int.Parse(first.Groups[1].Value)) / 60;
            }

            return -999;
        }
    }
}