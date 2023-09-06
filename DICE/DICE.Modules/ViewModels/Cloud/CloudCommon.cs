using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DICE.Modules.Cloud.DataProvider;
using System;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using System.Linq;

namespace DICE.Modules.ViewModels.Cloud
{
    public enum ModuleType { MegaCloud }

    public static class Modules
    {
		public static string MegaCloud { get { return "MegaCloud"; } }
	}
    public class ContentViewModelInjectedMessage
    {
        public ModuleType ModuleType { get; private set; }
        public object ViewModel { get; private set; }

        public ContentViewModelInjectedMessage(ModuleType moduleType, object viewModel)
        {
            ModuleType = moduleType;
            ViewModel = viewModel;
        }
    }

    public abstract class NavigationViewModelBase
    {
        public Uri Icon { get; protected set; }
        public string Header { get; protected set; }
        public virtual object ContentViewModel { get; protected set; }
        protected ModuleType ModuleType { get; private set; }
        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }

        public HeaderViewModel HeaderViewModel { get; set; }

        protected NavigationViewModelBase(ModuleType moduleType)
        {
            ModuleType = moduleType;

            Initialize();
            if (this.IsInDesignMode())
                InitializeInDesignMode();
            Messenger.Default.Register<ContentViewModelInjectedMessage>(this, OnViewModelInjectedMessage);
        }

        protected virtual void Initialize() { }
        protected virtual void InitializeInDesignMode() { }
        protected virtual void OnContentViewModelChanged(object oldValue) { }

        void OnViewModelInjectedMessage(ContentViewModelInjectedMessage message)
        {
            if (ModuleType == message.ModuleType)
                ContentViewModel = message.ViewModel;
        }

    }

    public class HeaderViewModel
    {
        public static HeaderViewModel Create()
        {
            return ViewModelSource.Create(() => new HeaderViewModel());
        }
        public Uri Icon { get; protected set; }
        public string Header { get; protected set; }
        public List<NavigationViewModelBase> Content { get; protected set; }
        public void Init(NavigationViewModelBase modelBase)
        {
            Content = new List<NavigationViewModelBase>() { modelBase };
            Header = modelBase.Header;
            Icon = modelBase.Icon;
        }
    }
    
	public abstract class MegaContentViewModelBase<T>
	{
		public virtual ObservableCollection<T> ItemsSource { get; protected set; }
		protected MegaIDataProvider MegaDataProvider { get; private set; }
		protected IList<T> Items { get; private set; }
		protected ModuleType ModuleType { get; private set; }

		protected MegaContentViewModelBase(ModuleType moduleType)
		{
			ModuleType = moduleType;
			InitializeDataProvider();
			InitializeItems();
			UpdateItemsSource();
			if (this.IsInDesignMode())
				InitializeInDesignMode();
			else
				Messenger.Default.Send(new ContentViewModelInjectedMessage(ModuleType, this));
		}

		protected virtual void InitializeInDesignMode() { }
		protected virtual void InitializeItems()
		{
			Items = MegaDataProvider.GetItems<T>().ToList();
		}
		protected virtual void InitializeDataProvider()
		{
			MegaDataProvider = MegaDataSource.GetDefaultDataProvider();
		}
		protected virtual void UpdateItemsSource()
		{
			ItemsSource = new ObservableCollection<T>(Items);
		}
	}

}