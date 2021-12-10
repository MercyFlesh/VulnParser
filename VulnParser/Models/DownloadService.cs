using System;
using System.Net;
using System.Windows;

namespace VulnParser.Models
{
    public class DownloadService
    {
        public static void Download(string downloadAddr, string filePath)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(new Uri(downloadAddr), filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }
    }
}
