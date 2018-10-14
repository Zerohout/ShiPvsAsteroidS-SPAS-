

using System;
using System.Windows.Forms;
using ShiPvsAsteroidS.MainForm;

namespace ShiPvsAsteroidS
{
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
