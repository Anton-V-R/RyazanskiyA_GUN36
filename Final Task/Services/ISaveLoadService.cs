using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Task.Services
{
    public interface ISaveLoadService<T>
    {
        void SaveData(T data, string identifier);
        T LoadData(string identifier);
    }
}
