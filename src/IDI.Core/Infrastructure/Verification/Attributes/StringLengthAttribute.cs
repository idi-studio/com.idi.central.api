using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;

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

            string displayName = Language.Instance.GetByCulture(Resources.Prefix.DISPLAY_NAME, this.DisplayName);

            if (this.MinLength > 0 && this.MaxLength > 0 && (!(length >= this.MinLength && length <= this.MaxLength)))
            {
                return new ValidationResult(Language.Instance.GetByCulture(Resources.Prefix.VERIFICATION, Resources.Key.CHARACTERS_RANGE).ToFormat(displayName, this.MinLength, this.MaxLength));
            }

            if (this.MinLength > 0 && this.MaxLength == 0 && length < this.MinLength)
            {
                return new ValidationResult(Language.Instance.GetByCulture(Resources.Prefix.VERIFICATION, Resources.Key.CHARACTERS_MINIMUM).ToFormat(displayName, this.MinLength));
            }

            if (this.MinLength == 0 && this.MaxLength > 0 && length > this.MaxLength)
            {
                return new ValidationResult(Language.Instance.GetByCulture(Resources.Prefix.VERIFICATION, Resources.Key.CHARACTERS_MAXIMUM).ToFormat(displayName, this.MaxLength));
            }

            return ValidationResult.Success;
        }
    }
}
