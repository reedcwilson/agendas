using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardAgendas
{
	public class ViewModelLocator
	{
		public static MainWindowViewModel MainWindowViewModel
		{
			get
			{
				if (_mainWindowViewModel == null)
				{
					_mainWindowViewModel = new MainWindowViewModel();
				}
				return _mainWindowViewModel;
			}
		} static MainWindowViewModel _mainWindowViewModel;
	}
}
