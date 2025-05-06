using Android.OS;
using Inventory.Droid.Services;
using Inventory.Services;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace Inventory.Droid.Services
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, AppSettings.DatabaseName);
        }
    }
}