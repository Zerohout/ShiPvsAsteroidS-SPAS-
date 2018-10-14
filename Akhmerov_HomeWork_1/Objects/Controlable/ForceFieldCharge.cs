using Game = Akhmerov_HomeWork_1.GameForm.Game;

namespace Akhmerov_HomeWork_1.Objects.Controlable
{
    using System.Drawing;
    using static ObjectValues.ShipObjects;
    using static Game;

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
            if (!ForceFieldEnabled) return;

            Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, new Rectangle(imagePos, Size), GraphicsUnit.Pixel);
        }

        public override void Update()
        {
            if (!ForceFieldEnabled) return;

            Pos = new Point(ship.Pos.X + Size.Width / 8, ship.Pos.Y + Size.Height / 2);

            GetDamage();
            GetCharge();
        }

        /// <summary>
        /// Получение урона кораблем.
        /// </summary>

        private void GetDamage()
        {
            if (!shipGetDamage) return;

            if (DamageShieldEnabled)
            {
                shipGetDamage = false;
                return;
            }

            DamageShieldEnabled = true;

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
            if (!shipGetCharge) return;

            if (imagePos.Y != 0)
            {
                imagePos.Y -= Size.Height;
            }

            shipGetCharge = false;
        }
    }
}
