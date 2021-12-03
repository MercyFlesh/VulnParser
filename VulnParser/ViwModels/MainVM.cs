using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using VulnParser.Views;

namespace VulnParser.ViwModels
{
    public class MainVM : BaseVM
    {
        public MainVM()
        {
            if (!File.Exists(fileName))
            {
                new DownloadWIndow().ShowDialog();
            }
        }
    }
}
