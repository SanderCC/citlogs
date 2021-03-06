using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.inherited;

namespace Manager
{
    public class Grouplog : Divider
    {
        public List<string> Bank { get; set; }
        public List<string> RankChanges { get; set; }
        public List<string> JoinsDepartures { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Applications { get; set; }
        public List<string> Management { get; set; }
        public List<string> CustomTitle { get; set; }
        public List<string> Other { get; set; }

        public Grouplog()
        {
            Bank = new List<string>();
            RankChanges = new List<string>();
            JoinsDepartures = new List<string>();
            Warnings = new List<string>();
            Applications = new List<string>();
            Management = new List<string>();
            CustomTitle = new List<string>();
            Other = new List<string>();
        }

        public override Task DivideLine(string line)
        {
            if (line.Length < 5) Console.WriteLine("Line too small, skipped.");

            else if (line.Contains("'s application. (")) Applications.Add(line);
            else if (line.Contains(" has submitted an application.")) Applications.Add(line);

            else if (line.Contains(" is promoting ")) RankChanges.Add(line);
            else if (line.Contains(" is demoting ")) RankChanges.Add(line);

            else if (line.Contains(") joined the group")) JoinsDepartures.Add(line);
            else if (line.Contains(" has kicked ")) JoinsDepartures.Add(line);
            else if (line.Contains(") left the group as ")) JoinsDepartures.Add(line);

            else if (line.Contains(" deposited to ")) Bank.Add(line);
            else if (line.Contains(" in the group bank")) Bank.Add(line);
            else if (line.Contains(" group bank for reason:")) Bank.Add(line);
            else if (line.Contains(" withdrew ")) Bank.Add(line);

            else if (line.Contains(") updated the group info")) Management.Add(line);
            else if (line.Contains(" has updated group application.")) Management.Add(line);
            else if (line.Contains(" blacklist.")) Management.Add(line);
            else if (line.Contains("updated the group whitelist")) Management.Add(line);

            else if (line.Contains(") warned ")) Warnings.Add(line);
            else if (line.Contains(") rewarded ")) Warnings.Add(line);

            else if (line.Contains(") has promoted group: ")) Management.Add(line);
            else if (line.Contains(") updated rank: ")) Management.Add(line);
            else if (line.Contains(") added rank: ")) Management.Add(line);
            else if (line.Contains(") removed rank: ")) Management.Add(line);

            else if (line.Contains("custom title")) CustomTitle.Add(line);

            else Other.Add(line);
            return Task.CompletedTask;
        }
    }
}