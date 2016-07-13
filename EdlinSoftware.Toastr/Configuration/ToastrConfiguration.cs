namespace EdlinSoftware.Toastr.Configuration
{
    public enum ToastrPositions
    {
        TopRight,
        TopLeft,
        TopFullWidth,
        TopCenter,
        BottomRight,
        BottomLeft,
        BottomFullWidth,
        BottomCenter
    }
    public enum ToastrShowMethods
    {
        FadeIn
    }
    public enum ToastrHideMethods
    {
        FadeOut
    }
    public enum ToastrEasings
    {
        Swing,
        Linear
    }

    public interface IToastrConfiguration
    {
        bool CloseButton { get; }
        bool NewestOnTop { get; }
        bool ProgressBar { get; }
        ToastrPositions PositionClass { get; }
        bool PreventDuplicates { get; }
        long TimeOut { get; }
        long ExtendedTimeOut { get; }
        ToastrShowMethods ShowMethod { get; }
        long ShowDuration { get; }
        ToastrEasings ShowEasing { get; }
        ToastrHideMethods HideMethod { get; }
        long HideDuration { get; }
        ToastrEasings HideEasing { get; }
    }

    public class ToastrConfiguration : IToastrConfiguration
    {
          public bool CloseButton { get; set; }
          public bool NewestOnTop { get; set; }
          public bool ProgressBar { get; set; }
          public ToastrPositions PositionClass { get; set; }
          public bool PreventDuplicates { get; set; }
          public long TimeOut { get; set; }
          public long ExtendedTimeOut { get; set; }
          public ToastrShowMethods ShowMethod { get; set; }
          public long ShowDuration { get; set; }
          public ToastrEasings ShowEasing { get; set; }
          public ToastrHideMethods HideMethod { get; set; }
          public long HideDuration { get; set; }
          public ToastrEasings HideEasing { get; set; }
    }
}