using System;
using System.Windows.Input;

namespace Upsinator.Logic
{
    internal class RelayCommand : ICommand
    {
        private Action<object> _action;

        public RelayCommand(Action<object> action)
        {
            this._action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter != null)
            {
                this._action(parameter);
            }
            else
            {
                this._action("");
            }
        }
    }
}
