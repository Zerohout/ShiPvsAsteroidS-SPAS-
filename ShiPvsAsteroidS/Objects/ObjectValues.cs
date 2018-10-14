using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ShiPvsAsteroidS.GameForm;
using ShiPvsAsteroidS.Objects.Controlable;

namespace ShiPvsAsteroidS.Objects
{
    internal static class ObjectValues
    {

        /// <summary>
        /// Установка начальных значений.
        /// </summary>

        public static void SetStartValues()
        {
            GameLevel = 1;
            GameScore = 0;

            SpaceObjects.ObjectAsteroid.AsteroidCounter = SpaceObjects.ObjectAsteroid.AsteroidMaxCountIncrement();
            SpaceObjects.ObjectAsteroid.AsteroidTimerCounter = 0;
            ShipObjects.BatterySpawnTimerCounter = 0;

            if (Game.removableCosmosObjects == null) return;

            foreach (var objs in Game.removableCosmosObjects)
            {
                foreach (var ob in objs)
                {
                    ob.DeleteFlag = true;
                }
            }
        }

        #region Общие переменные

        /// <summary>
        /// Уровень игры.
        /// </summary>

        public static int GameLevel = 1;

        /// <summary>
        /// Размер шрифта.
        /// </summary>

        public const int FontSize = 16;

        /// <summary>
        /// Игровые очки.
        /// </summary>

        public static int GameScore;

        /// <summary>
        /// Интервал таймера (миллисекунды)
        /// </summary>
        public const int Interval = 25;

        /// <summary>
        /// Количество тиков таймера в секунду
        /// </summary>

        private const int TicksPerSecond = 1000 / Interval;

        /// <summary>
        /// Количество очков урона, получаемые от столкновения с метеоритом.
        /// </summary>

        public const int DamagePoint = 20;

        /// <summary>
        /// Количество очков лечения, получаемые от подбора батарейки.
        /// </summary>

        public const int HealPoint = 20;

        /// <summary>
        /// Космические объекты.
        /// </summary>

        #endregion

        public static class SpaceObjects
        {
            /// <summary>
            /// Объекты типа "Астероид"
            /// </summary>

            public static class ObjectAsteroid
            {
                /// <summary>
                /// Коллекция объектов типа "Астероид".
                /// </summary>

                public static readonly List<BaseObject> asteroids = new List<BaseObject>();

                /// <summary>
                /// Счетчик количества астероидов.
                /// </summary>

                public static int AsteroidCounter = AsteroidMaxCountIncrement();

                /// <summary>
                /// Повышение максимального количества астероидов с каждым последующим уровнем.
                /// </summary>

                public static int AsteroidMaxCountIncrement()
                {
                    return 5 * GameLevel;
                }

                /// <summary>
                /// Начальное время появление астероидов.
                /// </summary>

                private static readonly Point AsteroidStartSpawnTime = new Point(10, 50);

                /// <summary>
                /// Время появления астероидов.
                /// </summary>

                private static Point AsteroidSpawnTime = new Point(AsteroidStartSpawnTime.X, AsteroidStartSpawnTime.Y);

                /// <summary>
                /// Таймер-счетчик появления астероидов.
                /// </summary>

                public static int AsteroidTimerCounter;

                /// <summary>
                /// Счетчик появления астероидов.
                /// </summary>

                private static int AsteroidSpawnTimeCounter;

                #region События таймера объектов типа "Астероид"

                /// <summary>
                /// События таймера объектов типа "Астероид".
                /// </summary>

                public static void AsteroidTimerEvent()
                {
                    if (InterfaceObjects.LevelPause) return;

                    SpawnAsteroids();
                    UpdateAsteroidCounter();

                    AsteroidTimerCounter++;
                }

                /// <summary>
                /// Обновление счетчика количества астероидов и переход на следующий уровень по его окончанию.
                /// </summary>

                private static void UpdateAsteroidCounter()
                {
                    if (AsteroidCounter != 0) return;

                    GameLevel++;
                    AsteroidCounter = AsteroidMaxCountIncrement();
                    InterfaceObjects.LevelPause = true;

                    if (AsteroidSpawnTime.Y > AsteroidSpawnTime.X)
                    {
                        AsteroidSpawnTime.Y--;
                    }
                }

                /// <summary>
                /// Создание астероидов по счетчику таймера, присвоение счётчику таймера случайное значение.
                /// </summary>

