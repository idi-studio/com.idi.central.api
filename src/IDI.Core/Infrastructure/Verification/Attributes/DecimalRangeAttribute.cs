using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    /// <summary>
    /// 小数范围验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class DecimalRangeAttribute : ValidationAttribute
    {
        public string DisplayName { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public DecimalRangeAttribute() { }

        public override ValidationResult IsValid(ValidationContext context)
        {
            string propertyName = context.Property.Name;
            object propertyValue = context.Property.GetValue(context.Instance, null);

            propertyValue = propertyValue ?? 0M;

            decimal value = Decimal.Parse(propertyValue.ToString());
            decimal minimum = (decimal)this.Minimum;
            decimal maximum = (decimal)this.Maximum;

            string displayName = LanguageManager.Instance.Get(Resources.Prefix.DISPLAY_NAME, this.DisplayName ?? propertyName);

            if (minimum > 0 && maximum > 0 && (!(value >= minimum && value <= maximum)))
            {
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.DecimalRange).ToFormat(displayName, minimum, maximum));
            }

            if (minimum > 0 && maximum == 0 && value < minimum)
            {
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.DecimalMinimum).ToFormat(displayName, minimum));
            }

            if (minimum == 0 && maximum > 0 && value > maximum)
            {
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.DecimalMaximum).ToFormat(displayName, maximum));
            }

            return ValidationResult.Success;
        }
    }
}
