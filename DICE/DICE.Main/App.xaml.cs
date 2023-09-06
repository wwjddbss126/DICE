using DICE.Common;
using DICE.Main.Properties;
using DICE.Main.ViewModels;
using System.Globalization;
using DICE.Main.Views;
using System.IO;
using System;
using System.Threading;
using DICE.Modules.ViewModels;
using DICE.Modules.ViewModels.Cloud;
using DICE.Modules.Views;
using DICE.Modules.Views.Cloud;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Core;
using System.ComponentModel;
using System.Windows;
using AppModules = DICE.Common.Modules;
using DICE.Modules.Cloud.Utils;

namespace DICE.Main
{
	public partial class App : Application
	{
		public App()
		{
			ApplicationThemeHelper.UpdateApplicationThemeName();
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
			SetCultureInfo();
			StrategyManager.Default.RegisterStrategy<AccordionControl, AccordionControlStrategy>();
#if !DXCORE3
			ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
			Theme.RegisterPredefinedPaletteThemes();
			ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
			ViewLocator.Default = new ViewLocator(typeof(App).Assembly);

			base.OnStartup(e);
			Bootstrapper.Run();
		}

		static System.Reflection.Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
		{
			string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
			if (partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6")
			{
				string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), "..\\..\\bin", partialName + ".dll");
				return System.Reflection.Assembly.LoadFrom(path);
			}
			return null;
		}

		static void SetCultureInfo()
		{
			CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
			demoCI.DateTimeFormat = new DateTimeFormatInfo();
			Thread.CurrentThread.CurrentCulture = demoCI;
			CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
			demoUI.DateTimeFormat = new DateTimeFormatInfo();
			Thread.CurrentThread.CurrentUICulture = demoUI;
		}

		protected override void OnExit(ExitEventArgs e)
		{
			ApplicationThemeHelper.SaveApplicationThemeName();
			base.OnExit(e);
		}
	}
	public class Bootstrapper
	{
		public static Bootstrapper Default { get; protected set; }
		public static void Run()
		{
			Default = new Bootstrapper();
			Default.RunCore();
		}
		protected Bootstrapper() { }

		const string StateVersion = "1.0";
		public virtual void RunCore()
		{
			ConfigureTypeLocators();
			RegisterModules();
			if (!RestoreState())
				InjectModules();
			ConfigureNavigation();
			ShowMainWindow();
		}

		protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
		protected virtual void ConfigureTypeLocators()
		{
			var mainAssembly = typeof(MainViewModel).Assembly;
			var modulesAssembly = typeof(ModuleViewModel).Assembly;
			var assemblies = new[] { mainAssembly, modulesAssembly };
			ViewModelLocator.Default = new ViewModelLocator(assemblies);
			ViewLocator.Default = new ViewLocator(assemblies);
		}
		protected virtual void RegisterModules()
		{
			Manager.GetRegion(Regions.Documents).VisualSerializationMode = VisualSerializationMode.PerKey;
			Manager.Register(Regions.MainWindow, new Module(AppModules.Main, MainViewModel.Create, typeof(MainView)));

			Manager.Register(Regions.MainControl, new Module("InLab", () => InLabViewModel.Create("InLab"), typeof(InLabView)));
			Manager.Register(Regions.MegaCloudTree, new Module("MegaCloud", () => MegaContentViewModel.Create(), typeof(MegaContentView)));
			Manager.Register(Regions.MegaCloudNavigation, new Module("MegaCloud", () => MegaNavViewModel.Create(), typeof(MegaNavView)));
		}
		protected virtual bool RestoreState()
		{
#if !DEBUG
            if (Settings.Default.StateVersion != StateVersion) return false;
            return Manager.Restore(Settings.Default.LogicalState, Settings.Default.VisualState);
#else
			return false;
#endif
		}
		protected virtual void InjectModules()
		{
			Manager.Inject(Regions.MainWindow, AppModules.Main);
			Manager.Inject(Regions.MainControl, "InLab");
			Manager.Inject(Regions.MegaCloudTree, "MegaCloud");
			Manager.Inject(Regions.MegaCloudNavigation, "MegaCloud");
		}
		protected virtual void ConfigureNavigation()
		{
			Manager.GetEvents(Regions.Navigation).Navigation += OnNavigation;
			Manager.GetEvents(Regions.Documents).Navigation += OnDocumentsNavigation;
			
			Manager.GetEvents(Regions.MainControl).Navigation += OnMainControlNavigation;
			Manager.GetEvents(Regions.NetworkControl).Navigation += OnNetworkControlNavigation;

			Manager.GetEvents(Regions.MessageControl).Navigation += OnMessageControlNavigation;

			Manager.GetEvents(Regions.MegaCloudNavigation).Navigation += MegaCloudNavigation;
			Manager.GetEvents(Regions.MegaCloudTree).Navigation += MegaCloudDocumentNavigation;
		}
		protected virtual void ShowMainWindow()
		{
			App.Current.MainWindow = new MainWindow();
			App.Current.MainWindow.Show();
			App.Current.MainWindow.Closing += OnClosing;
		}

		void OnNavigation(object sender, NavigationEventArgs e)
		{
			if (e.NewViewModelKey == null) return;
			Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
		}

		void MegaCloudNavigation(object sender, NavigationEventArgs e)
		{
			Manager.InjectOrNavigate(Regions.MegaCloudTree, e.NewViewModelKey);
		}
		void MegaCloudDocumentNavigation(object sender, NavigationEventArgs e)
		{
			Manager.Navigate(Regions.MegaCloudNavigation, e.NewViewModelKey);
		}

		void OnDocumentsNavigation(object sender, NavigationEventArgs e)
		{
			Manager.Navigate(Regions.Navigation, e.NewViewModelKey);

		}
		void OnMainControlNavigation(object sender, NavigationEventArgs e)
		{
			Manager.Navigate(Regions.MainControl, e.NewViewModelKey);
		}

		void OnNetworkControlNavigation(object sender, NavigationEventArgs e)
		{
			Manager.Navigate(Regions.NetworkControl, e.NewViewModelKey); ;
		}
		void OnMessageControlNavigation(object sender, NavigationEventArgs e)
		{
			Manager.Navigate(Regions.MessageControl, e.NewViewModelKey); ;
		}

		void OnClosing(object sender, CancelEventArgs e)
		{
			string logicalState;
			string visualState;
			Manager.Save(out logicalState, out visualState);
			Settings.Default.StateVersion = StateVersion;
			Settings.Default.LogicalState = logicalState;
			Settings.Default.VisualState = visualState;
			Settings.Default.Save();
		}
	}
}