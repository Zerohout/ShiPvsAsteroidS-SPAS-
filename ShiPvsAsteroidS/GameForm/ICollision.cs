using System.Drawing;

namespace ShiPvsAsteroidS.GameForm
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Point Location(ICollision obj);
        Rectangle Rect { get; }

    }
}
