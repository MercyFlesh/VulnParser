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
            countItemsOnPage.SelectedIndex = 0;  
            DataContext = new MainVM();
        }
    }
}
