using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace App.Services.Utils;

public static class JSON
{
    public static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
        AllowTrailingCommas = true,
    };

    public static T Parse<T>(string data)
    {
        return JsonSerializer.Deserialize<T>(data, Default)
            ?? throw new InvalidDataException($"Unable to deserialize JSON into {typeof(T).Name}.");
    }

    public static string Stringify<T>(T value)
    {
        return JsonSerializer.Serialize(value, Default);
    }
}

public class Logger<T>
{
    private readonly string Name;

    public Logger()
    {
        Name = typeof(T).Name;
    }

    private string BuildLog(LogLevel logLevel, string caller, params string[] args)
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append($"[{Name}->{caller}][{logLevel}]");
        string TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ssZ");
        stringBuilder.AppendLine($"[{TimeStamp}]:");
        foreach (var arg in args)
        {
            stringBuilder.AppendLine(arg);
        }
        return stringBuilder.ToString();
    }

    private void _Log(string caller, LogLevel logLevel = LogLevel.Trace, params string[] args)
    {
        string FinalMessage = BuildLog(logLevel, caller, args);
        Console.WriteLine(FinalMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Log(LogLevel logLevel, params string[] args)
    {
        string caller =
            new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "Unknown";
        _Log(caller, logLevel, args);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Log(params string[] args)
    {
        string caller =
            new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "Unknown";
        _Log(caller, LogLevel.Trace, args);
    }
}

public enum SuccessStatusCode
{
    Ok = 200,
    Created = 201,
    Accepted = 202,
    NoContent = 204,
}

public enum ErrorStatusCode
{
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    RequestTimeout = 408,
    InternalServerError = 500,
    GatewayTimeout = 504,
}

public sealed class ReturnType<T>(bool _Ok, int _HttpStatusCode, T? _Data, string? _ErrorMessage)
{
    public bool Ok { get; init; } = _Ok;
    public T? Data { get; set; } = _Data;
    public string? ErrorMessage { set; get; } = _ErrorMessage;
    public int HttpStatusCode { get; set; } = _HttpStatusCode;
    public static readonly Logger<ReturnType<T>> Logger = new();

    public static ReturnType<T> Success(
        T Data,
        SuccessStatusCode HttpStatusCode = SuccessStatusCode.Ok
    )
    {
        return new(true, (int)HttpStatusCode, Data, null);
    }

    public static ReturnType<T> Failure(
        string ErrorMessage,
        ErrorStatusCode HttpStatusCode = ErrorStatusCode.InternalServerError
    )
    {
        return new(false, (int)HttpStatusCode, default, ErrorMessage);
    }
}
