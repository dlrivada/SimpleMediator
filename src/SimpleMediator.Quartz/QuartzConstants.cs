namespace SimpleMediator.Quartz;

/// <summary>
/// Constants used for Quartz job data keys.
/// </summary>
internal static class QuartzConstants
{
    /// <summary>
    /// Key for storing requests in JobDataMap.
    /// </summary>
    internal const string RequestKey = "SimpleMediator.Request";

    /// <summary>
    /// Key for storing notifications in JobDataMap.
    /// </summary>
    internal const string NotificationKey = "SimpleMediator.Notification";
}
