using IDI.Core.Infrastructure.Commands;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class DataInitializationCommand : Command
    {
        public SeedData SeedData { get; private set; }

        public DataInitializationCommand()
        {
            this.SeedData = new SeedData();
        }
    }
}
