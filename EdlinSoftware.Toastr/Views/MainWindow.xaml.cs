using System.Windows;
using EdlinSoftware.Toastr.Models;
using EdlinSoftware.Toastr.ModelViews;

namespace EdlinSoftware.Toastr.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(new Person());
        }
    }
}
