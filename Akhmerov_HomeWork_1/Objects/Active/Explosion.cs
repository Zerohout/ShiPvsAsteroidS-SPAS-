namespace Akhmerov_HomeWork_1.Objects.Active
{
    using System.Drawing;
    using GameForm;

    class Explosion : BaseObject
    {
        private Point imagePos;
        private const string ImagePath = @"res\Explosion.png";
        public static Size explosionSize = Image.FromFile(ImagePath).Size;


        public Explosion(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {
            imagePos.X -= size.Width;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, new Rectangle(imagePos, Size), GraphicsUnit.Pixel);
        }

        public override void Update()
        {
            if (DeleteFlag) return;

            ExplosionAnimation();

        }

        private void ExplosionAnimation()
        {
            imagePos.X += Size.Width;

            if (imagePos.X == Image.Width)
            {
                imagePos.X = 0;
                imagePos.Y += Size.Height;

                if (imagePos.Y == Image.Height)
                {
                    DeleteFlag = true;
                    return;
                }
            }

            Pos.X -= (int)(Dir.X * 0.85);
        }
    }
}
