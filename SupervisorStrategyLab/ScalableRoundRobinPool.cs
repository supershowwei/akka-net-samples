using Akka.Actor;
using Akka.Routing;

namespace SupervisorStrategyLab
{
    public static class ScalableRoundRobinPool
    {
        public static RoundRobinPool Create(int scale, int leastInstances = 1)
        {
            return new RoundRobinPool(1, new DefaultResizer(leastInstances, scale)).WithSupervisorStrategy(
                new OneForOneStrategy(
                    ex =>
                        {
                            switch (ex)
                            {
                                case ActorInitializationException _:
                                case ActorKilledException _: return Directive.Stop;
                                default: return Directive.Restart;
                            }
                        }));
        }
    }
}