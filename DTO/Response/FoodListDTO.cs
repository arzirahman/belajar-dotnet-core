using System.Text.Json.Serialization;

namespace food_order_dotnet.DTO.Response
{

    public class FoodListDTO
    {
        [JsonPropertyName("foodId")]
        public int FoodId { get; set; }

        [JsonPropertyName("categories")]
        public FoodCategoryDto? Category { get; set; }

        [JsonPropertyName("nama_makanan")]
        public string? FoodName { get; set; }

        [JsonPropertyName("harga")]
        public int? Price { get; set; }

        [JsonPropertyName("image_filename")]
        public string? ImageFilename { get; set; }

        [JsonPropertyName("is_cart")]
        public bool? IsCart { get; set; }

        [JsonPropertyName("is_favorite")]
        public bool? IsFavorite { get; set; }
    }
}