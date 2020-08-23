using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApi.Models
{
    public class Transaction
    {
        public string Id{ get; set; }

        public string AccountId{ get; set; }

        [DataType(DataType.Date)]
        public DateTime TransDate{ get; set; }

        public float amount{ get; set; }

        public int TransType{ get; set; }

        public string Type{ get; set; }

        public string Desc{ get; set; }
    }
}
