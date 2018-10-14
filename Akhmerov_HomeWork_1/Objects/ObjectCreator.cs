namespace Akhmerov_HomeWork_1.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Controlable;
    using Passive;
    using Active;
    using System.Drawing;
    using System.IO;
    using GameForm;

    static class ObjectCreator
    {

        /// <summary>
        /// Добавить космические объекты.
        /// </summary>

        public static class AddSpaceObjects
        {
            /// <summary>
            /// Создание объекта "Астероид" со свойствами по умолчанию.
            /// </summary>
            /// <returns>Объект "Астероид".</returns>

            public static Asteroid CreateAsteroid()
            {
                var rnd = new Random();

                return new Asteroid(
                    new Point(Game.Width, rnd.Next(Asteroid.ImageSize.Height, Game.Height - Asteroid.ImageSize.Height)),
                    new Point(rnd.Next((int)(Asteroid.ImageSize.Width * 0.1), (int)(Asteroid.ImageSize.Width * 0.25)), 0),
                    Asteroid.ImageSize,
                    $@"Asteroids\Asteroid{rnd.Next(Directory.GetFiles(Asteroid.ImagePath).Length)}");
            }

            /// <summary>
            /// Создание объекта "Астероид" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Астероид"</returns>

            public static Asteroid CreateAsteroid(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new Asteroid(position, motionVector, objectSize, imagePath);
            }

            /// <summary>
            /// Создание объекта "Звезда".
            /// </summary>
            /// <returns>Объект "Звезда".</returns>

            public static Star CreateStar()
            {
                var rnd = new Random();

                return new Star(
                    new Point(
                        rnd.Next(Game.Width, Game.Width + Star.starSize.Width),
                        rnd.Next(Game.Height - Star.starSize.Height)),
                    new Point(rnd.Next(1, (int)(Star.starSize.Width * 1.5)), 0),
                    Star.starSize,
                    Star.Images[rnd.Next(Star.Images.Length)]);
            }

            /// <summary>
            /// Создание объекта "Звезда" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Звезда"</returns>

            public static Star CreateStar(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new Star(position, motionVector, objectSize, imagePath);
            }

            /// <summary>
            /// Создание объекта "Батарейка" со свойствами по умолчанию.
            /// </summary>
            /// <returns>Объект "Батарейка".</returns>

            public static Battery CreateBattery()
            {
                var rnd = new Random();

                return new Battery(
                    new Point(Game.Width, rnd.Next(Game.Height - Battery.BatteryImageSize.Height)),
                    new Point(
                        rnd.Next((int)(Battery.BatteryImageSize.Width * 0.25),
                            (int)(Battery.BatteryImageSize.Width * 0.5)), 0),
                    Battery.BatteryImageSize,
                    Battery.BatteryImagePath);
            }

            /// <summary>
            /// Создание объекта "Батарейка" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Батарейка"</returns>

            public static Battery CreateBattery(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new Battery(position, motionVector, objectSize, imagePath);
            }
        }

        /// <summary>
        /// Добавить объекты типа "Корабль" и его составляющие.
        /// </summary>

        public static class AddShipObjects
        {
            /// <summary>
            /// Создание объекта "Корабль" со свойствами по умолчанию.
            /// </summary>
            /// <returns>Объект "Корабль".</returns>

            public static Ship CreateShip()
            {
                return new Ship(
                    Control.MousePosition,
                    new Point(0, 0),
                    Ship.ShipImageSize,
                    Ship.ShipImagePath);
            }

            /// <summary>
            /// Создание объекта "Корабль" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Корабль"</returns>

            public static Ship CreateShip(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new Ship(position, motionVector, objectSize, imagePath);
            }

            /// <summary>
            /// Создание объекта "Огонь двигателя".
            /// </summary>
            /// <param name="engine">Расположение двигателя</param>
            /// <returns>Объект "Огонь двигателя" определенного расположения.</returns>

            public static EngineFire CreateEngineFire()
            {
                return new EngineFire(
                        new Point(Control.MousePosition.X, Control.MousePosition.Y),
                        new Point(0, 0),
                        new Size(EngineFire.EngineFireImageSize.Width / 4, EngineFire.EngineFireImageSize.Height / 8),
                        EngineFire.EngineFireImagePath);
            }

            /// <summary>
            /// Создание объекта "Огонь двигателя" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Огонь двигателя" определенного расположения</returns>

            public static EngineFire CreateEngineFire(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new EngineFire(position, motionVector, objectSize, imagePath);
            }

            /// <summary>
            /// Создание объекта "Снаряд".
            /// </summary>
            /// <param name="ship">Корабль, чьи снаряды будут созданы.</param>
            /// <returns>Объект "Снаряд".</returns>

            public static Bullet CreateBullet(BaseObject ship)
            {
                return new Bullet(
                    new Point(ship.Pos.X + ship.Size.Width, ship.Pos.Y + (int)(ship.Size.Height * 0.25)),
                    new Point(10, 0),
                    Bullet.BulletImageSize,
                    Bullet.BulletImagePath);
            }

            /// <summary>
            /// Создание объекта "Силовое поле".
            /// </summary>
            /// <returns>Объект "Силовое поле".</returns>

            public static ForceField CreateForceField()
            {
                return new ForceField(
                    Control.MousePosition,
                    new Point(0, 0),
                    new Size(
                        ForceField.ForceFieldImageSize.Width / 4,
                        ForceField.ForceFieldImageSize.Height / 5),
                    ForceField.ForceFieldImagePath);
            }

            /// <summary>
            /// Создание объекта "Силовое поле" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Силовое поле"</returns>

            public static ForceField CreateForceField(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new ForceField(position, motionVector, objectSize, imagePath);
            }
        }

        /// <summary>
        /// Добавить объекты интерфейса.
        /// </summary>

        public static class AddInterfaceObject
        {
            /// <summary>
            /// Создание объекта "Отображение имеющихся выстрелов".
            /// </summary>
            /// <param name="availabelBullets">Снаряды в наличии.</param>
            /// <returns>Объект "Отображение имеющихся выстрелов".</returns>

            public static Bullet CreateDisplayBullet(List<BaseObject> availabelBullets)
            {
                return new Bullet(
                    new Point(availabelBullets.Count * Bullet.BulletImageSize.Width,
                        Game.Height - Bullet.ShowBulletImageSize.Height),
                    new Point(0, 0),
                    Bullet.ShowBulletImageSize,
                    Bullet.ShowBulletImagePath);
            }

            /// <summary>
            /// Создание объекта "Заряд силового поля" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Заряд силового поля"</returns>

            public static Bullet CreateDisplayBullet(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new Bullet(position, motionVector, objectSize, imagePath);
            }

            /// <summary>
            /// Создание объекта "Заряд силового поля".
            /// </summary>
            /// <returns>Объект "Заряд силового поля".</returns>

            public static ForceFieldCharge CreateForceFieldCharge()
            {
                return new ForceFieldCharge(
                    new Point(Control.MousePosition.X, Control.MousePosition.Y - ForceFieldCharge.ForceFieldChargeImageSize.Height),
                    new Point(0, 0),
                    new Size(ForceFieldCharge.ForceFieldChargeImageSize.Width,
                        ForceFieldCharge.ForceFieldChargeImageSize.Height / 4),
                    ForceFieldCharge.ForceFieldChargeImagePath);
            }

            /// <summary>
            /// Создание объекта "Заряд силового поля" с пользовательскими свойствами.
            /// </summary>
            /// <param name="position">Положение объекта в пространстве</param>
            /// <param name="motionVector">Вектор движения объекта</param>
            /// <param name="objectSize">Размер объекта</param>
            /// <param name="imagePath">Путь к изображению объекта</param>
            /// <returns>Объект "Заряд силового поля"</returns>

            public static ForceFieldCharge CreateForceFieldCharge(Point position, Point motionVector, Size objectSize, string imagePath)
            {
                return new ForceFieldCharge(position, motionVector, objectSize, imagePath);
            }
        }

        /// <summary>
        /// Добавить общие объекты.
        /// </summary>

        public static class AddGeneralObjects
        {
            /// <summary>
            /// Создание объекта "Взрыв".
            /// </summary>
            /// <param name="asteroids">Список астероидов.</param>
            /// <param name="numberOfAsteroid">Номер астероида, который должен взорваться.</param>
            /// <returns>Объект "Взрыв".</returns>

            public static Explosion CreateExplosion(List<BaseObject> asteroids, int numberOfAsteroid)
            {
                return new Explosion(
                    new Point(asteroids[numberOfAsteroid].Pos.X - (int)(asteroids[numberOfAsteroid].Size.Width * 0.5),
                        asteroids[numberOfAsteroid].Pos.Y - (int)(asteroids[numberOfAsteroid].Size.Height * 0.5)),
                    asteroids[numberOfAsteroid].Dir,
                    new Size(Explosion.explosionSize.Width / 8, Explosion.explosionSize.Height / 6),
                    "Explosion");
            }
        }
    }
}
