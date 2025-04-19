using System;
using System.Threading.Tasks;

namespace PuzzleLab.Web.Services.Ui;

public enum ToastType
{
    Success,
    Error,
    Info
}

public class ToastNotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string Message { get; set; } = string.Empty;
    public ToastType Type { get; set; }
    public int DurationMs { get; set; } = 5000;
}

public class ToastService
{
    public static event Action<ToastNotification>? OnToastAdded;

    public static void ShowSuccess(string message, string? title = null, int durationMs = 5000)
    {
        Show(message, title, ToastType.Success, durationMs);
    }

    public static void ShowError(string message, string? title = null, int durationMs = 5000)
    {
        Show(message, title, ToastType.Error, durationMs);
    }

    public static void ShowInfo(string message, string? title = null, int durationMs = 5000)
    {
        Show(message, title, ToastType.Info, durationMs);
    }

    private static void Show(string message, string? title, ToastType type, int durationMs)
    {
        var toast = new ToastNotification
        {
            Message = message,
            Title = title,
            Type = type,
            DurationMs = durationMs
        };

        OnToastAdded?.Invoke(toast);
    }
}