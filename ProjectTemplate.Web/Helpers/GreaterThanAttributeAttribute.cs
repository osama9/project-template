using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.Helpers
{
    public class GreaterThanAttributeAttribute : ValidationAttribute, IClientModelValidator
    {
        public string OtherProperty { get; set; }

        public bool AllowEqual { get; set; }
        public bool AllowMeEmpty { get; set; }
        public bool AllowOtherEmpty { get; set; }

        private string _otherPropertyDisplayName;

        public GreaterThanAttributeAttribute(string name, bool allowEqual = false, bool allowMeEmpty = false, bool allowOtherEmpty = false)
        {
            OtherProperty = name;
            AllowEqual = allowEqual;
            AllowMeEmpty = allowMeEmpty;
            AllowOtherEmpty = allowOtherEmpty;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var firstComparable = value as IComparable;
            var secondComparable = GetSecondComparable(validationContext);

            if (firstComparable != null && secondComparable != null)
            {
                if ((firstComparable.CompareTo(secondComparable) <= 0 && !AllowEqual) || (firstComparable.CompareTo(secondComparable) < 0))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        protected IComparable GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

                _otherPropertyDisplayName = GetOtherPropertyDisplayName(propertyInfo);

                return secondValue as IComparable;
            }

            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _otherPropertyDisplayName);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var otherPropertyInfo = context.ModelMetadata.ContainerType.GetProperty(OtherProperty);

            _otherPropertyDisplayName = GetOtherPropertyDisplayName(otherPropertyInfo);

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-greaterthan", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            context.Attributes.Add("data-val-greaterthan-otherfieldname", this.OtherProperty);
            context.Attributes.Add("data-val-greaterthan-allowequal", this.AllowEqual.ToString());
            context.Attributes.Add("data-val-greaterthan-allowmeempty", this.AllowMeEmpty.ToString());
            context.Attributes.Add("data-val-greaterthan-allowotherempty", this.AllowOtherEmpty.ToString());

        }

        private string GetOtherPropertyDisplayName(System.Reflection.PropertyInfo otherPropertyInfo)
        {
            var attributes = otherPropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);

            if (attributes == null)
                return OtherProperty;

            return (attributes.FirstOrDefault() as DisplayAttribute).GetName();
        }
    }
}
