using Microsoft.Data.Sqlite;

namespace SimpleMediator.Benchmarks.Infrastructure;

/// <summary>
/// Helper to create SQLite schemas for Dapper benchmarks.
/// </summary>
public static class SqliteSchemaBuilder
{
    /// <summary>
    /// Creates the Outbox table schema.
    /// </summary>
    public static async Task CreateOutboxSchemaAsync(SqliteConnection connection)
    {
        var sql = @"
            CREATE TABLE IF NOT EXISTS OutboxMessages (
                Id TEXT PRIMARY KEY,
                NotificationType TEXT NOT NULL,
                Content TEXT NOT NULL,
                CreatedAtUtc TEXT NOT NULL,
                ProcessedAtUtc TEXT,
                ErrorMessage TEXT,
                RetryCount INTEGER NOT NULL DEFAULT 0,
                NextRetryAtUtc TEXT
            )";

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        await Task.Run(() => command.ExecuteNonQuery());
    }

    /// <summary>
    /// Creates the Inbox table schema.
    /// </summary>
    public static async Task CreateInboxSchemaAsync(SqliteConnection connection)
    {
        var sql = @"
            CREATE TABLE IF NOT EXISTS InboxMessages (
                MessageId TEXT PRIMARY KEY,
                RequestType TEXT NOT NULL,
                ReceivedAtUtc TEXT NOT NULL,
                ProcessedAtUtc TEXT,
                ExpiresAtUtc TEXT NOT NULL,
                Response TEXT,
                ErrorMessage TEXT,
                RetryCount INTEGER NOT NULL DEFAULT 0,
                NextRetryAtUtc TEXT,
                Metadata TEXT
            )";

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        await Task.Run(() => command.ExecuteNonQuery());
    }
}
