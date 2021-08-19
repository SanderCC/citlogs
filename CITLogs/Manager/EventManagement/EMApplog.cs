using System;
using System.Collections.Generic;
using System.Linq;

namespace Manager
{
    public class EMApplog
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public string Error { get; set; } = null;
        public double Duration { get; set; } = 0;
        public double HoursPlayed { get; set; } = 0;
        public int LoginHits { get; set; } = 0;
        public int EventWarps { get; set; } = 0;
        public List<string> Serials { get; set; }
        public List<string> Format { get; set; } = null;

        public EMApplog()
        {
            Serials = new List<string>();
            Format = new List<string>();
        }

        public void Divide(DateTime startPeriod, DateTime endPeriod, string logs)
        {
            try
            {
                Error = null;
                DateTime start = DateTime.Now;
                List<string> lines = logs.Split('\n').ToList();
                logs = null;
                EventWarps = lines.Count(l => l.Contains(" warped to the current event") && l.Contains(" (EM) "));
                LoginHits = lines.Count(l => l.Contains(" LOGIN MISC: "));
                GenerateFormat();
                Duration = (DateTime.Now - start).TotalSeconds;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }

        }

        private void GenerateFormat()
        {
            Format = new List<string>();
            Format.Add($"1. [b]Login hits[sub][size=7pt](from 01/01/2020 to 20/07/2020)[/size][/sub]:[/b] {LoginHits}");
            Format.Add($"2. [b]Hours before 2 months and Hours Currently [sub][size=7pt](from 20/05/2020 to 20/07/2020)[/size][/sub]:[/b]");
            Format.Add($"3. [b]Hours and LOGIN hits comparison [sub][size=7pt](from 20/05/2020 to 20/07/2020)[/size][/sub]:[/b]");
            Format.Add($"4. [b]Times Used /eventwarp [sub][size=7pt](from 20/05/2020 to 20/07/2020)[/size][/sub]:[/b] {EventWarps}");
            Format.Add($"5. [b]Ingame punishments (Punish Log):[/b]");
            Format.Add($"6. [b]Forum warnings:[/b]");
            Format.Add($"7. [b]Account Security check:[/b]");
        }
    }
}