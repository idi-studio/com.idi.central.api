using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;

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
                string displayName = Language.Instance.GetByCulture(Resources.Prefix.DISPLAY_NAME, this.DisplayName);

                return new ValidationResult(Language.Instance.GetByCulture(Resources.Prefix.VERIFICATION, Resources.Key.REQUIRED).ToFormat(displayName));
            }
        }
    }
}
