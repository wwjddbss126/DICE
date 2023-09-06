using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ModuleInjection;
using DICE.Modules.Cloud.Helpers;
using DICE.Common;

namespace DICE.Modules.ViewModels.Cloud
{
    public class MegaViewModel
    {
        public static MegaViewModel Create()
        {
            return ViewModelSource.Create(() => new MegaViewModel()); ;
        }

        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        protected MegaViewModel()
        {
#if !DXCORE3
            if (!ViewModelBase.IsInDesignMode)
                RegisterJumpList();
#endif
        }
        [Command(false)]
        public void RegisterJumpList()
        {
            var jumpListService = this.GetRequiredService<IApplicationJumpListService>();
            jumpListService.Items.AddOrReplace("MegaCloudNavigation", "MegaCloud", FilePathHelper.GetDXImageSource("Outlook Inspired/Glyph_Mail"), OpenCloud);
            jumpListService.Apply();
        }
        public void OpenCloud()
        {
            Manager.Navigate(Regions.MegaCloudTree, Modules.MegaCloud);

        }
    }
}
