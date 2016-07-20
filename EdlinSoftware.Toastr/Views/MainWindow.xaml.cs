using System.Collections.Generic;
using System.IO;
using System.Windows;
using EdlinSoftware.Toastr.Configuration;
using EdlinSoftware.Toastr.Models;
using Newtonsoft.Json;

namespace EdlinSoftware.Toastr.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private NotificationsController _controller;

        public MainWindow()
        {
            InitializeComponent();

            var configurationDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText("configuration.json"));

            var configurationReader = new ToastrConfigurationReader();
            var configuration = configurationReader.Get(configurationDictionary);

            var actionsExecutor = new ActionsExecutor(Dispatcher);
            _controller = new NotificationsController(
                (uint)SystemParameters.PrimaryScreenWidth, 
                (uint)SystemParameters.PrimaryScreenHeight, 
                actionsExecutor, 
                configuration,
                (notification, config) =>
                {
                    var notificationWindow = new NotificationWindow(notification, config);
                    notificationWindow.Show();
                    return notificationWindow;
                });

            Closing += (sender, args) =>
            {
                actionsExecutor.Dispose();
            };
        }

        private void OnShowMessage(object sender, RoutedEventArgs e)
        {
            _controller.AddNotification(new Notification
            {
                Type = NotificationType.Success,
                Title = "Information",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum"
            });
        }
    }
}
