using System.Collections.Generic;
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

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
