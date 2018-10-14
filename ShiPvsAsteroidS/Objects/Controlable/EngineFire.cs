using System.Drawing;
using Game = ShiPvsAsteroidS.GameForm.Game;

namespace ShiPvsAsteroidS.Objects.Controlable
{
    class EngineFire : BaseObject
    {
        public const string EngineFireImagePath = @"res\Ship\EngineFire.png";
        public static Size EngineFireImageSize = Image.FromFile(EngineFireImagePath).Size;
        private Point imagePos;
        private int firstPosY;
        private int secondPosY;

        public EngineFire(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {
            imagePos.Y -= size.Height;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos.X, firstPosY, new Rectangle(imagePos, Size), GraphicsUnit.Pixel);
            Game.Buffer.Graphics.DrawImage(Image, Pos.X, secondPosY, new Rectangle(imagePos, Size), GraphicsUnit.Pixel);
        }

        public override void Update()
        {
            FireEngineAnimation();

            Pos.X = ObjectValues.ShipObjects.ship.Pos.X - Size.Width + (int)(Size.Height * 0.3);
            firstPosY = ObjectValues.ShipObjects.ship.Pos.Y + Size.Height / 2;
            secondPosY = ObjectValues.ShipObjects.ship.Pos.Y + (int)(Size.Height * 1.2);
        }

        private void FireEngineAnimation()
        {
            imagePos.Y += Size.Height;

            if (imagePos.Y != Image.Height) return;

            imagePos.Y = 0;
            imagePos.X += Size.Width;
            
            if (imagePos.X == Image.Width)
            {
                imagePos.X = 0;
            }
        }
    }
}
