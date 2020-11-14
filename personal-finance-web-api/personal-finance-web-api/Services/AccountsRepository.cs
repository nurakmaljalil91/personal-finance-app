using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Services
{
    public class AccountsRepository : BaseRepository
    {
        private string accountsTable = "accounts";

        public AccountsRepository(DbConnection connection) : base(connection) { }


    }
}
