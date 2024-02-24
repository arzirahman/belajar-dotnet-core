using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_order_dotnet.Models
{
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        public string? CategoryName { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("created_time")]
        public DateTime? CreatedTime { get; set; }

        [Column("modified_by")]
        public string? ModifiedBy { get; set; }

        [Column("modified_time")]
        public DateTime? ModifiedTime { get; set; }
    }
}
