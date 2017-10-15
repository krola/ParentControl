using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Client.Logger.Sources
{
    public class FileSource : ISource
    {
        private StreamWriter _streamWriter;
        private string _fileName;
        private string _dictionary = "Log";

        public FileSource()
        {
            var dateTimeName = DateTime.Now.ToString("yyyy-dd-M");
            _fileName = $"{dateTimeName}.txt";
            var path = Path.Combine(_dictionary, _fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            _streamWriter = new StreamWriter(path,true);
        }

        public void Write(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
        }
    }
}
