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
        public string DisplayName { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public StringLengthAttribute() { }

        public override ValidationResult IsValid(ValidationContext context)
        {
            string propertyName = context.Property.Name;
            object propertyValue = context.Property.GetValue(context.Instance, null);

            propertyValue = propertyValue ?? "";

            int length = propertyValue.ToString().Length;

            string displayName = LanguageManager.Instance.Get(Resources.Prefix.DISPLAY_NAME, this.DisplayName ?? propertyName);

            if (this.MinLength > 0 && this.MaxLength > 0 && (!(length >= this.MinLength && length <= this.MaxLength)))
            {
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.CharactersRange).ToFormat(displayName, this.MinLength, this.MaxLength));
            }

            if (this.MinLength > 0 && this.MaxLength == 0 && length < this.MinLength)
            {
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.CharactersMinimum).ToFormat(displayName, this.MinLength));
            }

            if (this.MinLength == 0 && this.MaxLength > 0 && length > this.MaxLength)
            {
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.CharactersMaximum).ToFormat(displayName, this.MaxLength));
            }

            return ValidationResult.Success;
        }
    }
}
