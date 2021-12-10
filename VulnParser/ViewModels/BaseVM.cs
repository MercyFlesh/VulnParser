using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.IO;

namespace VulnParser.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public const string fileName = @"thrlist.xlsx";
        public const string downloadPath = @"https://bdu.fstec.ru/files/documents/" + fileName;
        public static string pathName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/" + fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
