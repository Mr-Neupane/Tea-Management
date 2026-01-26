using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace TeaManagement.Data
{
    public static class DBCreation
    {
        public static void EnsurePostgresDatabaseCreated(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var dbName = builder.Database;

            // connect to postgres system db
            builder.Database = "postgres";

            using var conn = new NpgsqlConnection(builder.ConnectionString);
            conn.Open();

            // check if DB exists
            using var checkCmd = new NpgsqlCommand(
                "SELECT 1 FROM pg_database WHERE datname = @name", conn);
            checkCmd.Parameters.AddWithValue("name", dbName);

            var exists = checkCmd.ExecuteScalar();

            if (exists == null)
            {
                using var createCmd =
                    new NpgsqlCommand($"CREATE DATABASE \"{dbName}\"", conn);
                createCmd.ExecuteNonQuery();
                Console.WriteLine($"📌 Created database: {dbName}");
            }
            else
            {
                Console.WriteLine($"✔ Database exists: {dbName}");
            }

            // Apply EF migrations
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}