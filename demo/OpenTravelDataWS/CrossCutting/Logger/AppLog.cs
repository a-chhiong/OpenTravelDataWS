using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace CrossCutting.Logger;

/// <summary>
/// 用來記錄全系統的訊息
/// </summary>
public static class AppLog
{
    private static ILoggerFactory? _factory;
    private static Func<string?>? _traceIdResolver;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="traceIdResolver"></param>
    public static void Initialize(
        ILoggerFactory factory,
        Func<string?>? traceIdResolver = null)
    {
        _factory = factory;
        _traceIdResolver = traceIdResolver;
    }

    private static ILogger GetLogger(string category) => 
        _factory?.CreateLogger(category) ?? throw new InvalidOperationException("AppLog, Not Initialized");

    private static string FormatMessage(
        string message,
        string? callerPath)
    {
        var traceId = _traceIdResolver?.Invoke();
        var fileName = Path.GetFileName(callerPath ?? "Unknown");
        
        if (string.IsNullOrEmpty(traceId)) 
            return $"{fileName} || TraceId : (none) || {message}";
        
        // Attach TraceId as an event property for NLog
        using (NLog.ScopeContext.PushProperty("TraceId", traceId))
        {
            // Return formatted message
            return $"{fileName} || TraceId : {traceId} || {message}";
        }
    }

    // ===== LOG METHODS =====

    /// <summary>
    /// 一般訊息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="category"></param>
    /// <param name="callerPath"></param>
    public static void Info(
        string message,
        string category = "App",
        [CallerFilePath] string? callerPath = null)
        => GetLogger(category).LogInformation(FormatMessage(message, callerPath));

    /// <summary>
    /// 偵錯用
    /// </summary>
    /// <param name="message"></param>
    /// <param name="category"></param>
    /// <param name="callerPath"></param>
    public static void Debug(
        string message,
        string category = "App",
        [CallerFilePath] string? callerPath = null)
        => GetLogger(category).LogDebug(FormatMessage(message, callerPath));

    /// <summary>
    /// 警告用
    /// </summary>
    /// <param name="message"></param>
    /// <param name="category"></param>
    /// <param name="callerPath"></param>
    public static void Warn(
        string message,
        string category = "App",
        [CallerFilePath] string? callerPath = null)
        => GetLogger(category).LogWarning(FormatMessage(message, callerPath));

    /// <summary>
    /// 嚴重錯誤
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    /// <param name="category"></param>
    /// <param name="callerPath"></param>
    public static void Error(
        string message,
        Exception? ex = null,
        string category = "App",
        [CallerFilePath] string? callerPath = null)
        => GetLogger(category).LogError(ex, FormatMessage(message, callerPath));
}
