using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_order_dotnet.Models
{
    public class Food
    {
        [Key]
        [Column("food_id")]
        public int FoodId { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("food_name")]
        public string? FoodName { get; set; }

        [Column("image_filename")]
        public string? ImageFilename { get; set; }

        [Column("price")]
        public int? Price { get; set; }

        [Column("ingridient")]
        public string? Ingredient { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("created_time")]
        public DateTime? CreatedTime { get; set; }

        [Column("modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("modified_time")]
        public DateTime? ModifiedTime { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
