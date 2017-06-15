using IDI.Core.Infrastructure.Commands;

namespace IDI.Central.Domain.Modules.SCM.Commands
{
    public class InitializeCommand : Command
    {
        public SeedData SeedData { get; private set; }

        public InitializeCommand()
        {
            this.SeedData = new SeedData();
        }
    }
}
