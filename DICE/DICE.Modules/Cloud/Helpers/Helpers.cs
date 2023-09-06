using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Images;
using DevExpress.Xpf.Core;
using System.Windows.Media;
using System;

namespace DICE.Modules.Cloud.Helpers
{
    public class FilePathHelper
    {
        public static string GetFullPath(string name)
        {
            var type = Type.GetType("DevExpress.DemoData.Helpers.DataFilesHelper, " + AssemblyInfo.SRAssemblyDemoData + ", Version=" + AssemblyInfo.Version + ", Culture=neutral, PublicKeyToken=" + AssemblyInfo.PublicKeyToken);
            var method = type.GetMethod("FindFile", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var propValue = "Data";
            return (string)method.Invoke(null, new object[] { name, propValue });
        }
        public static Uri GetDXImageUri(string path)
        {
            return AssemblyHelper.GetResourceUri(typeof(DXImages).Assembly, string.Format("SvgImages/{0}.svg", path));
        }
        public static Uri GetAppImageUri(string path, string extension = "svg", UriKind uriKind = UriKind.Relative)
        {
            return new Uri(string.Format("/DevMailClient;component/Images/{0}.{1}", path, extension), uriKind);
        }
        public static ImageSource GetDXImageSource(string path)
        {
            return (ImageSource)new SvgImageSourceExtension() { Uri = GetDXImageUri(path), Size = new System.Windows.Size(16, 16) }.ProvideValue(null);
        }
    }
}
