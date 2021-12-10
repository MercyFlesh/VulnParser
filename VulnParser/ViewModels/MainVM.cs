using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using VulnParser.Models;
using VulnParser.Views;
using System.Windows;

namespace VulnParser.ViewModels
{
    public class MainVM : BaseVM
    {
        private List<Vulnerability> vulnerabilitiesList = new List<Vulnerability>();
        public List<Vulnerability> VulnerabilitiesList { get => vulnerabilitiesList; set => vulnerabilitiesList = value; }
        public ObservableCollection<Vulnerability> CurrentPage { get; private set; } = new ObservableCollection<Vulnerability>();
        public RelayCommand NextPageClick { get; }
        public RelayCommand PrevPageClick { get; }
        public RelayCommand HidenFullTable { get; }
        public RelayCommand VisibleFullTable { get; }
        public RelayCommand UpadteDB { get; }
        public RelayCommand SaveDB { get; }

        private int currentCountItems;
        private int currentPageNum;
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
                if (VulnerabilitiesList.Count == 0)
                {
                    CurrentPageNum = 0;
                    CountPages = 0;
                }
                else
                {
                    UpdateCurrentPagesNum();
                }

                UpdatePageCollection();
                OnPropertyChanged();
            }
        }

        public void UpdateCurrentPagesNum()
        {
            CurrentPageNum = 1;
            CountPages = (int)Math.Ceiling(((double)VulnerabilitiesList.Count) / currentCountItems);

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
                (object param) => { return CurrentPageNum > 1; }
            );

            HidenFullTable = new RelayCommand(
                o => { ColsFlag = Visibility.Hidden; },
                o => { return true; }
            );

            VisibleFullTable = new RelayCommand(
                o => { ColsFlag = Visibility.Visible; },
                o => { return true; }
            );

            UpadteDB = new RelayCommand(
                o => { UpdateDBList(); },
                o => { return true; }
            );

            SaveDB = new RelayCommand(
                o => { ParseExcelService.SaveVulnsList(VulnerabilitiesList); },
                o => { return true; }
            );

            if (!File.Exists(fileName))
            {
                new DownloadWindow().ShowDialog();
            }

            if (File.Exists(fileName))
            {
                ColsFlag = Visibility.Hidden;

                try
                {
                    VulnerabilitiesList = ParseExcelService.GetVulnsList(fileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error for readig excel: " + ex);
                }
            }
        }
        
        /// <summary>
        /// (╯°□°)╯ ┻━━┻
        /// </summary>
        private void UpdateDBList()
        {
            try
            {
                UpdateWindow updateWindow = new UpdateWindow(ref vulnerabilitiesList);
                updateWindow.Show();

                UpdateCurrentPagesNum();
                UpdatePageCollection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex);
            }
        }
        
        private void UpdatePageCollection()
        {
            CurrentPage.Clear();
            foreach (var vuln in PagedService<Vulnerability>
                .GetCurrentPage(VulnerabilitiesList, currentCountItems, currentPageNum, countPages))
            {
                Vulnerability copyVuln = vuln.Copy();
                if (ColsFlag == Visibility.Hidden)
                {
                    copyVuln.Id = copyVuln.Id.Insert(0, "УБИ.");
                }

                CurrentPage.Add(copyVuln);
            }
        }
    }
}
