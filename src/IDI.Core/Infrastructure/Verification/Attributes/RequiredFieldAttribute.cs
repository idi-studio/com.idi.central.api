using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Localization;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    /// <summary>
    /// 必填验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredFieldAttribute : ValidationAttribute
    {
        public string DisplayName { get; set; }

        public RequiredFieldAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }

        public override ValidationResult IsValid(ValidationContext context)
        {
            object propertyValue = context.Property.GetValue(context.Instance, null);

            propertyValue = propertyValue ?? "";

            if (!string.IsNullOrEmpty(propertyValue.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                //return new ValidationResult($"'{this.DisplayName}'必填!");
                return new ValidationResult(Language.Instance.Get("required"));
            }
        }
    }
}
