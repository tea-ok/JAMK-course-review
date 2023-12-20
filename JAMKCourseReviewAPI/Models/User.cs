using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMKCourseReviewAPI.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email_address")]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }
    }
}