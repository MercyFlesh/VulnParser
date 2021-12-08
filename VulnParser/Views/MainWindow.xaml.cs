using System.Windows;
using VulnParser.ViwModels;

namespace VulnParser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
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
