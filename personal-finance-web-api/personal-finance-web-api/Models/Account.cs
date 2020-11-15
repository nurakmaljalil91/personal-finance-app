using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Models
{
    public class Account
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.00, 9999999999.00, ErrorMessage = "Price must be between 0.00 and 9999999999.00")]
        public float Balance { get; set; }
        [Required]
        public string ImageLink { get; set; }

    }
}
