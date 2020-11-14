using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace personal_finance_web_api.Services
{
    public abstract class BaseRepository
    {
        private readonly DbConnection _connection;

        protected BaseRepository(DbConnection connection)
        {
            _connection = connection;
        }

        // use for buffered queries that return a type
        protected async Task<T> WithConnection<T>(Func<DbConnection, Task<T>> getData)
        {
            try
            {
                await _connection.OpenAsync();
                return await getData(_connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName),
                    ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(
                    String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)",
                        GetType().FullName), ex);
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        protected async Task WithConnection(Func<DbConnection, Task> getData)
        {
            try
            {
                await _connection.OpenAsync();
                await getData(_connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName),
                    ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(
                    String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)",
                        GetType().FullName), ex);
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        protected async Task<TResult> WithConnection<TRead, TResult>(Func<DbConnection, Task<TRead>> getData,
            Func<TRead, Task<TResult>> process)
        {
            try
            {
                await _connection.OpenAsync();
                var data = await getData(_connection);
                return await process(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName),
                    ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(
                    String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)",
                        GetType().FullName), ex);
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
