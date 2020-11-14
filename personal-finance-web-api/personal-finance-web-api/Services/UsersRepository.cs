using Newtonsoft.Json;
using personal_finance_web_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Services
{
    public class UsersRepository : BaseRepository
    {
        private string usersTable = "users";

        public UsersRepository(DbConnection connection) : base(connection) { }

        public async Task<IEnumerable<UserDTO>> ReadUsers()
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {usersTable};";
                command.CommandType = CommandType.Text;

                var query = new List<UserDTO>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            query.Add(new UserDTO
                            {
                                Fullname = reader.IsDBNull(reader.GetOrdinal("fullname")) ? string.Empty : reader.GetString(reader.GetOrdinal("fullname")),
                                Username = reader.IsDBNull(reader.GetOrdinal("username")) ? string.Empty : reader.GetString(reader.GetOrdinal("username")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email")),
                                Status = reader.IsDBNull(reader.GetOrdinal("status")) ? 0 : reader.GetInt32(reader.GetOrdinal("status"))
                            });
                        }
                    }
                    reader.Close();
                }
                return query;
            });
        }

        public async ValueTask<UserDTO> ReadUser(string userId)
        {
            return await WithConnection(async conn =>
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM {usersTable} WHERE user_id='{userId}';";
                command.CommandType = CommandType.Text;

                var user = new UserDTO();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {

                            user.Fullname = reader.IsDBNull(reader.GetOrdinal("fullname")) ? string.Empty : reader.GetString(reader.GetOrdinal("fullname"));
                            user.Username = reader.IsDBNull(reader.GetOrdinal("username")) ? string.Empty : reader.GetString(reader.GetOrdinal("username"));
                            user.Email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email"));
                            user.Status = reader.IsDBNull(reader.GetOrdinal("status")) ? 0 : reader.GetInt32(reader.GetOrdinal("status"));

                        }
                    }
                    else
                    {
                        user = null;
                    }
                    reader.Close();
                }
                return user;
            });
        }

        public async ValueTask<Message> CreateUser(User user)
        {
            return await WithConnection(async conn =>
            {

                var sqlString = $"INSERT INTO {usersTable}(user_id, fullname, username, password,email,status) " +
                $"VALUES ('{user.Id}','{user.Fullname}', '{user.Username}', '{user.Password}','{user.Email}',{user.Status}); ";

                using (DbCommand command = conn.CreateCommand())
                {
                    command.CommandText = sqlString;
                    command.CommandType = CommandType.Text;

                    await command.ExecuteNonQueryAsync();
                }

                var message = new Message();
                message.Status = "Success create new user";
                message.Created = DateTime.Now;

                return message;
            });
        }

        public async ValueTask<Message> UpdateUser(User user)
        {
            return await WithConnection(async conn =>
            {
                var checkId = $"SELECT EXISTS(SELECT 1 FROM {usersTable} WHERE user_id='{user.Id}')";
                var sqlString = $"UPDATE {usersTable} SET " +
                $"fullname = '{user.Fullname}', " +
                $"username = '{user.Username}', " +
                $"password = '{user.Password}', " +
                $"email = '{user.Email}' " +
                $" WHERE user_id = '{user.Id}'; ";

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
                        message.Status = $"Success update the user with user id {user.Id}";
                    }
                    else
                    {
                        message.Status = $"No user with user id {user.Id}";
                    }
                }

                message.Created = DateTime.Now;

                return message;
            });
        }

        public async ValueTask<Message> DeleteUser(string userId)
        {
            return await WithConnection(async conn =>
            {
                var checkId = $"SELECT EXISTS(SELECT 1 FROM {usersTable} WHERE user_id='{userId}')";
                var sqlString = $"DELETE FROM {usersTable} WHERE user_id='{userId}'";
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
                        message.Status = $"Success delete the user with user id {userId}";
                    }
                    else
                    {
                        message.Status = $"No user with user id {userId}";
                    }

                }

                message.Created = DateTime.Now;

                return message;
            });
        }
    }


}
