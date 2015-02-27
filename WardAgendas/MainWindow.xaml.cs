using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WardAgendas
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (tabControl.SelectedIndex == 0)
				Properties.Settings.Default.Offset = int.Parse(BishopricOffset.Text);
			else if (tabControl.SelectedIndex == 1)
				Properties.Settings.Default.Offset = int.Parse(PecOffset.Text);
			else if (tabControl.SelectedIndex == 2)
				Properties.Settings.Default.Offset = int.Parse(WardCouncelOffset.Text);

			Properties.Settings.Default.Update = DateTime.Parse(UpdateTxt.Text);

			Properties.Settings.Default.Save();
		}
	}
}
