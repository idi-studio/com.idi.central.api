using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common.Enums;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        public VerificationGroup Group { get; set; } = VerificationGroup.Default;

        public abstract ValidationResult IsValid(ValidationContext context);

        public bool Enabled(VerificationGroup group)
        {
            return group.HasFlag(this.Group);
        }
    }
}
