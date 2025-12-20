using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Dapper.Oracle;
using SimpleMediator.Messaging;

namespace SimpleMediator.Dapper.Oracle.GuardTests;

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
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
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
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("configure");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with factory throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapperWithFactory_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        Func<IServiceProvider, System.Data.IDbConnection> factory = _ => Substitute.For<System.Data.IDbConnection>();
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, factory, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with factory throws ArgumentNullException when factory is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapperWithFactory_NullFactory_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Func<IServiceProvider, System.Data.IDbConnection> factory = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, factory, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("connectionFactory");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with factory throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapperWithFactory_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Func<IServiceProvider, System.Data.IDbConnection> factory = _ => Substitute.For<System.Data.IDbConnection>();
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, factory, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("configure");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection string throws ArgumentNullException when services is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapperWithConnectionString_NullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null!;
        var connectionString = "Data Source=localhost;User Id=test;Password=test;";
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, connectionString, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection string throws ArgumentNullException when connectionString is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapperWithConnectionString_NullConnectionString_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        string connectionString = null!;
        Action<MessagingConfiguration> configure = _ => { };

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, connectionString, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("connectionString");
    }

    /// <summary>
    /// Verifies that AddSimpleMediatorDapper with connection string throws ArgumentNullException when configure is null.
    /// </summary>
    [Fact]
    public void AddSimpleMediatorDapperWithConnectionString_NullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = "Data Source=localhost;User Id=test;Password=test;";
        Action<MessagingConfiguration> configure = null!;

        // Act & Assert
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorDapper(services, connectionString, configure);
        act.Should().Throw<ArgumentNullException>().WithParameterName("configure");
    }
}
