using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VulnParser.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        public readonly Action<object> action;
        public readonly Action<object> execute;
        public readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object param)
        {
            if (execute != null)
                execute(param);
        }

        public bool CanExecute(object param)
        {
            return canExecute == null || canExecute(param);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}