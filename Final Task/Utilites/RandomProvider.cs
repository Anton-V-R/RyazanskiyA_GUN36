namespace Final_Task.Utilites
{
    /// <summary>
    /// Статический рандом для всех игр
    /// </summary>
    public static class RandomProvider
    {
        private static readonly System.Random _random = new System.Random();
        private static readonly object _syncLock = new object();

        public static int Next(int minValue, int maxValue)
        {
            lock(_syncLock)
            {
                return _random.Next(minValue, maxValue);
            }
        }
    }
}
