using System;
using System.IO;
using PCLStorage;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ParentControl.Core.Storage
{
    public class LocalStorageBase <T> where T : class
    {
        private T _store;
        private IFolder _storageFolder;
        private IFile _storeFile;
        private string _fileName;
        private readonly IFolder _rootFolder;
        private const string _folderName = ".pc";

        public LocalStorageBase(string fileName)
        {
            _rootFolder = FileSystem.Current.LocalStorage;
            _fileName = fileName;
            InitServiceFolderAsync();
            InitServiceFileAsync();
        }
        private async Task InitServiceFileAsync()
        {
            var fileExistance = await _storageFolder.CheckExistsAsync(_fileName);
            if (fileExistance == ExistenceCheckResult.NotFound)
            {
                _store = Activator.CreateInstance(typeof(T)) as T;
                var json = JsonConvert.SerializeObject(_store);
                _storeFile = await _storageFolder.CreateFileAsync(_fileName, CreationCollisionOption.OpenIfExists);
            }
            else
            {
                _storeFile = await _storageFolder.GetFileAsync(_fileName);
                var content = await _storeFile.ReadAllTextAsync();
                _store = JsonConvert.DeserializeObject<T>(content);
            }
        }

        private async System.Threading.Tasks.Task InitServiceFolderAsync()
        {
            string appDataFolder = Path.Combine(_rootFolder.Path, _folderName);

            var folderExistance = await _rootFolder.CheckExistsAsync(_folderName);
            if (folderExistance == ExistenceCheckResult.NotFound)
            {
                _storageFolder =  await _rootFolder.CreateFolderAsync(_folderName, CreationCollisionOption.OpenIfExists);
            }
        }

        protected async Task UpdateConfigAsync()
        {
            var json = JsonConvert.SerializeObject(_store);
            await _storeFile.WriteAllTextAsync(json);
        }

        public T Store { get { return _store; } }
    }
}
