using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        public string Group { get; set; } = Constants.VerificationGroup.Default;

        public abstract ValidationResult IsValid(ValidationContext context);

        public bool Enabled(string group)
        {
            if (group.IsNull())
                return true;

            return this.Group.Split(",").Contains(group);
        }
    }
}
