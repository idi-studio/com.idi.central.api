using System;
using System.ComponentModel.DataAnnotations;

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
                return new ValidationResult($"'{this.DisplayName}'最长{this.MinLength}-{this.MaxLength}个字符!");
            }

            if (this.MinLength > 0 && this.MaxLength == 0 && length < this.MinLength)
            {
                return new ValidationResult($"'{this.DisplayName}'最小{this.MinLength}个字符!");
            }

            if (this.MinLength == 0 && this.MaxLength > 0 && length > this.MaxLength)
            {
                return new ValidationResult($"'{this.DisplayName}'最长{this.MaxLength}个字符!");
            }

            return ValidationResult.Success;
        }
    }
}
