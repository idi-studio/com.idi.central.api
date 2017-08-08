using IDI.Central.Domain.Localization;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductCreationCommand : Command
    {
        [RequiredField(Resources.Key.DisplayName.ProductName)]
        [StringLength(Resources.Key.DisplayName.ProductName, MaxLength = 50)]
        public string Name { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductCode)]
        [StringLength(Resources.Key.DisplayName.ProductCode, MaxLength = 50)]
        public string Code { get; set; }

        [RequiredField(Resources.Key.DisplayName.ProductTags)]
        public string Tags { get; set; }
    }
}
