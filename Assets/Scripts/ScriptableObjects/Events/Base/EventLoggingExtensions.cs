// Comment to hush
// #define VERBOSE
using UnityEngine;

internal static class EventLoggingExtensions
{
    internal static string BuildLogMessage(this string src, string eventName) =>
        $"{src} raised {eventName}";

    internal static void Log(this string msg)
    {
#if UNITY_EDITOR && VERBOSE
        Debug.Log(msg);
#endif
    }

    internal static void LogWarning(this string msg)
    {
#if UNITY_EDITOR
        Debug.LogWarning($"{msg} but no one listens.");
#endif
    }
}
