using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Localization;
using IDI.Core.Localization.Packages;

namespace IDI.Core.Infrastructure.Verification.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CompareAttribute : ValidationAttribute
    {
        private readonly Dictionary<CompareMethod, string> messages;
        private readonly Dictionary<CompareMethod, Func<IComparable, IComparable, bool>> functions;

        public string DisplayName { get; private set; }

        public string OtherDisplayName { get; private set; }

        public string OtherProperty { get; private set; }

        public CompareMethod Method { get; private set; }

        public CompareAttribute(CompareMethod method, string otherProperty)
        {
            this.OtherProperty = otherProperty;
            this.Method = method;

            messages = new Dictionary<CompareMethod, string>
            {
                { CompareMethod.EqualTo, Resources.Key.Verification.EqualTo },
                { CompareMethod.LessThan, Resources.Key.Verification.LessThan },
                { CompareMethod.LessThanOrEqualTo, Resources.Key.Verification.LessThanOrEqualTo },
                { CompareMethod.GreaterThan, Resources.Key.Verification.GreaterThan },
                { CompareMethod.GreaterThanOrEqualTo, Resources.Key.Verification.GreaterThanOrEqualTo }
            };

            functions = new Dictionary<CompareMethod, Func<IComparable, IComparable, bool>>
            {
                { CompareMethod.EqualTo, EqualTo },
                { CompareMethod.LessThan, LessThan },
                { CompareMethod.LessThanOrEqualTo, LessThanOrEqualTo },
                { CompareMethod.GreaterThan, GreaterThan },
                { CompareMethod.GreaterThanOrEqualTo, GreaterThanOrEqualTo }
            };
        }

        public override ValidationResult IsValid(ValidationContext context)
        {
            this.DisplayName = LanguageManager.Instance.Get(Resources.Prefix.DISPLAY_NAME, context.Property.Name);
            this.OtherDisplayName = LanguageManager.Instance.Get(Resources.Prefix.DISPLAY_NAME, OtherProperty);

            object propA = context.Property.GetValue(context.Instance, null);
            object propB = context.Instance.GetType().GetProperty(OtherProperty).GetValue(context.Instance, null);

            propA = propA ?? "";
            propB = propB ?? "";

            if (propA.GetType() != propB.GetType())
                return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.CannotCompare).ToFormat(this.DisplayName, this.OtherDisplayName));

            if (propA.TypeOf<DateTime>())
                return Compare((DateTime)propA, (DateTime)propB);

            if (propA.TypeOf<TimeSpan>())
                return Compare((DateTime)propA, (DateTime)propB);

            return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, Resources.Key.Verification.CannotCompare).ToFormat(this.DisplayName, this.OtherDisplayName));
        }

        private ValidationResult Compare(IComparable a, IComparable b)
        {
            if (functions[this.Method](a, b))
                return ValidationResult.Success;

            return new ValidationResult(LanguageManager.Instance.Get(Resources.Prefix.VERIFICATION, messages[this.Method]).ToFormat(this.DisplayName, this.OtherDisplayName));
        }

        private bool EqualTo<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) == 0;
        }

        private bool LessThan<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) == -1;
        }

        private bool LessThanOrEqualTo<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) == 0 || a.CompareTo(b) == -1;
        }

        private bool GreaterThan<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) == 1;
        }

        private bool GreaterThanOrEqualTo<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) == 0 || a.CompareTo(b) == 1;
        }
    }
}
