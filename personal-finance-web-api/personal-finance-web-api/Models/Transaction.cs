using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Models
{
    public class Transaction
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string ExpenseType { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public float Total { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
