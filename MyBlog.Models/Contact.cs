using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Contact
    {
        [Key]
        public int RespondId { get; set; }
        [Required(ErrorMessage = "請輸入您的稱呼")]
        public string RespondName { get; set; }
        [Required(ErrorMessage = "請輸入您的Email")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email")]
        public string RespondEmail { get; set; }
        [Required(ErrorMessage = "請輸入您的留言")]
        [MaxLength(1024)]
        public string RespondMeaasge {  get; set; }
    }
}
