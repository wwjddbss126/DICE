using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICE.Modules.ViewModels
{
    public class MainToolViewModel : ViewModelBase
    {
        public string Caption { get; private set; }

        public static MainToolViewModel Create(string caption)
        {
            return ViewModelSource.Create(() => new MainToolViewModel()
            {
                Caption = caption,
            });
        }
        protected MainToolViewModel()
        {
        }

    }
}
