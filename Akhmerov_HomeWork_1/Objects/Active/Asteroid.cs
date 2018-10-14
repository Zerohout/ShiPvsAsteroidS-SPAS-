namespace Akhmerov_HomeWork_1.Objects.Active
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    class Asteroid : BaseObject
    {
        readonly Random rnd = new Random();
        public const string ImagePath = @"res\Asteroids";
        public static Size ImageSize = Image.FromFile($@"{Directory.EnumerateFiles(ImagePath).First()}").Size;

        public Asteroid(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {
            var tempSize = rnd.Next((int)(size.Width * 0.7), size.Width + 1);
            Size = new Size(tempSize, tempSize);
        }

        public override void Update()
        {
            if (DeleteFlag) return;
            
            if (Pos.X < 0)
            {
                ObjectValues.GameScore -= Size.Width;
                DeleteFlag = true;
                return;
            }

            Pos.X -= Dir.X;
        }
    }
}
