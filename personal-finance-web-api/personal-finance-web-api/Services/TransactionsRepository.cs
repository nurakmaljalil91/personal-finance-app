using personal_finance_web_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Services
{
    public class TransactionsRepository : BaseRepository
    {
        private string transactionTable = "transactions";
        private string accountsTable = "accounts";
        public TransactionsRepository(DbConnection connection) : base(connection) { }

        public async Task<IEnumerable<Transaction>> ReadTransactions()
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {transactionTable}";
                command.CommandType = CommandType.Text;

                var query = new List<Transaction>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            query.Add(new Transaction
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("transaction_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("transaction_id")),
                                AccountId = reader.IsDBNull(reader.GetOrdinal("account_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("transaction_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("transaction_name")),
                                Type = reader.IsDBNull(reader.GetOrdinal("transaction_type")) ? 3 : reader.GetInt16(reader.GetOrdinal("transaction_type")),
                                Place = reader.IsDBNull(reader.GetOrdinal("transaction_place")) ? string.Empty : reader.GetString(reader.GetOrdinal("transaction_place")),
                                Total = reader.IsDBNull(reader.GetOrdinal("total")) ? 0 : reader.GetFloat(reader.GetOrdinal("total")),
                                TransactionDate = reader.IsDBNull(reader.GetOrdinal("transaction_date")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("transaction_date")),
                                ImageLink = reader.IsDBNull(reader.GetOrdinal("image_link")) ? string.Empty : reader.GetString(reader.GetOrdinal("image_link"))

                            });
                        }
                    }
                    reader.Close();
                }
                return query;

            });
        }



        public async Task<IEnumerable<Transaction>> ReadTransactionsByAccountId(string accountId)
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {transactionTable} WHERE account_id='{accountId}'";
                command.CommandType = CommandType.Text;

                var query = new List<Transaction>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            query.Add(new Transaction
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("transaction_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("transaction_id")),
                                AccountId = reader.IsDBNull(reader.GetOrdinal("account_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("account_id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("transaction_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("transaction_name")),
                                Type = reader.IsDBNull(reader.GetOrdinal("transaction_type")) ? 3 : reader.GetInt16(reader.GetOrdinal("transaction_type")),
                                Place = reader.IsDBNull(reader.GetOrdinal("transaction_place")) ? string.Empty : reader.GetString(reader.GetOrdinal("transaction_place")),
                                Total = reader.IsDBNull(reader.GetOrdinal("total")) ? 0 : reader.GetFloat(reader.GetOrdinal("total")),
                                TransactionDate = reader.IsDBNull(reader.GetOrdinal("transaction_date")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("transaction_date")),
                                ImageLink = reader.IsDBNull(reader.GetOrdinal("image_link")) ? string.Empty : reader.GetString(reader.GetOrdinal("image_link"))
                            });
                        }
                    }
                    reader.Close();
                }
                return query;

            });
        }

        public async ValueTask<Message> CreateTransaction(Transaction transaction)
        {
            return await WithConnection(async conn =>
            {

                
                var getBalance = $"SELECT balance FROM {accountsTable} WHERE account_id='{transaction.AccountId}';";

                var date = transaction.TransactionDate.Date;
                var convertedDate = $"{date.Year}-{date.Month}-{date.Day}";

                var sqlString = $"INSERT INTO  {transactionTable} " +
                $"( transaction_id ,account_id ,transaction_name,transaction_type,expense_type,transaction_place,total,transaction_date ) " +
                $"VALUES ('{transaction.Id}','{transaction.AccountId}','{transaction.Name}','{transaction.Type}','{transaction.ExpenseType}'," +
                $" '{transaction.Place}', {transaction.Total}, '{convertedDate}'); ";

                float balance = 0;

                var exist = false;
                var message = new Message();
                using (DbCommand command = conn.CreateCommand())
                {

                    command.CommandText = getBalance;
                    command.CommandType = CommandType.Text;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                exist = true;
                                balance = reader.IsDBNull(reader.GetOrdinal("balance")) ? 0 : reader.GetFloat(reader.GetOrdinal("balance"));
                            }
                        }
                        else
                        {
                            exist = false;
                        }
                        reader.Close();
                    }

                    if (exist)
                    {
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                        if(transaction.Type == 0)
                        {
                            var newBalance = balance - transaction.Total;
                            command.CommandText = $"UPDATE {accountsTable} SET balance={newBalance} WHERE account_id='{transaction.AccountId}'; ";
                            command.CommandType = CommandType.Text;
                            await command.ExecuteNonQueryAsync();
                        }
                        else
                        {
                            var newBalance = balance + transaction.Total;
                            command.CommandText = $"UPDATE {accountsTable} SET balance={newBalance} WHERE account_id='{transaction.AccountId}'; ";
                            command.CommandType = CommandType.Text;
                            await command.ExecuteNonQueryAsync();
                        }
                        
                        message.Status = $"Succes created the transaction for account id {transaction.AccountId}";
                    }
                    else
                    {
                        message.Status = $"The account id of {transaction.AccountId} is not exist";
                    }

                }

                message.Created = DateTime.Now;

                return message;
            });
        }

        public async ValueTask<Message> UpdateTransaction(Transaction transaction)
        {
            return await WithConnection(async conn =>
            {

                var checkId = $"SELECT EXISTS(SELECT 1 FROM {transactionTable} WHERE transaction_id='{transaction.Id}')";
                var date = transaction.TransactionDate.Date;
                var convertedDate = $"{date.Year}-{date.Month}-{date.Day}";
                var sqlString = $"UPDATE {transactionTable} SET " +
                $"transaction_id= {transaction.Id}," +
                $"account_id={transaction.AccountId} ," +
                $"transaction_name={transaction.Name}," +
                $"transaction_type={transaction.Type}," +
                $"expense_type={transaction.ExpenseType}," +
                $"transaction_place={transaction.Place}," +
                $"total={transaction.Total}," +
                $"transaction_date= {convertedDate}, " +
                $"image_link='{transaction.ImageLink}';";

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
                        message.Status = $"Success update the transation with transaction id {transaction.Id}";
                    }
                    else
                    {
                        message.Status = $"No transaction with transaction id {transaction.Id}";
                    }
                }

                message.Created = DateTime.Now;

                return message;
            });
        }

        public async ValueTask<Message> DeleteTransaction(string transactionId)
        {
            return await WithConnection(async conn =>
            {
                var checkId = $"SELECT EXISTS(SELECT 1 FROM {transactionTable} WHERE transaction_id='{transactionId}')";
                var sqlString = $"DELETE FROM {transactionTable} WHERE transaction_id='{transactionId}'";
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
                        message.Status = $"Success delete the transaction with transaction id {transactionId}";
                    }
                    else
                    {

                        message.Status = $"No transaction with transaction id {transactionId}";

                    }

                }

                message.Created = DateTime.Now;

                return message;
            });
        }

    }
}
