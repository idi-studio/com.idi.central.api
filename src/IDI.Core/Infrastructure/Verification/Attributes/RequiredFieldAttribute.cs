using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    /// <summary>
    /// 必填验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredFieldAttribute : ValidationAttribute
    {
        public string DisplayName { get; set; }

        public RequiredFieldAttribute() { }

        public override ValidationResult IsValid(ValidationContext context)
        {
            string propertyName = context.Property.Name;
            object propertyValue = context.Property.GetValue(context.Instance, null);

            propertyValue = propertyValue ?? "";

            if (!string.IsNullOrEmpty(propertyValue.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                string displayName = LanguageManager.Instance.Get(Resources.Prefix.DISPLAY_NAME, this.DisplayName?? propertyName);

                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.Required).ToFormat(displayName));
            }
        }
    }
}
