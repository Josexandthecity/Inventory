using Inventory.Services;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using Inventory.UWP.Services;

[assembly: Dependency(typeof(PathService))]
namespace Inventory.UWP.Services
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, AppSettings.DatabaseName);
        }
    }
}
