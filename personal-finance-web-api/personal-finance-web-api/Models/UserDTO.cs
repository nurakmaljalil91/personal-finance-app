using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Models
{
    public class UserDTO
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }
}
