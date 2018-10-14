using System;
using System.Drawing;
using System.IO;
using System.Linq;
using ShiPvsAsteroidS.GameForm;

namespace ShiPvsAsteroidS.Objects.Passive
{
    class Star : BaseObject
    {
        static readonly Random rnd = new Random();

        private const string ImagePath = @"res\Stars\";
        public static readonly string[] Images = Directory.GetFiles(ImagePath);

        public static Size starSize = Image.FromFile(Images.First()).Size;

        public Star(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {
            var tempSize = rnd.Next(1, size.Width + 1);
            Size = new Size(tempSize, tempSize);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;

            if (Pos.X < 0 - Size.Width)
            {
                DeleteFlag = true;
                //Pos = new Point(Game.Width + starSize.Width, rnd.Next(Game.Height - Size.Height));

                //var tempSize = rnd.Next(1, starSize.Width);
                //Size = new Size(tempSize, tempSize);

                //Dir = new Point(rnd.Next(1, (int)(Size.Width * 1.5)), 0);

                //Image.Dispose();
                //Image = Image.FromFile(Images[rnd.Next(Images.Length)]);
            }

            if (Pos.Y > Game.Height || Pos.Y < 0)
            {
                Pos.Y = rnd.Next(Game.Height - Size.Height);
            }
        }
    }
}
