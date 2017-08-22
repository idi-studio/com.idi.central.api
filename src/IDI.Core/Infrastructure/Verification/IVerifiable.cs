using IDI.Core.Common.Enums;

namespace IDI.Core.Infrastructure.Verification
{
    public interface IVerifiable
    {
        VerificationGroup Group { get; set; }
    }
}
