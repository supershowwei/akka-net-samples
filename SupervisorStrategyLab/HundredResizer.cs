using Akka.Routing;

namespace SupervisorStrategyLab
{
    public static class HundredResizer
    {
        public static Resizer Create(int lower = 1)
        {
            return new DefaultResizer(lower, 100);
        }
    }
}