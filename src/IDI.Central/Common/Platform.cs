using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Core.Infrastructure;

namespace IDI.Central.Common
{
    public class Platform
    {
        public class Data
        {
            public void Initial()
            {
                ServiceLocator.CommandBus.Send(new DataInitializationCommand());
            }
        }

        public static Data SeedData()
        {
            return new Data();
        }
    }
}
