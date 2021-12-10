using System.Windows;
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
    }
}
