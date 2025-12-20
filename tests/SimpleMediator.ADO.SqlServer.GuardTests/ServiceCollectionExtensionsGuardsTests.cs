using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Messaging;

namespace SimpleMediator.ADO.SqlServer.GuardTests;

/// <summary>
/// Guard tests for <see cref="ServiceCollectionExtensions"/> to verify null parameter handling.
/// </summary>
public class ServiceCollectionExtensionsGuardsTests
{
    /// <summary>
    /// Verifies that AddSimpleMediatorADO throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("configure");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO with connection string throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_WithConnectionString_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        var connectionString = "Server=localhost;Database=test;";
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, connectionString, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO with connection string throws ArgumentNullException when connectionString is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_WithConnectionString_NullConnectionString_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        string connectionString = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, connectionString, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("connectionString");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO with connection string throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_WithConnectionString_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = "Server=localhost;Database=test;";
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, connectionString, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("configure");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO with factory throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_WithFactory_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        Func<IServiceProvider, System.Data.IDbConnection> connectionFactory = _ => Substitute.For<System.Data.IDbConnection>();
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, connectionFactory, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO with factory throws ArgumentNullException when connectionFactory is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_WithFactory_NullConnectionFactory_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Func<IServiceProvider, System.Data.IDbConnection> connectionFactory = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, connectionFactory, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("connectionFactory");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorADO with factory throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorADO_WithFactory_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Func<IServiceProvider, System.Data.IDbConnection> connectionFactory = _ => Substitute.For<System.Data.IDbConnection>();
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorADO(services, connectionFactory, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("configure");
    }
}
