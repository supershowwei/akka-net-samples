using Akka.Routing;

namespace SupervisorStrategyLab
{
    public static class ThousandResizer
    {
        public static Resizer Create()
        {
            return new DefaultResizer(1, 1000);
        }
    }
}