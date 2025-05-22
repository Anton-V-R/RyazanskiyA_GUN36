using Final_Task.Models;

namespace Final_Task.Services
{
    public interface ISaveLoadService<T>
    {
        T LoadPlayerProfile(string identifier);
        void SavePlayerProfile(T profile, string identifier);
    }
}
