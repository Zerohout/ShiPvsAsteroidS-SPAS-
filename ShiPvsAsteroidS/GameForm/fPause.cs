using System;
using System.Drawing;
using System.Windows.Forms;
using ShiPvsAsteroidS.Objects;

namespace ShiPvsAsteroidS.GameForm
{
    public partial class fPause : Form
    {

        public fPause()
        {
            InitializeComponent();
        }

        private void fPause_Load(object sender, EventArgs e)
        {
            lblGamePoints.Text = ObjectValues.GameScore.ToString();
            Cursor.Clip = new Rectangle(new Point(Location.X, Location.Y), new Size(Bounds.Width, Bounds.Height));
        }

        private void btnReturnToGame_Click(object sender, EventArgs e)
        {
            Game.gameTimer.Start();
            Cursor.Hide();
            Close();
        }

        private void btnExitToMainMenu_Click(object sender, EventArgs e)
        {
            Program.CloseGame = true;
            Game.gameTimer.Start();
            ObjectValues.GameScore = 0;
            Close();
        }
    }
}
