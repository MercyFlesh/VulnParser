﻿using System.Windows;
using VulnParser.ViewModels;
using VulnParser.Models;

namespace VulnParser.Views
{
    /// <summary>
    /// Логика взаимодействия для DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public DownloadWindow()
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
