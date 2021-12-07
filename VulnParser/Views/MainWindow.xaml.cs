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
    }
}
