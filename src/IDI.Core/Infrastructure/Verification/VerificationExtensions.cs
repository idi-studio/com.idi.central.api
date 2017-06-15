using System.Collections.Generic;

namespace IDI.Core.Infrastructure.Verification
{
    public static class VerificationExtensions
    {
        public static bool IsValid<T>(this T instance, out List<string> errors) where T : IVerifiable
        {
            var validator = new Validator<T>(instance);

            return validator.IsValid(out errors);
        }
    }
}
