using System.Windows;
using System.Windows.Input;
using VulnParser.ViewModels;

namespace VulnParser.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
            countItemsOnPage.SelectedIndex = 0;
        }

        private void FullTableBtn_Click(object sender, RoutedEventArgs e)
        {
            FullTableBtn.IsEnabled = false;
            ShortTableBtn.IsEnabled = true;
        }

        private void ShortTableBtn_Click(object sender, RoutedEventArgs e)
        {
            FullTableBtn.IsEnabled = true;
            ShortTableBtn.IsEnabled = false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RollUpBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ExpandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
