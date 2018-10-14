using System.Drawing;
using Akhmerov_HomeWork_1.Objects;

namespace Akhmerov_HomeWork_1.GameForm
{
    using System;
    using System.Windows.Forms;
    using static Game;

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
            gameTimer.Start();
            Cursor.Hide();
            Close();
        }

        private void btnExitToMainMenu_Click(object sender, EventArgs e)
        {
            Program.CloseGame = true;
            gameTimer.Start();
            ObjectValues.GameScore = 0;
            Close();
        }
    }
}
