using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VulnParser.ViwModels;

namespace VulnParser.Views
{
    /// <summary>
    /// Логика взаимодействия для DownloadWIndow.xaml
    /// </summary>
    public partial class DownloadWIndow : Window
    {
        public DownloadWIndow()
        {
            InitializeComponent();
        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            downloadBtn.IsEnabled = false;
            
            try
            {
                using(WebClient webClient = new WebClient())
                {
                    webClient.DownloadFileAsync(new Uri(BaseVM.downloadPath), BaseVM.fileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}
