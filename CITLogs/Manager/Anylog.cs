using System;
using System.Collections.Generic;

namespace Manager
{
    public class Anylog
    {
        public List<string> Filtered { get; set; }
        public double Duration { get; set; } = 0;
        public string Error { get; set; } = null;
        public Anylog()
        {
            Filtered = new List<string>();
        }

        public List<string> Filter(string input, string filterInput)
        {
            DateTime start = DateTime.Now;
            List<string> filters = new List<string>();
            
            try
            {
                foreach (string filter in filterInput.Split("\n"))
                {
                    filters.Add(filter);
                }
                
                foreach (string line in input.Split("\n"))
                {
                    foreach (string filter in filters)
                    {
                        if(line.Contains(filter)) Filtered.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            Duration = (DateTime.Now - start).TotalSeconds;
            return Filtered;
        }
    }
}