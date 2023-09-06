using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DICE.Modules.ViewModels.Cloud.Mega;
using System.ComponentModel;

namespace DICE.Modules.Views.Cloud
{
	public partial class MegaView : UserControl, INotifyPropertyChanged
	{
		private string tips;
		public string Tips
		{
			get { return tips; }
			set
			{
				if (tips != value)
				{
					tips = value;
					OnPropertyChanged("Tips");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private List<MenuItem> GetSiblingGroupItems(MenuItem currentItem)
		{
			var parentItem = currentItem.Parent as MenuItem;

			if (parentItem == null)
			{
				return null;
			}

			List<MenuItem> items = new List<MenuItem>();

			foreach (var item in parentItem.Items)
			{
				MenuItem container = parentItem.ItemContainerGenerator.ContainerFromItem(item) as MenuItem;
				if (container == null || container.Tag == null)
				{
					continue;
				}
				if (container.Tag.Equals(currentItem.Tag))
				{
					items.Add(container);
				}
			}
			return items;
		}

		public MegaView()
		{
			InitializeComponent();
		}
	}
}