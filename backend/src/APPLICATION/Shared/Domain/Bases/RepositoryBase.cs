using APPLICATION.Shared.Helpers;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.Domain.Bases
{
    public class RepositoryBase
    {
        private readonly IDbConnection _connection;
        protected readonly IMemoryCache _memoryCache;
        protected readonly string _path;

        public RepositoryBase(IDbConnection connection, IMemoryCache memoryCache)
        {
            _connection = connection;
            _memoryCache = memoryCache;
            _path = $"{GetType().Namespace}.Queries";
        }

        protected async Task<IEnumerable<T>> GetAsync<T>(string query, object filter, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryAsync<T>(new CommandDefinition(
                    parameters: filter,
                    commandText: query,
                    cancellationToken: cancellationToken
                    ));

                return result;
            }
            catch (SqlException)
            {
                return default;
            }
        }

        protected async Task<T> GetOneAsync<T>(string query, object filter, CancellationToken cancellationToken)
        {
            try
            {
                return await _connection.QueryFirstAsync<T>(new CommandDefinition(
                    parameters: filter,
                    commandText: query,
                    cancellationToken: cancellationToken
                    ));
            }
            catch (SqlException)
            {
                return default;
            }
        }

        protected async Task<int> ExecuteAsync(string query, object filter, CancellationToken cancellationToken)
        {
            try
            {
                return await _connection.ExecuteAsync(
                    new CommandDefinition(
                    parameters: filter,
                    commandText: query,
                    cancellationToken: cancellationToken
                    ));
            }
            catch (SqlException)
            {
                return default;
            }
        }

        protected async Task<IEnumerable<T>> GetBufferingAnyAsync<T>(string tag, string query, object filter, CancellationToken cancellationToken)
        {
            return await _memoryCache.GetOrCreate(tag, async (ctx) =>
            {
                try
                {
                    return await _connection.QueryAsync<T>(new CommandDefinition(
                        parameters: filter,
                        commandText: query,
                        cancellationToken: cancellationToken
                        ));
                }
                catch (SqlException)
                {
                    return default;
                }
            });
        }

        protected async Task<T> GetBufferingOneAsync<T>(string tag, string query, object filter, CancellationToken cancellationToken)
        {
            return await _memoryCache.GetOrCreate(tag, async (ctx) =>
            {
                return await _connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(
                    parameters: filter,
                    commandText: query,
                    cancellationToken: cancellationToken
                    ));
            });
        }

        protected async Task<T> GetBufferingOneAsync<T>(object key, string queryName, object filter, int ttlMinutes = 120, CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(key, async (cache) =>
            {
                var query = await _path.ReadFile(queryName, TypeArchives.SQL);

                cache.SlidingExpiration = TimeSpan.FromMinutes(ttlMinutes);

                return await _connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(
                    parameters: filter,
                    commandText: query,
                    cancellationToken: cancellationToken
                    ));
            });
        }

        protected async Task<T> GetBufferingOneConditionAsync<T>(object key, string queryName, object filter, Func<string, IEnumerable<T>, T> condition, string target, int ttlMinutes = 120, CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(key, async (cache) =>
            {
                var query = await _path.ReadFile(queryName, TypeArchives.SQL);

                cache.SlidingExpiration = TimeSpan.FromMinutes(ttlMinutes);

                var result = await _connection.QueryAsync<T>(new CommandDefinition(
                    parameters: filter,
                    commandText: query,
                    cancellationToken: cancellationToken
                    ));

                return condition(target, result);
            });
        }
    }
}