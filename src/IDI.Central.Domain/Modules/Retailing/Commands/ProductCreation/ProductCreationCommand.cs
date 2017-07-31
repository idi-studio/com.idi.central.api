using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductCreationCommand : Command
    {
        [RequiredField("product-name")]
        [StringLength("product-name", MaxLength = 50)]
        public string Name { get; set; }

        [RequiredField("product-code")]
        [StringLength("product-code", MaxLength = 50)]
        public string Code { get; set; }

        [StringLength("product-model", MaxLength = 50)]
        public string Model { get; set; } = "";

        [StringLength("product-spec", MaxLength = 50)]
        public string Specifications { get; set; } = "";
    }
}
