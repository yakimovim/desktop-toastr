using System;
using System.Collections.Generic;
using EdlinSoftware.Toastr.Configuration;
using EdlinSoftware.Toastr.Models.PositionCalculation;

namespace EdlinSoftware.Toastr.Models
{
    /// <summary>
    /// Controls life-cycle of notifications.
    /// </summary>
    public class NotificationsController : IDisposable
    {
        private readonly List<INotificationWindow> _notificationWindows = new List<INotificationWindow>(5);
        private readonly ActionsExecutor _actionsExecutor;
        private readonly IToastrConfiguration _configuration;
        private readonly Func<Notification, IToastrConfiguration, INotificationWindow> _windowCreator;
        private readonly PositionCalculator _positionCalculator;

        public NotificationsController(uint desktopWidth, uint desktopHeight, ActionsExecutor actionsExecutor, IToastrConfiguration configuration, Func<Notification, IToastrConfiguration, INotificationWindow> windowCreator)
        {
            if (actionsExecutor == null) throw new ArgumentNullException(nameof(actionsExecutor));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _actionsExecutor = actionsExecutor;
            _configuration = configuration;
            _windowCreator = windowCreator;
            _positionCalculator = PositionCalculatorFactory.GetCalculator(desktopWidth, desktopHeight, configuration.HorizontalOffset, configuration.VerticalOffset, configuration.Width, configuration.PositionClass);
        }

        public void AddNotification(Notification notification)
        {
            var window = _windowCreator(notification, _configuration);
            window.Expired += OnWindowExpired;

            _actionsExecutor.AddAction(() =>
            {
                if (_configuration.NewestOnTop)
                { _notificationWindows.Insert(0, window); }
                else
                { _notificationWindows.Add(window); }

                _positionCalculator.Recalculate(_notificationWindows);
            });
        }

        private void OnWindowExpired(INotificationWindow window)
        {
            window.Expired -= OnWindowExpired;

            _actionsExecutor.AddAction(() =>
            {
                _notificationWindows.Remove(window);

                _positionCalculator.Recalculate(_notificationWindows);
            });
        }

        public void Dispose()
        {
            _actionsExecutor.Dispose();
        }
    }

    public interface IPosition
    {
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; set; }
        double Height { get; }
    }

    public interface INotificationWindow : IPosition
    {
        event Action<INotificationWindow> Expired;
    }


}