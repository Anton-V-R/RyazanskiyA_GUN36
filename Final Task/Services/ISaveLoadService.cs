namespace Final_Task.Services
{
    public interface ISaveLoadService<T>
    {
        T LoadData(string identifier);

        void SaveData(T data, string identifier);
    }
}
