namespace EdlinSoftware.Toastr.Models
{
    /// <summary>
    /// Types of Toastr messages.
    /// </summary>
    public enum NotificationType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public interface INotification
    {
        NotificationType Type { get; }
        string Title { get; }
        string Message { get; }
    }

    public struct Notification : INotification
    {
        public NotificationType Type { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}