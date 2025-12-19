using Microsoft.EntityFrameworkCore;
using SimpleMediator.EntityFrameworkCore.Outbox;
using SimpleMediator.EntityFrameworkCore.Inbox;

namespace SimpleMediator.Benchmarks.Infrastructure;

/// <summary>
/// Shared DbContext for EF Core benchmarks.
/// Configures all messaging entities (Outbox, Inbox, Saga, Scheduling).
/// </summary>
public sealed class BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options) : DbContext(options)
{
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Outbox configuration
        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.ToTable("OutboxMessages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NotificationType).IsRequired();
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAtUtc).IsRequired();
            entity.Property(e => e.RetryCount).HasDefaultValue(0);
        });

        // Inbox configuration
        modelBuilder.Entity<InboxMessage>(entity =>
        {
            entity.ToTable("InboxMessages");
            entity.HasKey(e => e.MessageId);
            entity.Property(e => e.RequestType).IsRequired();
            entity.Property(e => e.ReceivedAtUtc).IsRequired();
            entity.Property(e => e.ExpiresAtUtc).IsRequired();
            entity.Property(e => e.RetryCount).HasDefaultValue(0);
        });
    }
}
