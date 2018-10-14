using Akhmerov_HomeWork_1.Objects;

namespace Akhmerov_HomeWork_1.GameForm
{
    using System;
    using System.Windows.Forms;
    using static Game;
   // using static Game.ObjectValues;

    public partial class fGame : Form
    {

        public fGame()
        {
            InitializeComponent();
        }

        private void fGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                fPause fp = new fPause();
                gameTimer.Stop();
                Cursor.Show();
                ResetCursor();
                fp.ShowDialog();
                



            }
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
           
           
        }

        private void fGame_MouseClick(object sender, MouseEventArgs e)
        {

            ObjectValues.ShipObjects.Shot();

        }
    }
}
