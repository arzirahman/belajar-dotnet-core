using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_order_dotnet.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Column("username")]
        public string? Username { get; set; }

        [StringLength(100)]
        [Column("fullname")]
        public string? Fullname { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("created_time")]
        public DateTime? CreatedTime { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Column("modified_by")]     
        public string? ModifiedBy { get; set; }

        [Column("modified_time")] 
        public DateTime? ModifiedTime { get; set; }

        public void HashPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public bool VerifyPassword(string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, Password);
        }
    }
}