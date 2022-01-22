using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.inherited
{
    public abstract class Divider : IDivider
    {
        public string Error { get; set; } = null;
        public double Duration { get; set; } = 0;
        
        public Task Divide(string content)
        {
            DateTime start = DateTime.Now;
            var tasks = new List<Task>();
            try
            {
                tasks.AddRange(content.Split('\n').Select(DivideLine));
                tasks.ForEach(AwaitLine);
            }
            catch (Exception e)
            {
                Error = e.Message;
                Console.WriteLine(e);
                return Task.FromException(e);
            }

            Duration = (DateTime.Now - start).TotalSeconds;
            return Task.CompletedTask;
        }
        
        private static async void AwaitLine(Task task)
        {
            await task;
        }

        public abstract Task DivideLine(string line);
    }
}