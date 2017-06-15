using System;
using System.ComponentModel.DataAnnotations;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        public abstract ValidationResult IsValid(ValidationContext context);
    }
}
