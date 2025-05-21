using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;

namespace Final_Task.Services
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private readonly string _basePath;

        public FileSystemSaveLoadService(string basePath)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
            Directory.CreateDirectory(_basePath);
        }

        public void SaveData(string data, string identifier)
        {
            if(string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Identifier cannot be empty", nameof(identifier));

            string filePath = Path.Combine(_basePath, $"{identifier}.txt");
            File.WriteAllText(filePath, data);
        }

        public string LoadData(string identifier)
        {
            if(string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Identifier cannot be empty", nameof(identifier));

            string filePath = Path.Combine(_basePath, $"{identifier}.txt");
            return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
        }
    }

}