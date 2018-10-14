using System.Drawing;
using Game = ShiPvsAsteroidS.GameForm.Game;

namespace ShiPvsAsteroidS.Objects.Controlable
{
    class ForceFieldCharge : BaseObject
    {
        public const string ForceFieldChargeImagePath = @"res\ship\ForceFieldCharge.png";
        public static Size ForceFieldChargeImageSize = Image.FromFile(ForceFieldChargeImagePath).Size;
        private Point imagePos;

        public ForceFieldCharge(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {

        }

        public override void Draw()
        {
            if (!ObjectValues.ShipObjects.ForceFieldEnabled) return;

            Game.Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, new Rectangle(imagePos, Size), GraphicsUnit.Pixel);
        }

        public override void Update()
        {
            if (!ObjectValues.ShipObjects.ForceFieldEnabled) return;

            Pos = new Point(ObjectValues.ShipObjects.ship.Pos.X + Size.Width / 8, ObjectValues.ShipObjects.ship.Pos.Y + Size.Height / 2);

            GetDamage();
            GetCharge();
        }

        /// <summary>
        /// Получение урона кораблем.
        /// </summary>

        private void GetDamage()
        {
            if (!Game.shipGetDamage) return;

            if (ObjectValues.ShipObjects.DamageShieldEnabled)
            {
                Game.shipGetDamage = false;
                return;
            }

            ObjectValues.ShipObjects.DamageShieldEnabled = true;

            if (imagePos.Y != Image.Height)
            {
                imagePos.Y += Size.Height;
            }
            else
            {
                imagePos.Y = 0;
            }
        }

        /// <summary>
        /// Получение заряда кораблем.
        /// </summary>

        private void GetCharge()
        {
            if (!Game.shipGetCharge) return;

            if (imagePos.Y != 0)
            {
                imagePos.Y -= Size.Height;
            }

            Game.shipGetCharge = false;
        }
    }
}
