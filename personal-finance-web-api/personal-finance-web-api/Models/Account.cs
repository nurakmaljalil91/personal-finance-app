using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Models
{
    public class Account
    {
        public string Name { get; set; }
        public float Balance { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
