using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace IDI.Core.Infrastructure.Verification
{
    public sealed class Validator<T> where T : IVerifiable
    {
        private HashSet<string> _errors;

        public T Instance { get; private set; }

        public Type ObjectType { get; private set; }

        public Validator(T instance)
        {
            this._errors = new HashSet<string>();
            this.Instance = instance;
            this.ObjectType = instance.GetType();
        }

        public bool IsValid(out List<string> errors)
        {
            bool valid = true;

            var properties = this.ObjectType.GetProperties();

            foreach (var property in properties)
            {
                valid &= Valid(property);
            }

            errors = _errors.ToList();

            return valid;
        }

        private bool Valid(PropertyInfo property)
        {
            var valid = true;

            var attributes = property.GetCustomAttributes().Where(attribute => attribute.GetType().GetTypeInfo().BaseType == typeof(Attributes.ValidationAttribute)).ToList();

            if (attributes.Count == 0)
                return valid;

            foreach (var attribute in attributes)
            {
                var att = attribute as Attributes.ValidationAttribute;

                if (!att.Enabled(this.Instance.Group))
                    continue;

                if (att != null)
                {
                    var context = new ValidationContext(this.Instance, property);

                    var result = att.IsValid(context);

                    if (result != ValidationResult.Success)
                    {
                        this._errors.Add(result.ErrorMessage);
                        valid &= false;
                    }
                }
            }

            return valid;
        }
    }
}
