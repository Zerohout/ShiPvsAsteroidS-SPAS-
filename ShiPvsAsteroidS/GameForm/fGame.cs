using System;
using System.Windows.Forms;
using ShiPvsAsteroidS.Objects;

namespace ShiPvsAsteroidS.GameForm
{
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
                Game.gameTimer.Stop();
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
