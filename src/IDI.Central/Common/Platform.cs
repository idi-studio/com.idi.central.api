using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Core.Infrastructure;

namespace IDI.Central.Common
{
    public class Platform
    {
        public class Data
        {
            public void Initial()
            {
                ServiceLocator.CommandBus.Send(new InitializeCommand());
            }
        }

        public static Data SeedData()
        {
            return new Data();
        }
    }
}
