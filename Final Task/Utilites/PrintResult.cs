using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Task.Utilites
{
    public static class PrintResult
    {
        public static void ColorInfo(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Info(string result)
        {
            Console.WriteLine(result);
        }
    }
}
