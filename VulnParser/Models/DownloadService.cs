using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    webClient.DownloadFileAsync(new Uri(downloadAddr), filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }
    }
}
