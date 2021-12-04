using System.IO;
using System.Collections.ObjectModel;
using VulnParser.Models;
using VulnParser.Views;

namespace VulnParser.ViwModels
{
    public class MainVM : BaseVM
    {
        public ObservableCollection<Vulnerability> Vulnerabilities { get; set; } = new ObservableCollection<Vulnerability>();

        public MainVM()
        {
            if (!File.Exists(fileName))
            {
                new DownloadWindow().ShowDialog();
            }

            if (File.Exists(fileName))
            {
                Vulnerabilities = ParseExcelService.GetVulnsList(fileName);
            }
        }
    }
}