using System.Drawing;
using Game = ShiPvsAsteroidS.GameForm.Game;

namespace ShiPvsAsteroidS.Objects.Controlable
{
    class ForceField : BaseObject
    {
        public const string ForceFieldImagePath = @"res\Ship\ForceField.png";
        public static Size ForceFieldImageSize = Image.FromFile(ForceFieldImagePath).Size;

        private Point imagePos;

        public ForceField(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
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

            Pos = new Point(ObjectValues.ShipObjects.ship.Pos.X - (int)(Size.Width * 0.2),
                ObjectValues.ShipObjects.ship.Pos.Y - (int)(Size.Height * 0.33));

            if (ObjectValues.ShipObjects.ResetForceFieldAnimation)
            {
                ResetForceField();
            }

            ForceFieldAnimation();
        }

        /// <summary>
        /// Сброс анимации силового поля.
        /// </summary>

        private void ResetForceField()
        {
            imagePos = new Point(0, 0);
            ObjectValues.ShipObjects.ForceFieldAnimationTimerCount = 0;

            ObjectValues.ShipObjects.ForceFieldAnimation = true;
            ObjectValues.ShipObjects.ResetForceFieldAnimation = false;

        }

        /// <summary>
        /// Анимация силового поля
        /// </summary>

        private void ForceFieldAnimation()
        {
            if (!ObjectValues.ShipObjects.ForceFieldAnimation) return;

            imagePos.Y += Size.Height;

            if (imagePos.Y != Image.Height) return;
            imagePos.X += Size.Width;
            imagePos.Y = 0;

            if (imagePos.X != Image.Width) return;
            imagePos.X = 0;

            ObjectValues.ShipObjects.ForceFieldAnimation = false;
        }
    }
}
