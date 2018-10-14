using System.Drawing;
using System.IO;
using ShiPvsAsteroidS.GameForm;

namespace ShiPvsAsteroidS.Objects
{
    internal abstract class BaseObject : ICollision
    {
        public Point Pos;
        public Point Dir;
        public Size Size;
        public Image Image { get; protected set; }
        public bool DeleteFlag;
        public int Energy;
        public int FullEnergy;

        protected BaseObject(Point pos, Point dir, Size size, string imageName)
        {
            Image = Image.FromFile(File.Exists(imageName) ? imageName : $@"res\{imageName}.png");
            Pos = pos;
            Dir = dir;
            Size = size;
            DeleteFlag = false;
            GameObjectException();
        }

        private void GameObjectException()
        {
            if (Size.Width > Image.Width || Size.Height > Image.Height)
            {
                throw new GameObjectException($"Размер объекта {this} ({Size.Width},{Size.Height}) превышает максимальное значение ({Image.Width}, {Image.Height})");
            }
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public abstract void Update();

        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(Rect);
        }

        public Point Location(ICollision o)
        {
            return new Point(o.Rect.Right, Rect.Location.Y - Image.Height);
        }
    }
}
