using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Models
{
    public class User
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public int Status { get; set; }

        [Required]
        public string ImageLink { get; set; }
    }
}
