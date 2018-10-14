namespace Akhmerov_HomeWork_1
{
    using System.Drawing;

    interface ICollision
    {
        bool Collision(ICollision obj);
        Point Location(ICollision obj);
        Rectangle Rect { get; }

    }
}
