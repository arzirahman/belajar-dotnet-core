using System.ComponentModel.DataAnnotations;
using food_order_dotnet.DTO.Validation;
using food_order_dotnet.Resources;

namespace food_order_dotnet.DTO.Request 
{
    public class SignUpRequest 
    {
        [Required(ErrorMessageResourceName = "UsernameRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        public string? Username { get; set; }

        [Required(ErrorMessageResourceName = "FullnameRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        public string? Fullname { get; set; }
        
        [Required(ErrorMessageResourceName = "PasswordRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        [MinLength(6, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).+$", ErrorMessageResourceName = "PasswordRegex", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string? Password { get; set; }

        [Required(ErrorMessageResourceName = "RetypePasswordRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        [IsEqual(nameof(Password), ErrorMessageResourceName = "PasswordNotMatch", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string? RetypePassword { get; set; }
    }
}