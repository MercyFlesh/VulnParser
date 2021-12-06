using System.IO;
using System.Collections.ObjectModel;
using VulnParser.Models;
using VulnParser.Views;
using System.Windows;

namespace VulnParser.ViwModels
{
    public class MainVM : BaseVM
    {
        public ObservableCollection<Vulnerability> Vulnerabilities { get; set; } = new ObservableCollection<Vulnerability>();

        private string currentCountItems;
        public string CurrentCountItems
        {
            get => currentCountItems;
            set 
            { 
                currentCountItems = value; 
                OnPropertyChanged(nameof(CurrentCountItems)); 
            }
        }

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