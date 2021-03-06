using System;
using System.Collections.Generic;
using System.Linq;

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
            var start = DateTime.Now;
            var filters = new List<string>();
            
            try
            {
                var inputArray = input.Split('\n');
                foreach (var filter in filterInput.Split('\n'))
                {
                    filters.Add(filter.ToLower());
                }

                foreach (string filter in filters)
                {
                    if (inputArray.Any(s => s.ToLower().Contains(filter)))
                    {
                        Filtered.AddRange(
                            inputArray.Where(s => s.Contains(filter))
                            );
                    }
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            Filtered.Sort();
            Filtered = Filtered.Distinct().ToList();
            Duration = (DateTime.Now - start).TotalSeconds;
            return Filtered;
        }
    }
}