using System;
using System.Collections.Generic;
using System.Windows;
using VulnParser.ViewModels;


namespace VulnParser.Views
{
    public partial class UpdateWindow : Window
    {
        public UpdateWindow(ref List<Vulnerability> oldVulnsList)
        {
            UpdateVM update = null;
            try
            {
                update = new UpdateVM(ref oldVulnsList);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Update error: " + ex);
                Close();
            }
            
            InitializeComponent();
            DataContext = update;
        }
    }
}
