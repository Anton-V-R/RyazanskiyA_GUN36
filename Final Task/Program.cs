using System;
using System.IO;

using Final_Task.Services;

namespace Final_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string basePath = Path.Combine(Environment.CurrentDirectory, "CasinoSaves");

                var saveLoadService = new FileSystemSaveLoadService(basePath);
                var casino = new Casino(saveLoadService);
                casino.StartGame();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }
        }
    }
}
