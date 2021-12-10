﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VulnParser.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public const string fileName = "thrlist.xlsx";
        public static string localPath = @"";
        public const string downloadPath = @"https://bdu.fstec.ru/files/documents/" + fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
