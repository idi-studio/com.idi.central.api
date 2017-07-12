using IDI.Core.Infrastructure.Commands;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class DataInitializationCommand : Command
    {
        public Seed Seed { get; private set; }

        public DataInitializationCommand()
        {
            this.Seed = new Seed();
        }
    }
}
