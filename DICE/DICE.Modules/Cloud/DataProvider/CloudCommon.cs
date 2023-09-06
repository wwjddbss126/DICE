using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DICE.Modules.ViewModels.Cloud;

namespace DICE.Modules.Cloud.DataProvider
{
    public interface IDataProvider
    {
        IEnumerable<T> GetItems<T>();
    }
	public interface MegaIDataProvider
	{
		IEnumerable<T> GetItems<T>();
	}
	public static class MegaDataSource
	{
		static MegaIDataProvider instance;
		public static MegaIDataProvider GetDefaultDataProvider()
		{
			return instance ?? (instance = CreateDefaultDataProvider());
		}
		static MegaIDataProvider CreateDefaultDataProvider()
		{
			return new MegaDataProvider();
		}
	}
	public abstract class MegaDataProviderBase : MegaIDataProvider
	{
		#region Props
		IList<MegaDetailViewModel> details;
		#endregion
		IEnumerable<MegaDetailViewModel> GetMailMessages() { return details ?? (details = FillMessages()); }

		public virtual IEnumerable<T> GetItems<T>()
		{
			Type requestedType = typeof(T);
			if (IsDerivedFrom<MegaDetailViewModel, T>())
				return GetMailMessages().Cast<T>();

			throw new NotSupportedException();
		}
		protected abstract IList<MegaDetailViewModel> FillMessages();
		protected static bool IsDerivedFrom<TBase, TAncestor>()
		{
			Type baseType = typeof(TBase);
			Type ancestorType = typeof(TAncestor);
			return baseType.Equals(ancestorType) || baseType.IsAssignableFrom(ancestorType);
		}
	}

}
