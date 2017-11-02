using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common.Enums;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        public ValidationGroup Group { get; set; } = ValidationGroup.Default;

        public abstract ValidationResult IsValid(ValidationContext context);

        public bool Enabled(ValidationGroup group)
        {
            return group.HasFlag(this.Group);
        }
    }
}
