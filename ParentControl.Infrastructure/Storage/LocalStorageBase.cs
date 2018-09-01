using System;
using System.IO;
using Newtonsoft.Json;

namespace ParentControl.Infrastructure.Storage
{
    public class LocalStorageBase <T> where T : class
    {
        private T _store;
        private string _fullPath;
        private string _fileName;

        private const string _folderName = ".parentcontrol";

        public LocalStorageBase(string fileName)
        {
            _fileName = fileName;
            InitServiceFolder();
            InitServiceFile();
        }
        private void InitServiceFile()
        {
            var filePath = Path.Combine(_fullPath, _fileName);
            if (!File.Exists(filePath))
            {
                _store = Activator.CreateInstance(typeof(T)) as T;
                var json = JsonConvert.SerializeObject(_store);
                File.WriteAllText(filePath, json);
            }
            else
            {
                var config = File.ReadAllText(filePath);
                _store = JsonConvert.DeserializeObject<T>(config);
            }
            _fullPath = filePath;
        }

        private void InitServiceFolder()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(appDataFolder, _folderName);

            // Check if folder exists and if not, create it
            if (!Directory.Exists(specificFolder))
            {
                var di = Directory.CreateDirectory(specificFolder);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            _fullPath = specificFolder;
        }

        protected void SaveStore()
        {
            var json = JsonConvert.SerializeObject(_store);
            File.WriteAllText(_fullPath, json);
        }

        public T Store { get { return _store; } }

        public string FullPath
        {
            get { return _fullPath; }
        }
    }
}
