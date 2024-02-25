using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using food_order_dotnet.Resources;

namespace food_order_dotnet.DTO.Request
{
    public class CartRequest
    {
        [Required(ErrorMessageResourceName = "FoodIdRequired",  ErrorMessageResourceType = typeof(ValidationMessages))]
        [JsonPropertyName("food_id")]
        public int? FoodId { get; set; }
    }
}