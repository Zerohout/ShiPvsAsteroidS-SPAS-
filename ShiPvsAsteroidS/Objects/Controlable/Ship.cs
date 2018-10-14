using System.Drawing;
using System.Windows.Forms;
using ShiPvsAsteroidS.GameForm;
using Game = ShiPvsAsteroidS.GameForm.Game;

namespace ShiPvsAsteroidS.Objects.Controlable
{
    class Ship : BaseObject
    {
        public const string ShipImagePath = @"res\Ship\Ship.png";
        public static Size ShipImageSize = Image.FromFile(ShipImagePath).Size;

        public Ship(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {
            FullEnergy = 100;
            Energy = FullEnergy;
        }

        public override void Update()
        {
            Pos = new Point(Control.MousePosition.X - Game.GameForm.Location.X, Control.MousePosition.Y - Game.GameForm.Location.Y);

            GetDamage();
            GetCharge();
        }

        /// <summary>
        /// Получение урона.
        /// </summary>

        private void GetDamage()
        {
            if (!Game.shipGetDamage) return;

            if (ObjectValues.ShipObjects.DamageShieldEnabled)
            {
                Game.shipGetDamage = false;
                return;
            }

            if (Energy > (int)(FullEnergy * 0.2))
            {
                Energy -= ObjectValues.DamagePoint;
            }
            else
            {
                GameOver();
            }
        }

        /// <summary>
        /// Окончание игры.
        /// </summary>

        private void GameOver()
        {
            Game.gameTimer.Stop();
            Cursor.Show();

            var fp = new fPause
            {
                lblLabel = { Text = "Поражение!" },
                lblGamePoints = { Text = ObjectValues.GameScore.ToString() },
                btnReturnToGame = { Enabled = false }
            };
            fp.ShowDialog();
        }

        /// <summary>
        /// Получение заряда.
        /// </summary>

        private void GetCharge()
        {
            if (!Game.shipGetCharge) return;

            if (Energy < FullEnergy)
            {
                Energy += ObjectValues.HealPoint;
            }
        }
    }
}

