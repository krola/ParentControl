using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Service
{
    public class BaseConfig <T> where T : class
    {
        private T _config;
        private string _fullPath;
        private string _fileName;

        private const string _folderName = ".pc";

        public BaseConfig(string fileName)
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
                _config = Activator.CreateInstance(typeof(T)) as T;
                var json = JsonConvert.SerializeObject(_config);
                File.WriteAllText(filePath, json);
            }
            else
            {
                var config = File.ReadAllText(filePath);
                _config = JsonConvert.DeserializeObject<T>(config);
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

        protected void UpdateConfig()
        {
            var json = JsonConvert.SerializeObject(_config);
            File.WriteAllText(_fullPath, json);
        }

        public T Config { get { return _config; } }

        public string FullPath
        {
            get { return _fullPath; }
        }
    }
}
