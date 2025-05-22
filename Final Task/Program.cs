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

                var casino = new Casino(basePath);
                casino.StartGame();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }
        }
    }
}
