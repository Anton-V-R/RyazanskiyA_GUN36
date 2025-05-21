using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Task.Models
{
    /// <summary>
    /// Профиль игрока
    /// </summary>
    public class PlayerProfile
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество денег
        /// </summary>
        public int Bank { get; set; } = 1000;
    }
}
