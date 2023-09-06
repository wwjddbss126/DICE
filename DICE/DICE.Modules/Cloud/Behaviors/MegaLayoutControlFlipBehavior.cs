using System.Linq;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Mvvm.UI.Interactivity;
using DICE.Modules.ViewModels.Cloud;
using System.Windows;
using System.Collections.Generic;

namespace DICE.Modules.Cloud.Behaviors
{
	public class MegaLayoutControlFlipBehavior : Behavior<LayoutControl>
	{
		public static readonly DependencyProperty LayoutModeProperty =
			DependencyProperty.Register("MegaLayoutMode", typeof(MegaLayoutMode), typeof(MegaLayoutControlFlipBehavior),
			new PropertyMetadata(MegaLayoutMode.Normal, (d, e) => ((MegaLayoutControlFlipBehavior)d).OnLayoutModeChanged()));
		public static readonly DependencyProperty OrderIndexProperty =
			DependencyProperty.RegisterAttached("OrderIndex", typeof(int), typeof(MegaLayoutControlFlipBehavior), new PropertyMetadata(-1));
		public static int GetOrderIndex(DependencyObject target)
		{
			return (int)target.GetValue(OrderIndexProperty);
		}
		public static void SetOrderIndex(DependencyObject target, int value)
		{
			target.SetValue(OrderIndexProperty, value);
		}

		public MegaLayoutMode MegaLayoutMode
		{
			get { return (MegaLayoutMode)GetValue(LayoutModeProperty); }
			set { SetValue(LayoutModeProperty, value); }
		}

		void OnLayoutModeChanged()
		{
			if (AssociatedObject == null)
				return;

			IEnumerable<FrameworkElement> children = AssociatedObject.Children.Cast<FrameworkElement>().ToList();
			if (MegaLayoutMode == MegaLayoutMode.Normal)
				children = children.OrderBy(x => GetOrderIndex(x));
			else
				children = children.OrderByDescending(x => GetOrderIndex(x));

			AssociatedObject.Children.Clear();
			children.ToList().ForEach(x => {
				x.Width = double.NaN;
				x.Height = double.NaN;
				AssociatedObject.Children.Add(x);
			});
		}
	}
}
