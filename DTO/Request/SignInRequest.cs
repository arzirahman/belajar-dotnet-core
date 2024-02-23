using System.ComponentModel.DataAnnotations;
using food_order_dotnet.Resources;

namespace food_order_dotnet.DTO.Request 
{
    public class SignInRequest 
    {
        [Required(ErrorMessageResourceName = "UsernameRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        public string? Username { get; set; }
        
        [Required(ErrorMessageResourceName = "PasswordRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        [MinLength(6, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string? Password { get; set; }
    }
}