                private static void SpawnAsteroids()
                {
                    if (AsteroidTimerCounter != AsteroidSpawnTimeCounter) return;

                    var rnd = new Random();

                    AsteroidSpawnTimeCounter = rnd.Next(AsteroidSpawnTime.X, AsteroidSpawnTime.Y);
                    AsteroidTimerCounter = 0;

                    if (asteroids.Count >= AsteroidCounter) return;
                    asteroids.Add(ObjectCreator.AddSpaceObjects.CreateAsteroid());
                }

                #endregion
            }

            public static class ObjectStar
            {
                /// <summary>
                /// Коллекция объектов типа "Звезда"
                /// </summary>

                public static readonly List<BaseObject> stars = new List<BaseObject>();

                /// <summary>
                /// Максимальное количество звезд.
                /// </summary>

                private const int StarsMaxCount = 100;

                /// <summary>
                /// Создание объектов типа "Звезда".
                /// </summary>

                public static void SpawnStars()
                {
                    if (stars.Count > StarsMaxCount) return;

                    stars.Add(ObjectCreator.AddSpaceObjects.CreateStar());
                }
            }
        }

        /// <summary>
        /// Объекты связанные с кораблём.
        /// </summary>

        public static class ShipObjects
        {
            public static readonly BaseObject ship = ObjectCreator.AddShipObjects.CreateShip();

            public static readonly BaseObject engineFire = ObjectCreator.AddShipObjects.CreateEngineFire();

            #region Переменные и методы объекта типа "Силовое поле"

            /// <summary>
            /// Объект типа "Силовое поле".
            /// </summary>

            public static readonly BaseObject forceField = ObjectCreator.AddShipObjects.CreateForceField();

            /// <summary>
            /// Статус работы силового поля.
            /// </summary>

            public static bool ForceFieldEnabled;

            /// <summary>
            /// Интервал отображения анимации силового поля.
            /// </summary>

            private const int ForceFieldAnimationInterval = TicksPerSecond * 3;

            /// <summary>
            /// Флаг анимации силового поля.
            /// </summary>

            public static bool ForceFieldAnimation;

            /// <summary>
            /// Флаг на сброс анимации силового поля.
            /// </summary>

            public static bool ResetForceFieldAnimation;

            /// <summary>
            /// Счетчик таймера анимации силового поля.
            /// </summary>

            public static int ForceFieldAnimationTimerCount;

            /// <summary>
            /// События таймера объекта типа "Силовое поле".
            /// </summary>

            public static void ForceFieldTimerEvents()
            {
                ForceFieldEnabled = ship.Energy > (int)(ship.FullEnergy * 0.2);

                if (ForceFieldAnimationTimerCount == ForceFieldAnimationInterval)
                {
                    ForceFieldAnimation = true;
                    ForceFieldAnimationTimerCount = 0;
                }

                ForceFieldAnimationTimerCount++;
            }

            #endregion

            /// <summary>
            /// Объект типа "Заряд силового поля".
            /// </summary>

            public static readonly BaseObject forceFieldCharge = ObjectCreator.AddInterfaceObject.CreateForceFieldCharge();



            #region Щит после получения урона

            /// <summary>
            /// Статус щита после получения урона.
            /// </summary>

            public static bool DamageShieldEnabled;

            /// <summary>
            /// Счетчик таймера щита после получения урона.
            /// </summary>

            private static int DamageShieldTimerCount;

            /// <summary>
            /// События таймера щита после получения урона.
            /// </summary>

            public static void DamageShieldTimerEvent()
            {
                if (!DamageShieldEnabled) return;

                if (DamageShieldTimerCount == TicksPerSecond)
                {
                    DamageShieldEnabled = false;
                    DamageShieldTimerCount = 0;
                }

                DamageShieldTimerCount++;
            }

            #endregion

            #region Переменные и методы объектов типа "Батарейка"

            /// <summary>
            /// Коллекция объектов типа "Батарейка".
            /// </summary>

            public static readonly List<BaseObject> batteries = new List<BaseObject>();

            /// <summary>
            /// Таймер-счетчик появления батареек.
            /// </summary>
            /// 
            public static int BatterySpawnTimerCounter;

            /// <summary>
            /// Минимальное и максимальное значение времени появления объектов типа "Батарейка".
            /// </summary>

            private static Point MinMaxBatterySpawnTime = new Point(TicksPerSecond * 10, TicksPerSecond * 30);

            /// <summary>
            /// Установить случайный интервал появления объекта "Батарейка".
            /// </summary>

            private static int SetBatterySpawnTime() => new Random().Next(MinMaxBatterySpawnTime.X, MinMaxBatterySpawnTime.Y);

            /// <summary>
            /// Интервал между появлениями батареек (в секундах)
            /// </summary>

