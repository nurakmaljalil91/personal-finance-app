using personal_finance_web_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Services
{
    public class AccountsRepository : BaseRepository
    {
        private string accountsTable = "accounts";

        public AccountsRepository(DbConnection connection) : base(connection) { }

        public async Task<IEnumerable<AccountDTO>> ReadAccounts()
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {accountsTable}";
                command.CommandType = CommandType.Text;

                var query = new List<AccountDTO>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            query.Add(new AccountDTO
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("account_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("account_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_name")),
                                Balance = reader.IsDBNull(reader.GetOrdinal("balance")) ? 0 : reader.GetFloat(reader.GetOrdinal("balance")),
                                LastUpdate = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("updated_at"))

                            });
                        }
                    }
                    reader.Close();
                }
                return query;

            });
        }

        public async ValueTask<AccountDTO> ReadAccount(string accountId)
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {accountsTable} WHERE account_id='{accountId}'";
                command.CommandType = CommandType.Text;

                var account = new AccountDTO();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            account.Id = reader.IsDBNull(reader.GetOrdinal("account_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_id"));
                            account.Name = reader.IsDBNull(reader.GetOrdinal("account_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_name"));
                            account.Balance = reader.IsDBNull(reader.GetOrdinal("balance")) ? 0 : reader.GetFloat(reader.GetOrdinal("balance"));
                            account.LastUpdate = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("updated_at"));
                        }
                    }
                    reader.Close();
                }
                return account;

            });
        }

        public async Task<IEnumerable<AccountDTO>> ReadAccountsByUserId(string userId)
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {accountsTable} WHERE user_id='{userId}'";
                command.CommandType = CommandType.Text;

                var query = new List<AccountDTO>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            query.Add(new AccountDTO
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("account_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("account_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_name")),
                                Balance = reader.IsDBNull(reader.GetOrdinal("balance")) ? 0 : reader.GetFloat(reader.GetOrdinal("balance")),
                                LastUpdate = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("updated_at"))

                            });
                        }
                    }
                    reader.Close();
                }
                return query;

            });
        }

        public async ValueTask<Message> CreateAccount(Account account)
        {
            return await WithConnection(async conn =>
            {

                var checkId = $"SELECT EXISTS(SELECT 1 FROM {accountsTable} WHERE user_id='{account.UserId}')";
                var sqlString = $"INSERT INTO {accountsTable} (account_id ,user_id ,account_name ,balance ) VALUES " +
                $"('{account.Id}','{account.UserId}', '{account.Name}', {account.Balance});";
                var exist = false;
                var message = new Message();
                using (DbCommand command = conn.CreateCommand())
                {

                    command.CommandText = checkId;
                    command.CommandType = CommandType.Text;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                exist = reader.GetBoolean(reader.GetOrdinal("exists"));
                            }
                        }
                        reader.Close();
                    }

                    if (exist)
                    {
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                        message.Status = $"Succes created the account for user {account.UserId}";
                    }
                    else
                    {
                        message.Status = $"The user id of {account.UserId} is not exist";
                    }

                }

                message.Created = DateTime.Now;

                return message;
            });
        }

        public async ValueTask<Message> UpdateAccount(Account account)
        {
            return await WithConnection(async conn =>
            {

                var checkId = $"SELECT EXISTS(SELECT 1 FROM {accountsTable} WHERE account_id='{account.Id}')";
                var sqlString = $"UPDATE {accountsTable} SET user_id='{account.UserId}', account_name = '{account.Name}', " +
                $"balance={account.Balance} WHERE account_id='{account.Id}'; ";

                var succes = false;
                var message = new Message();
                using (DbCommand command = conn.CreateCommand())
                {

                    command.CommandText = checkId;
                    command.CommandType = CommandType.Text;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                succes = reader.GetBoolean(reader.GetOrdinal("exists"));
                            }
                        }
                        reader.Close();
                    }

                    if (succes)
                    {
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                        message.Status = $"Success update the account with account id {account.Id}";
                    }
                    else
                    {
                        message.Status = $"No account with account id {account.Id}";
                    }
                }

                message.Created = DateTime.Now;

                return message;
            });
        }

        public async ValueTask<Message> DeleteAccount(string accountId)
        {
            return await WithConnection(async conn =>
            {
                var checkId = $"SELECT EXISTS(SELECT 1 FROM {accountsTable} WHERE account_id='{accountId}')";
                var sqlString = $"DELETE FROM {accountsTable} WHERE account_id='{accountId}'";
                var succes = false;
                var message = new Message();
                using (DbCommand command = conn.CreateCommand())
                {

                    command.CommandText = checkId;
                    command.CommandType = CommandType.Text;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                succes = reader.GetBoolean(reader.GetOrdinal("exists"));
                            }
                        }
                        reader.Close();
                    }

                    if (succes)
                    {
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                        message.Status = $"Success delete the account with account id {accountId}";
                    }
                    else
                    {
                        
                        message.Status = $"No account with account id {accountId}";
                    }

                }

                message.Created = DateTime.Now;

                return message;
            });
        }


    }
}
