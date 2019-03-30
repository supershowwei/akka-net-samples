using System;
using Akka.Actor;

namespace SupervisorStrategyLab
{
    public class UserSupervisorStrategyConfigurator : SupervisorStrategyConfigurator
    {
        public override SupervisorStrategy Create()
        {
            return new OneForOneStrategy(
                ex =>
                    {
                        Console.WriteLine(ex.GetBaseException().Message);

                        return Directive.Restart;
                    });
        }
    }
}