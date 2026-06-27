using Microsoft.Extensions.Options;
using OptionPatternDatabaseExample.Domain.Entities;
using OptionPatternDatabaseExample.Domain.Interfaces;
using OptionPatternDatabaseExample.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OptionPatternDatabaseExample.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseOptions _dbOptions;

        public UserRepository(IOptions<DatabaseOptions> dbOptions)
        {
            _dbOptions = dbOptions.Value;
        }

        public async Task<List<User>> GetActiveUsersAsync()
        {
            using var connection = new SqlConnection(_dbOptions.ConnectionString);

            const string sql = "SELECT Id, Name, Email, CreatedAt, IsActive FROM Users WHERE IsActive = 1";
            var users = await connection.QueryAsync<User>(sql);
            return users.ToList();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_dbOptions.ConnectionString);
            const string sql = "SELECT Id, Name, Email, CreatedAt, IsActive FROM Users WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task AddAsync(User user)
        {
            using var connection = new SqlConnection(_dbOptions.ConnectionString);
            const string sql = @"
                INSERT INTO Users (Name, Email, CreatedAt, IsActive) 
                VALUES (@Name, @Email, @CreatedAt, @IsActive)";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            using var connection = new SqlConnection(_dbOptions.ConnectionString);
            const string sql = @"
                UPDATE Users 
                SET Name = @Name, Email = @Email, IsActive = @IsActive 
                WHERE Id = @Id";
            await connection.ExecuteAsync(sql, user);
        }
    }
}
