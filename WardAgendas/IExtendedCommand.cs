using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WardAgendas
{
	/// <summary>
	/// Interface for extending the properties of an ICommand object
	/// </summary>
	public interface IExtendedCommand
	{
		/// <summary>
		/// The name of the ICommand - should be unique within the scope of an application
		/// Note: It is likely that at least a Name must exist for shortcut keys to be assigned to the command.
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// Area, eg. "ImageEdit.ThumbnailView", can be used for grouping when managing commands
		/// </summary>
		string Area { get; set; }
		
		/// <summary>
		/// A short description of the command.  The first part of the tooltip. Can be used when managing commands.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// The Gesture text of the command eg.  "Ctrl+C".  Also the last part of the tooltip when it exists.
		/// Note: This can be bound to an ExtendedKeyBinding GestureText property in XAML where it will automatically update as changes occur in the Gesture
		/// </summary>
		string GestureText { get; set; }

		/// <summary>
		/// An additional set Gesture text of the command eg.  "Ctrl+D".  Also the last part of the tooltip when it exists.
		/// Note: Items in this list can be bound to an ExtendedKeyBinding GestureText property in XAML where it will automatically update as changes occur in the Gesture
		/// </summary>
		ObservableCollection<string> AdditionalGestureText { get; }

		/// <summary>
		/// The Display text
		/// </summary>
		string DisplayText { get; }

		/// <summary>
		/// The tooltip for this command.  A combination of Description and GestureText
		/// Note: this can be bound to the ToolTip property of a control in XAML
		/// </summary>
		string ToolTip { get; }
	}
}
