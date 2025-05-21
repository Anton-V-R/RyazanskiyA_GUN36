namespace Final_Task.Models
{
    /// <summary>
    /// Профиль игрока
    /// </summary>
    public class PlayerProfile
    {
        /// <summary>
        /// Количество денег
        /// </summary>
        public int Bank { get; set; } = 1000;

        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }
    }
}
