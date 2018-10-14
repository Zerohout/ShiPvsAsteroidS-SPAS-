

using Akhmerov_HomeWork_1.GameForm;

namespace Akhmerov_HomeWork_1
{
    using System;
    using System.Windows.Forms;
    using MainForm;
    
    static class Program
    {
        public static bool CloseGame;
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread] 
        
        

        static void Main()
        {
                fMain fm = new fMain();
                Application.Run(fm);

        }

        
    }
}
