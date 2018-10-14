using System;
using System.Drawing;
using System.Windows.Forms;
using ShiPvsAsteroidS.GameForm;

namespace ShiPvsAsteroidS.MainForm
{
    public partial class fMain : Form
    {
        public static bool AppExit;

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            SplashScreen.Init(this);
            SplashScreen.Draw();

            var formPath = new System.Drawing.Drawing2D.GraphicsPath();
            formPath.AddEllipse(0, 0, Width, Height);
            var formRegion = new Region(formPath);
            Region = formRegion;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AppExit = true;
            Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var game = new Game();
            game = null;
            var fg = new fGame();
            
            game = new Game();

            

            SplashScreen.buffer.Dispose();
            SplashScreen.mainTimer.Stop();
            game.Init(fg);
            Hide();
            fg.Show();
            game.Draw();
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функция не доступна в данной версии. Ждите обновления.", "Sepo");
        }

        private void fMain_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
