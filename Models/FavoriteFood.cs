using System.ComponentModel.DataAnnotations.Schema;

namespace food_order_dotnet.Models
{
    public class FavoriteFood
    {
        [Column("food_id")]
        public int? FoodId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("is_favorite")]
        public bool? IsFavorite { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("created_time")]
        public DateTime? CreatedTime { get; set; }

        [Column("modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("modified_time")]
        public DateTime? ModifiedTime { get; set; }

        [ForeignKey("FoodId")]
        public Food? Food { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
