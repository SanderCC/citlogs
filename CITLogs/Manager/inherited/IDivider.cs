using System.Threading.Tasks;

namespace Manager.inherited
{
    public interface IDivider
    {
        public Task Divide(string content);
        public Task DivideLine(string line);
    }
}