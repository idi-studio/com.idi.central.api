using IDI.Core.Common;
using IDI.Core.Common.Basetypes;

namespace IDI.Central.Models.Retailing
{
    public class ProductCreationInput : IInput
    {
        public string Name { get; set; } = "";

        public string Code { get; set; } = "";

        public ProfileCollection Profile { get; set; } = new ProfileCollection();
    }
}