            private static int BatterySpawnTime = SetBatterySpawnTime();

            /// <summary>
            /// События таймера объектов типа "Батарейка".
            /// </summary>

            public static void BatteryTimerEvent()
            {
                var batterySpawn = false;

                if (BatterySpawnTimerCounter == BatterySpawnTime)
                {
                    batterySpawn = true;
                    BatterySpawnTime = SetBatterySpawnTime();
                    BatterySpawnTimerCounter = 0;
                }

                if (batterySpawn) { batteries.Add(ObjectCreator.AddSpaceObjects.CreateBattery()); }

                BatterySpawnTimerCounter++;
            }

            #endregion

            #region Переменные и методы объектов типа "Снаряд"

            /// <summary>
            /// Коллекция объектов типа "Снаряд".
            /// </summary>

            public static readonly List<BaseObject> bullets = new List<BaseObject>();

            /// <summary>
            /// Доступные выстрелы.
            /// </summary>

            public static readonly List<BaseObject> availabelBullets = new List<BaseObject>();

            /// <summary>
            /// Максимальное количество снарядов.
            /// </summary>

            public const int MaxBullets = 5;

            /// <summary>
            /// Произвести выстрел.
            /// </summary>

            public static void Shot()
            {
                if (bullets.Count >= MaxBullets) return;

                GameScore -= Bullet.BulletImageSize.Height;

                bullets.Add(ObjectCreator.AddShipObjects.CreateBullet(ship));

                if (availabelBullets.Count < MaxBullets - bullets.Count) return;

                if (availabelBullets.Count > 0)
                {
                    availabelBullets.Last().DeleteFlag = true;
                }
            }

            /// <summary>
            /// Отображение доступных для выстрела снарядов.
            /// </summary>

            public static void DisplayAvailableShots()
            {
                if (availabelBullets.Count > MaxBullets) return;

                while (availabelBullets.Count < MaxBullets - bullets.Count)
                {
                    availabelBullets.Add(ObjectCreator.AddInterfaceObject.CreateDisplayBullet(availabelBullets));
                }
            }

            #endregion
        }

        /// <summary>
        /// Объекты интерфейса.
        /// </summary>

        public static class InterfaceObjects
        {
            /// <summary>
            /// Положение текста с игровыми очками.
            /// </summary>

            public static PointF GameScorePosition;

            /// <summary>
            /// Положение текста с игровым уровнем.
            /// </summary>

            public static PointF GameLevelPosition;

            /// <summary>
            /// Положение текста с количеством оставшихся астероидов.
            /// </summary>

            public static PointF GameAsteroidCountPosition;

            /// <summary>
            /// Флаг паузы после окончания уровня.
            /// </summary>

            public static bool LevelPause;

            /// <summary>
            /// Таймер счетчик паузы после окончания уровня.
            /// </summary>

            private static int LevelPauseTimerCount;

            /// <summary>
            /// Длительность паузы, после прохождения уровня.
            /// </summary>

            private const int LevelPauseInterval = TicksPerSecond * 2;

            /// <summary>
            /// Пауза после окончания уровня.
            /// </summary>

            public static void PauseAfterEndLevel()
            {
                if (!LevelPause)
                {
                    GameTextPosition();
                    return;
                }

                PauseTextPosition();

                PauseTimerEvent();
            }

            /// <summary>
            /// Позиция текста при игровом процессе.
            /// </summary>

            private static void GameTextPosition()
            {
                GameScorePosition = new PointF((float)(Game.Width * 0.25), 0);

                GameLevelPosition = new PointF(
                    Bullet.ShowBulletImageSize.Height * ShipObjects.MaxBullets,
                    Game.Height - FontSize * 2);

                GameAsteroidCountPosition = new PointF(GameLevelPosition.X * 2, GameLevelPosition.Y);
            }

            /// <summary>
            /// Положение текста при паузе после прохождения уровня.
            /// </summary>

            private static void PauseTextPosition()
            {
                var x = Game.Width / 3;
                var y = Game.Height / 2;

                GameScorePosition = new PointF(x, y - Bullet.ShowBulletImageSize.Height);
                GameLevelPosition = new PointF(x, y);
                GameAsteroidCountPosition = new PointF(x, y + Bullet.ShowBulletImageSize.Height);
            }

            /// <summary>
            /// События таймера паузы после окончания уровня.
            /// </summary>

            private static void PauseTimerEvent()
            {
                if (LevelPauseTimerCount == LevelPauseInterval)
                {
                    LevelPause = false;
                    LevelPauseTimerCount = 0;
                }

                LevelPauseTimerCount++;
            }

        }
    }
}
