using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Category
    {
        [Key]
        [DisplayName("類別序號")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "類別名稱不能空白")]
        [DisplayName("類別名稱")]
        [MaxLength(50)]
        [RegularExpression(@"^[\u4e00-\u9fa5a-zA-Z0-9\s#]+$", ErrorMessage = "類別名稱不能包含特殊符號")]
        public string CategoryName { get; set; }
    }
}
