using System.Windows;
using VulnParser.ViwModels;
using VulnParser.Models;

namespace VulnParser.Views
{
    public partial class DownloadWIndow : Window
    {
        public DownloadWIndow()
        {
            InitializeComponent();
        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            downloadBtn.IsEnabled = false;
            DownloadService.Download(BaseVM.downloadPath, BaseVM.fileName);
            Close();
        }
    }
}
