using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Messaging;

namespace SimpleMediator.Dapper.Sqlite.GuardTests;

/// <summary>
/// Guard tests for <see cref="ServiceCollectionExtensions"/> to verify null parameter handling.
/// </summary>
public class ServiceCollectionExtensionsGuardsTests
{
    /// <summary>
    /// Verifies that AddSimpleMediatorDapper throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection factory throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_WithFactory_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        Func<IServiceProvider, System.Data.IDbConnection> connectionFactory = _ => null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(connectionFactory, configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection factory throws ArgumentNullException when connectionFactory is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_WithFactory_NullConnectionFactory_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Func<IServiceProvider, System.Data.IDbConnection> connectionFactory = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(connectionFactory, configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection factory throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_WithFactory_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Func<IServiceProvider, System.Data.IDbConnection> connectionFactory = _ => null!;
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(connectionFactory, configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection string throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_WithConnectionString_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        var connectionString = "Data Source=test.db";
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(connectionString, configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection string throws ArgumentNullException when connectionString is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_WithConnectionString_NullConnectionString_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        string connectionString = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(connectionString, configure);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection string throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapper_WithConnectionString_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = "Data Source=test.db";
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => services.AddSimpleMediatorDapper(connectionString, configure);
        act.Should().Throw<ArgumentNullException>();
    }
}
