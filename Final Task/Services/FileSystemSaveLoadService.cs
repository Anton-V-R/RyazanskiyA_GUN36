using System;
using System.IO;

using Final_Task.Models;

using Newtonsoft.Json;

namespace Final_Task.Services
{
    public class FileSystemSaveLoadService : ISaveLoadService<PlayerProfile>
    {
        private const int _bank = 1000;
        private const string _profileFileName = "player_profile.json";
        private readonly string _profilesDirectory;
        private readonly string _filePath;
        public FileSystemSaveLoadService(string basePath)
        {
            _profilesDirectory = basePath ?? throw new ArgumentNullException(nameof(basePath));
            Directory.CreateDirectory(_profilesDirectory);
            _filePath = Path.Combine(_profilesDirectory, _profileFileName);
        }

        public PlayerProfile LoadPlayerProfile(string filePath)
        {
            if(!File.Exists(_filePath))
            {
                return CreateNewProfile();
            }

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<PlayerProfile>(json) ?? CreateNewProfile();
            }
            catch(Exception ex) //when(ex is IOException or JsonException)
            {
                Console.WriteLine($"Error loading profile: {ex.Message}");
                return CreateNewProfile();
            }
        }

        public void SavePlayerProfile(PlayerProfile profile, string profileName)
        {
            if(profile == null)
                throw new ArgumentNullException(nameof(profile));

            try
            {
                string filePath = Path.Combine(_profilesDirectory, $"{profileName}.json");

                string json = JsonConvert.SerializeObject(profile, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch(Exception ex) //when(ex is IOException or JsonException)
            {
                Console.WriteLine($"Error saving profile: {ex.Message}");
            }
        }

        private PlayerProfile CreateNewProfile()
        {
            Console.WriteLine("Creating new player profile...");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            return new PlayerProfile
            {
                Name = string.IsNullOrWhiteSpace(name) ? "Player" : name,
                Bank = _bank
            };
        }
    }

}