using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Manager
{
    public class Adminlog
    {
        public string Error { get; set; } = null;
        public double Duration { get; set; } = 0;
        public List<string> CITC { get; set; } = new List<string>();
        public List<string> Login { get; set; } = new List<string>();
        public List<string> LoginMisc { get; set; } = new List<string>();
        public List<string> QuitMisc { get; set; } = new List<string>();
        public List<string> Mutes { get; set; } = new List<string>();
        public List<string> Jails { get; set; } = new List<string>();
        public List<string> Bans { get; set; } = new List<string>();
        public List<string> LogsFetched { get; set; } = new List<string>();
        public List<string> Cad { get; set; } = new List<string>();
        public List<string> Support { get; set; } = new List<string>();
        public List<string> Reports { get; set; } = new List<string>();
        public List<string> PendingPunishments { get; set; } = new List<string>();
        public List<string> PossibleAbuse { get; set; } = new List<string>();
        public List<string> RegisteredAbuse { get; set; } = new List<string>();
        public List<string> DutyRelated { get; set; } = new List<string>();
        public List<string> Other { get; set; } = new List<string>();
        public bool UseSpoilers { get; set; } = false;

        public List<string> ActionsTaken()
        {
            List<string> actions = new List<string>();
            actions.AddRange(Mutes);
            actions.AddRange(Jails);
            actions.AddRange(Bans);
            return actions;
        }

        public void Divide(string content)
        {
            DateTime start = DateTime.Now;
            try
            {
                foreach (string line in content.Split('\n'))
                {
                    if (line.Contains(" CITC ")) CITC.Add(line);

                    else if (line.Contains(" LOGIN: ")) Login.Add(line);
                    else if (line.Contains(" LOGIN MISC: ")) LoginMisc.Add(line);
                    else if (line.Contains(" QUIT MISC: ")) QuitMisc.Add(line);

                    else if (line.Contains(" (AA)(BAN) ")) Bans.Add(line);
                    else if (line.Contains(" (AA)(MUTE) ")) Mutes.Add(line);
                    else if (line.Contains(" (AA)(JAIL) ")) Jails.Add(line);
                    else if (line.Contains(" (AA)(CONTACTADMIN) ")) Cad.Add(line);
                    else if (line.Contains(" (AA)(SUPPORT) ")) Support.Add(line);

                    else if (line.Contains(" opened '") && line.Contains(" ms")) LogsFetched.Add(line);
                    else if (line.Contains(" [CM] ") && line.Contains(" set ")) Reports.Add(line);
                    else if (line.Contains("[Complaint ID") && line.Contains(" replied with ")) Reports.Add(line);

                    else if (line.Contains(" warped to ") && !line.Contains(" WL: 0") && !line.Contains(" (EM) "))
                        RegisteredAbuse.Add(line);
                    else if (line.Contains(" warped to ") && line.Contains(" WL: 0") && !line.Contains(" (EM) "))
                        PossibleAbuse.Add(line);
                    else if (line.Contains(" ST ") && (!line.Contains("from 0") && line.Contains("wanted points.")))
                        RegisteredAbuse.Add(line);
                    else if (line.Contains(" ST ") && line.Contains("from 0 wanted points.")) PossibleAbuse.Add(line);
                    else if (line.Contains("abuse")
                             || line.Contains("recommendation")
                             || line.Contains("leak")
                             || line.Contains("bias")
                             || line.Contains("accept")
                             )
                        PossibleAbuse.Add(line);

                    else if (line.Contains("object. ID:")) DutyRelated.Add(line);
                    else if (line.Contains("changed account:")) DutyRelated.Add(line);
                    else if (line.Contains("changed the password of account")) DutyRelated.Add(line);
                    else if (line.Contains("checked the PIN code of account")) DutyRelated.Add(line);
                    else if (line.Contains("zone 0 p")) DutyRelated.Add(line);

                    else if (line.Contains(" (PP) ")) PendingPunishments.Add(line);
                    else Other.Add(line);
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }

            Duration = (DateTime.Now - start).TotalSeconds;
        }

        public string Format()
        {
            const string defaultValue = "TO_BE_FILLED_IN";
            string result = "";
            result += $"[center][size=13pt][b]Team <?>:[/b][/size][/center][br][br]\n";
            result += $"L<?>. [url=https://cit.gg/index.php?action=profile;u=FORUMCODE]{GetNickname()}[/url] ({GetAccount()}):";
            result += $"\n[b]Name:[/b] {GetNickname()}";
            result += $"\n[b]Account name:[/b] {GetAccount()}";
            result += $"\n[b]Rank:[/b] {defaultValue}";
            result += $"\n[b]Duties:[/b] {defaultValue}";
            result += $"\n[b]Hours played:[/b] {GetHoursPlayed()}";
            result += $"\n[b]Login hits:[/b] {Login.Count}";
            result = AddSpoilerFromList(result, Login);

            result += $"\n[b]CITC hits:[/b] {CITC.Count}";
            result = AddSpoilerFromList(result, CITC);

            result +=
                $"\n[b]Admin actions:[/b] {ActionsTaken().Count} (+{LogsFetched.Count} Logs opened for L3)";
            result = AddSpoilerFromList(result, ActionsTaken());

            result += $"\n[b]Mutes:[/b] {Mutes.Count}";
            result = AddSpoilerFromList(result, Mutes);

            result += $"\n[b]Jails:[/b] {Jails.Count}";
            result = AddSpoilerFromList(result, Jails);

            result += $"\n[b]Bans:[/b] {Bans.Count}";
            result = AddSpoilerFromList(result, Bans);

            result += $"\n[b]Forum Warnings:[/b] {defaultValue}";

            result += $"\n[b]Contact Admin:[/b] {Cad.Count}";
            result = AddSpoilerFromList(result, Cad);

            result += $"\n[b]Reports:[/b] {Reports.Count} [CM] actions taken.";
            result = AddSpoilerFromList(result, Reports);

            result += $"\n[b]Abuse:[/b] {defaultValue}";
            result = AddSpoilerFromList(result, RegisteredAbuse);

            result += $"\n[b]Duty related actions:[/b] {defaultValue}";
            result = AddSpoilerFromList(result, DutyRelated);
            result += $"\n\n[i]Additional notes:[/i] {AdditionalNotes()}";
            return result;
        }

        private string AdditionalNotes()
        {
            double actionsCounter = 0;
            actionsCounter += Bans.Count * 1.3;
            actionsCounter += Mutes.Count;
            actionsCounter += Jails.Count * 1.1;
            actionsCounter += Reports.Count * 1.4;
            actionsCounter += CITC.Count * 0.2;
            if (actionsCounter < 30)
            {
                return "Low in-game activity";
            }
            if (actionsCounter < 60)
            {
                return "In-game activity is fine with room for improvement";
            }

            return "Decent in-game activity";
        }

        private string AddSpoilerFromList(string result, List<string> lines)
        {
            if (UseSpoilers)
            {
                result += " [spoiler]";
                foreach (var item in lines)
                {
                    result += $"\n{item}";
                }

                result += "[/spoiler] ";
            }
            return result;
        }

        private string GetHoursPlayed()
        {
            if (LoginMisc.Count > 1)
            {
                string pattern = "PlayTime: ([0-9]+) ";
                var first = Regex.Match(LoginMisc.First(), pattern);
                var last = Regex.Match(QuitMisc.Last(), pattern);
                double result = (int.Parse(last.Groups[1].Value) - int.Parse(first.Groups[1].Value)) / 60;
                return $"({last.Groups[1].Value} - {first.Groups[1].Value})/60 = {result}";
            }

            return "COULD_NOT_CHECK";
        }

        private string GetNickname()
        {
            if (Login.Any())
            {
                string pattern = " CITC (.*): ";
                var match = Regex.Match(CITC.First(), pattern).Groups[1].Value;
                if (match.Length < 1)
                {
                    return "COULD_NOT_FIND";
                }

                return match.Replace(":","").Replace(" CITC ", "");
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
    }
}