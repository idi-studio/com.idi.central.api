using IDI.Core.Common.Enums;

namespace IDI.Core.Infrastructure.Verification
{
    public interface IVerifiable
    {
        ValidationGroup Group { get; set; }
    }
}
