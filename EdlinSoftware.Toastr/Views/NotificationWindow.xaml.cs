using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EdlinSoftware.Toastr.Configuration;
using EdlinSoftware.Toastr.Models;
using Timer = System.Timers.Timer;

namespace EdlinSoftware.Toastr.Views
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : INotificationWindow
    {
        private readonly IToastrConfiguration _config;
        private readonly Timer _timer;

        public NotificationWindow()
        {
            InitializeComponent();
        }

        public NotificationWindow(Notification notification, IToastrConfiguration config)
        {
            InitializeComponent();

            _config = config;

            MessageText.Text = notification.Message;
            TitleText.Text = notification.Title ?? string.Empty;
            TitleText.Visibility = string.IsNullOrWhiteSpace(notification.Title)
                ? Visibility.Collapsed
                : Visibility.Visible;
            CloseButton.Visibility = !config.CloseButton
                ? Visibility.Collapsed
                : Visibility.Visible;

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri($"pack://application:,,,/EdlinSoftware.Toastr;component/Images/{notification.Type}.png");
            logo.EndInit();
            TypeImage.Source = logo;

            _timer = new Timer
            {
                Interval = config.TimeOut
             };
            _timer.Elapsed += OnTimer;
            _timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            CloseNotificationWindow();
        }

        private void CloseNotificationWindow()
        {
            lock (this)
            {
                _timer.Stop();
                _timer.Elapsed -= OnTimer;
                _timer.Dispose();
            }
            Dispatcher.Invoke(Close);
            Expired?.Invoke(this);
        }

        double IPosition.Width
        {
            get { return ActualWidth; }
            set { Width = value; }
        }

        double IPosition.Height => (int) ActualHeight;

        public event Action<INotificationWindow> Expired;

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            lock (this)
            {
                if (_timer.Enabled)
                {
                    _timer.Stop();
                    _timer.Elapsed -= OnTimer;
                }
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            lock (this)
            {
                _timer.Interval = _config.ExtendedTimeOut;
                _timer.Elapsed += OnTimer;
                _timer.Start();
            }
        }

        private void OnCloseClick(object sender, MouseButtonEventArgs e)
        {
            CloseNotificationWindow();
        }
    }
}
