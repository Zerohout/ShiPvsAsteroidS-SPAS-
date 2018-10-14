using Game = Akhmerov_HomeWork_1.GameForm.Game;

namespace Akhmerov_HomeWork_1.Objects.Controlable
{
    using System.Drawing;
    using System.Windows.Forms;
    using GameForm;
    using static Game;
    using static ObjectValues.ShipObjects;

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
            if (!shipGetDamage) return;

            if (DamageShieldEnabled)
            {
                shipGetDamage = false;
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
            gameTimer.Stop();
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
            if (!shipGetCharge) return;

            if (Energy < FullEnergy)
            {
                Energy += ObjectValues.HealPoint;
            }
        }
    }
}

