using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Category
    {
        [Key]
        [DisplayName("類別序號")]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("類別名稱")]
        public string CategoryName { get; set; }
    }
}
