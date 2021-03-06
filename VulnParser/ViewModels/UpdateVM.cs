using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VulnParser.Views;
using VulnParser.Models;

namespace VulnParser.ViewModels
{
    public class UpdateVM : BaseVM
    {
        public string fileTempPathName = Path.GetTempPath() + fileName;
        public List<Vulnerability> oldVulnsList { get; set; }
        public List<Vulnerability> newVulnsList { get; set; }
        public ObservableCollection<Vulnerability> BeforeChangesList { get; set; } = new ObservableCollection<Vulnerability>();
        public ObservableCollection<Vulnerability> AfterChangesList { get; set; } = new ObservableCollection<Vulnerability>();

        private int countChanges;
        private string changesMessage;
        public string ChangesMessage
        {
            get => changesMessage;
            set
            {
                changesMessage = value;
                OnPropertyChanged();
            }
        }

        public UpdateVM(ref List<Vulnerability> oldVulnsList)
        {
            this.oldVulnsList = oldVulnsList;
            try
            {
                /*DownloadService.Download(downloadPath, pathName);
                if (File.Exists(pathName))
                {
                    newVulnsList = ParseExcelService.GetVulnsList(pathName);
                }*/
                
                DownloadService.Download(downloadPath, fileTempPathName);
                if (File.Exists(fileTempPathName))
                {
                    newVulnsList = ParseExcelService.GetVulnsList(fileTempPathName);
                }
            }
            catch (Exception)
            {
                throw;
            }

            Update();
            
            countChanges = BeforeChangesList.Count > AfterChangesList.Count ? BeforeChangesList.Count : AfterChangesList.Count;
            if (countChanges == 0)
            {
                changesMessage = "Изменения отсутствуют";
            }
            else
            {
                changesMessage = $"База успешно обновлена\nКоличество изменений: {countChanges}";
                oldVulnsList = newVulnsList;
            }
        }

        private void Update()
        {
            int minListCount = oldVulnsList.Count < newVulnsList.Count ? oldVulnsList.Count : newVulnsList.Count;
            for (int i = 0; i < minListCount; i++)
            {
                if (!oldVulnsList[i].Equals(newVulnsList[i]))
                {
                    BeforeChangesList.Add(oldVulnsList[i]);
                    AfterChangesList.Add(newVulnsList[i]);
                }
            }
            
            for (int i = minListCount; i < oldVulnsList.Count; i++)
                BeforeChangesList.Add(oldVulnsList[i]);
            
            for (int i = minListCount; i < newVulnsList.Count; i++)
                AfterChangesList.Add(newVulnsList[i]);
        }
    }
}
