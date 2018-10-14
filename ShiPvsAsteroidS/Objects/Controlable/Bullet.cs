using System.Drawing;
using ShiPvsAsteroidS.GameForm;

namespace ShiPvsAsteroidS.Objects.Controlable
{
    class Bullet : BaseObject
    {
        public const string BulletImagePath = @"res\Bullets\Bullet.png";
        public const string ShowBulletImagePath = @"res\Bullets\ShowBullet.png";
        public static Size BulletImageSize = Image.FromFile(BulletImagePath).Size;
        public static Size ShowBulletImageSize = Image.FromFile(ShowBulletImagePath).Size;

        public Bullet(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {

        }

        public override void Update()
        {
            if (DeleteFlag) return;

            Pos.X += Dir.X;

            if (Pos.X > Game.Width)
            {
                DeleteFlag = true;
            }
        }
    }
}
