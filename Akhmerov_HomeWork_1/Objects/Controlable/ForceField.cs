using Game = Akhmerov_HomeWork_1.GameForm.Game;

namespace Akhmerov_HomeWork_1.Objects.Controlable
{
    using System.Drawing;
    using static ObjectValues.ShipObjects;
    using static Game;


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
            if (!ForceFieldEnabled) return;

            Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, new Rectangle(imagePos, Size), GraphicsUnit.Pixel);

        }

        public override void Update()
        {
            if (!ForceFieldEnabled) return;

            Pos = new Point(ship.Pos.X - (int)(Size.Width * 0.2),
                ship.Pos.Y - (int)(Size.Height * 0.33));

            if (ResetForceFieldAnimation)
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
            ForceFieldAnimationTimerCount = 0;

            ObjectValues.ShipObjects.ForceFieldAnimation = true;
            ResetForceFieldAnimation = false;

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
