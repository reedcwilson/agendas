using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WardAgendas
{
	/// <summary>
	/// An implementation of the common MVVM DelegateCommand pattern
	/// </summary>
	public class DelegateCommand : ICommand, INotifyPropertyChanged
	{
		Dispatcher dispatcherThread;
		private readonly Func<object, bool> _canExecute;
		private readonly Action<object> _execute;
		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute", "Cannot be null");

			_execute = execute;
			_canExecute = canExecute;
			if (Application.Current != null)
				dispatcherThread = Application.Current.Dispatcher;
			CommandManager.RequerySuggested += CommandManager_RequerySuggested;
		}

		void CommandManager_RequerySuggested(object sender, EventArgs e)
		{
			RaiseCanExecuteChanged();			
		}


		#region ICommand Implementation

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return ((this._canExecute == null) ? true : this._canExecute.Invoke(parameter));
		}

		public void Execute(object parameter)
		{
			this._execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			EventHandler canExecuteChanged = this.CanExecuteChanged;
			if (canExecuteChanged != null)
			{
				// since commands can be linked to UI elements we need to respect this when firing events.
				if (dispatcherThread != null)
					dispatcherThread.Invoke(canExecuteChanged, this, EventArgs.Empty);
				else
					canExecuteChanged(this, EventArgs.Empty);
			}
		}

		#endregion


		#region INotifyPropertyChanged Implementation

		public event PropertyChangedEventHandler PropertyChanged;
		public void RaisePropertyChanged(string e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(e));
			}
		}

		#endregion
	}
}
