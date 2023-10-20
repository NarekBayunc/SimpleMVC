using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Data.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MaxDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private const string DefaultErrorMessageFormatString = "The field {0} must be a date no later than {1}.";

        public DateTime MaxDate { get; }

        public MaxDateAttribute(int yearsInTheFuture)
        {
            MaxDate = DateTime.Now.AddYears(yearsInTheFuture);
            ErrorMessage = DefaultErrorMessageFormatString;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MaxDate.ToShortDateString());
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-maxdate", errorMessage);
            MergeAttribute(context.Attributes, "data-val-maxdate-max", MaxDate.ToShortDateString());
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue <= MaxDate)
                {
                    return ValidationResult.Success!;
                }
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
