using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApi.Models
{
    public class Account
    {
        public string Id { get; set; }
        public float Balance { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime LatestDate { get; set; }
    }
}
