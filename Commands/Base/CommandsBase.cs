using System.Windows.Input;

namespace WpfApp1.Commands.Base
{
    public class CommandsBase : ICommand
    {
        private readonly Action<object?> _executeAction;
        private readonly Predicate<object?>? _canExecuteAction;

        public CommandsBase(Action<object?> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }
        public CommandsBase(Action<object?> executeAction, Predicate<object?> canExecute)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }
        public void Execute(object? parameter)
        {
            if (_executeAction != null)
            {
                _executeAction(parameter);
            }
        }
    }
}
