using System;
using System.ComponentModel.DataAnnotations;

namespace food_order_dotnet.DTO.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsEqualAttribute(string otherPropertyName) : ValidationAttribute
    {
        private readonly string _otherPropertyName = otherPropertyName;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || validationContext == null)
            {
                return ValidationResult.Success;
            }

            var propertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName) 
                ?? throw new ArgumentException($"Property {_otherPropertyName} not found");
            
            var otherPropertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);
            if (!string.Equals(value, otherPropertyValue))
            {
                var errorMessage = new ValidationResult(ErrorMessageString, [validationContext.MemberName!]);
                return errorMessage;
            }

            return ValidationResult.Success;
        }
    }
}
