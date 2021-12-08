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
        public RelayCommand HidenFullTable { get; }
        public RelayCommand VisibleFullTable { get; }
        
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
                UpdatePageCollection();
                OnPropertyChanged();
            }
        }

        public string CurrentCountItems
        {
            get => currentCountItems.ToString();
            set 
            { 
                currentCountItems = Convert.ToInt32(value);
                CurrentPageNum = 1;
                CountPages = (int)Math.Ceiling(((double)VulnerabilitiesList.Count) / currentCountItems);

                UpdatePageCollection();
                OnPropertyChanged();
            }
        }

        public int CurrentPageNum
        {
            get => currentPageNum;
            set
            {
                currentPageNum = value;
                OnPropertyChanged();
            }
        }

        public int CountPages
        {
            get => countPages;
            set
            {
                countPages = value;
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            NextPageClick = new RelayCommand(
                (object param) => { CurrentPageNum++; UpdatePageCollection(); }, 
                (object param) => { return CurrentPageNum < countPages; }
            );
            PrevPageClick = new RelayCommand(
                (object param) => { CurrentPageNum--; UpdatePageCollection(); }, 
                (object param) => { return CurrentPageNum != 1; }
            );

            HidenFullTable = new RelayCommand(
                o => { ColsFlag = Visibility.Hidden; },
                o => { return true; }
            );

            VisibleFullTable = new RelayCommand(
                o => { ColsFlag = Visibility.Visible; },
                o => { return true; }
            );

            if (!File.Exists(fileName))
            {
                new DownloadWindow().ShowDialog();
            }

            if (File.Exists(fileName))
            {
                ColsFlag = Visibility.Hidden;
                VulnerabilitiesList = ParseExcelService.GetVulnsList(fileName);
                //CurrentPage = new ObservableCollection<Vulnerability>(PagedService<Vulnerability>
                //    .GetCurrentPage(VulnerabilitiesList, currentCountItems, currentPageNum, countPages));
            }
        }

        /// <summary>
        /// (╯°□°)╯ ┻━━┻
        /// </summary>
        public void UpdatePageCollection()
        {
            CurrentPage.Clear();
            foreach(var vuln in PagedService<Vulnerability>
                .GetCurrentPage(VulnerabilitiesList, currentCountItems, currentPageNum, countPages))
            {
                if (ColsFlag == Visibility.Hidden)
                {
                    if (!vuln.Id.StartsWith("УБИ."))
                        vuln.Id = vuln.Id.Insert(0, "УБИ.");
                }
                else if (vuln.Id.StartsWith("УБИ."))
                    vuln.Id = vuln.Id.Remove(0, 4);

                CurrentPage.Add(vuln);
            }
        }
    }
}