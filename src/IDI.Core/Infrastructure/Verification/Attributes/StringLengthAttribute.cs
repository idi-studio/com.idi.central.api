using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common;
using IDI.Core.Localization;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    /// <summary>
    /// 字符长度验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class StringLengthAttribute : ValidationAttribute
    {
        public string DisplayName { get; private set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public StringLengthAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }

        public override ValidationResult IsValid(ValidationContext context)
        {
            object propertyValue = context.Property.GetValue(context.Instance, null);

            propertyValue = propertyValue ?? "";

            int length = propertyValue.ToString().Length;

            if (this.MinLength > 0 && this.MaxLength > 0 && (!(length >= this.MinLength && length <= this.MaxLength)))
            {
                return new ValidationResult(Language.Instance.GetByCulture("verification-characters-range").ToFormat(this.DisplayName, this.MinLength, this.MaxLength));
            }

            if (this.MinLength > 0 && this.MaxLength == 0 && length < this.MinLength)
            {
                return new ValidationResult(Language.Instance.GetByCulture("verification-characters-minimum").ToFormat(this.DisplayName, this.MinLength));
            }

            if (this.MinLength == 0 && this.MaxLength > 0 && length > this.MaxLength)
            {
                return new ValidationResult(Language.Instance.GetByCulture("verification-characters-maximum").ToFormat(this.DisplayName, this.MaxLength));
            }

            return ValidationResult.Success;
        }
    }
}
