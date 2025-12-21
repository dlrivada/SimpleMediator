using System.Reflection;
using MassTransit;

namespace SimpleMediator.MassTransit;

/// <summary>
/// Extension methods for configuring MassTransit bus with SimpleMediator consumers.
/// </summary>
public static class MassTransitBusConfiguratorExtensions
{
    /// <summary>
    /// Adds SimpleMediator request consumers for all IRequest types in the specified assemblies.
    /// </summary>
    /// <param name="configurator">The MassTransit bus registration configurator.</param>
    /// <param name="assemblies">The assemblies to scan for request types.</param>
    /// <returns>The configurator for chaining.</returns>
    public static IBusRegistrationConfigurator AddSimpleMediatorRequestConsumers(
        this IBusRegistrationConfigurator configurator,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(configurator);
        ArgumentNullException.ThrowIfNull(assemblies);

        foreach (var assembly in assemblies)
        {
            var requestTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)));

            foreach (var requestType in requestTypes)
            {
                var requestInterface = requestType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));
                var responseType = requestInterface.GetGenericArguments()[0];

                var consumerType = typeof(MassTransitRequestConsumer<,>).MakeGenericType(requestType, responseType);
                configurator.AddConsumer(consumerType);
            }
        }

        return configurator;
    }

    /// <summary>
    /// Adds SimpleMediator notification consumers for all INotification types in the specified assemblies.
    /// </summary>
    /// <param name="configurator">The MassTransit bus registration configurator.</param>
    /// <param name="assemblies">The assemblies to scan for notification types.</param>
    /// <returns>The configurator for chaining.</returns>
    public static IBusRegistrationConfigurator AddSimpleMediatorNotificationConsumers(
        this IBusRegistrationConfigurator configurator,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(configurator);
        ArgumentNullException.ThrowIfNull(assemblies);

        foreach (var assembly in assemblies)
        {
            var notificationTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Where(t => typeof(INotification).IsAssignableFrom(t));

            foreach (var notificationType in notificationTypes)
            {
                var consumerType = typeof(MassTransitNotificationConsumer<>).MakeGenericType(notificationType);
                configurator.AddConsumer(consumerType);
            }
        }

        return configurator;
    }

    /// <summary>
    /// Adds all SimpleMediator consumers (requests and notifications) for types in the specified assemblies.
    /// </summary>
    /// <param name="configurator">The MassTransit bus registration configurator.</param>
    /// <param name="assemblies">The assemblies to scan for message types.</param>
    /// <returns>The configurator for chaining.</returns>
    public static IBusRegistrationConfigurator AddSimpleMediatorConsumers(
        this IBusRegistrationConfigurator configurator,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(configurator);
        ArgumentNullException.ThrowIfNull(assemblies);

        configurator.AddSimpleMediatorRequestConsumers(assemblies);
        configurator.AddSimpleMediatorNotificationConsumers(assemblies);

        return configurator;
    }
}
