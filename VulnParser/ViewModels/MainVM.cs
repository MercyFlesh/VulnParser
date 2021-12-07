using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using VulnParser.Models;
using VulnParser.Views;
using System.Windows;
using System.Windows.Input;
using VulnParser.ViewModels.Commands;
using System.ComponentModel;

namespace VulnParser.ViwModels
{
    public class MainVM : BaseVM
    {
        public List<Vulnerability> VulnerabilitiesList { get; set; } = new List<Vulnerability>();
        public ObservableCollection<Vulnerability> CurrentPage { get; set; } = new ObservableCollection<Vulnerability>();
        public RelayCommand NextPageClick { get; }
        public RelayCommand PrevPageClick { get; }
        
        private int currentCountItems;
        private int currentPageNum = 1;
        private int countPages;
        private Visibility colsFlag;

        public Visibility ColsFlag
        {
            get => colsFlag;
            set
            {
                colsFlag = value;
                OnPropertyChanged();
            }
        }

        public string CurrentCountItems
        {
            get => currentCountItems.ToString();
            set 
            { 
                currentCountItems = Convert.ToInt32(value);
                countPages = (int)Math.Ceiling(((double)VulnerabilitiesList.Count) / currentCountItems);
                UpdatePageCollection();
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            NextPageClick = new RelayCommand(
                (object param) => { currentPageNum++; UpdatePageCollection(); }, 
                (object param) => { return currentPageNum < countPages; }
            );
            PrevPageClick = new RelayCommand(
                (object param) => { currentPageNum--; UpdatePageCollection(); }, 
                (object param) => { return currentPageNum != 1; }
            );
            
            ColsFlag = Visibility.Hidden;            
            if (!File.Exists(fileName))
            {
                new DownloadWindow().ShowDialog();
            }

            if (File.Exists(fileName))
            {
                VulnerabilitiesList = ParseExcelService.GetVulnsList(fileName);
                CurrentPage = new ObservableCollection<Vulnerability>(PagedService<Vulnerability>
                    .GetCurrentPage(VulnerabilitiesList, currentCountItems, currentPageNum, countPages));
            }
        }

        /// <summary>
        /// obs collection isnt updated without this fuck...
        /// (╯°□°)╯ ┻━━┻
        /// </summary>
        public void UpdatePageCollection()
        {
            MessageBox.Show(currentPageNum.ToString());
            CurrentPage.Clear();
            foreach(var vuln in PagedService<Vulnerability>
                .GetCurrentPage(VulnerabilitiesList, currentCountItems, currentPageNum, countPages))
            {
                CurrentPage.Add(vuln);
            }
        }
    }
}