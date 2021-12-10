using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VulnParser.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public static string fileName = "thrlist.xlsx";
        public static string downloadPath = @"https://bdu.fstec.ru/files/documents/" + fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
