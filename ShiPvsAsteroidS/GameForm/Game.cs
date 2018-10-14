using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ShiPvsAsteroidS.MainForm;
using ShiPvsAsteroidS.Objects;
using ShiPvsAsteroidS.Objects.Controlable;
using ObjectCreator = ShiPvsAsteroidS.Objects.ObjectCreator;
using ObjectValues = ShiPvsAsteroidS.Objects.ObjectValues;

namespace ShiPvsAsteroidS.GameForm
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        
        private static List<BaseObject> explosives, shipComponents;

        public static List<List<BaseObject>> removableCosmosObjects;

        public static Timer gameTimer;

        private static float backgroundVector;

        static readonly Image background = new Bitmap(@"res\Cosmos.png");


        public static bool shipGetDamage, shipGetCharge;

        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static Form GameForm { get; private set; }
        
        /// <summary>
        /// Инициализация, загрузка и присвоение свойств объектам.
        /// </summary>

        private static void Load()
        {
            explosives = new List<BaseObject>();

            shipComponents = new List<BaseObject>
            {
                ObjectValues.ShipObjects.ship,
                ObjectValues.ShipObjects.engineFire,
                ObjectValues.ShipObjects.forceFieldCharge,
                ObjectValues.ShipObjects.forceField
            };

            removableCosmosObjects = new List<List<BaseObject>>
            {
                ObjectValues.SpaceObjects.ObjectStar.stars,
                ObjectValues.SpaceObjects.ObjectAsteroid.asteroids,
                ObjectValues.ShipObjects.bullets,
                ObjectValues.ShipObjects.availabelBullets,
                explosives,
                ObjectValues.ShipObjects.batteries
            };
        }

        /// <summary>
        /// Инициализация графики.
        /// </summary>
        /// <param name="form">
        /// Форма, в которой произойдет инициализация графики
        /// </param>

        public void Init(Form form)
        {
            ObjectValues.SetStartValues();
            _context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            Width = form.Width;
            Height = form.Height;
            GameForm = form;

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            gameTimer = new Timer { Interval = ObjectValues.Interval };
            gameTimer.Start();
            gameTimer.Tick += Timer_Tick;
        }

        /// <summary>
        /// События при каждом тике таймера.
        /// </summary>

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Program.CloseGame)
            {
                ExitGame();
                return;
            }

            TimerEvents();
            Draw();
            Update();

            WindowSizeCheck();

            Cursor.Clip = new Rectangle(
                new Point(GameForm.Location.X, GameForm.Location.Y),
                new Size(GameForm.Bounds.Width / 2, GameForm.Bounds.Height - Ship.ShipImageSize.Height));
        }

        /// <summary>
        /// События таймера.
        /// </summary>

        private static void TimerEvents()
        {
            if (backgroundVector < (background.Width / 2) * -1)
            {
                backgroundVector = 0;
            }

            ObjectValues.InterfaceObjects.PauseAfterEndLevel();

            ObjectValues.ShipObjects.BatteryTimerEvent();
            ObjectValues.SpaceObjects.ObjectAsteroid.AsteroidTimerEvent();
            ObjectValues.ShipObjects.ForceFieldTimerEvents();
            ObjectValues.ShipObjects.DamageShieldTimerEvent();
            
            ObjectValues.SpaceObjects.ObjectStar.SpawnStars();
        }

        /// <summary>
        /// Прорисовка и перерисовка объектов.
        /// </summary>

        public void Draw()
        {

            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawImage(background, new PointF(backgroundVector -= 0.5f, 0));

            DrawObjectCollection();

            Buffer.Graphics.DrawString(
                $"Очки: {ObjectValues.GameScore}",
                new Font("Comic Sans MS", ObjectValues.FontSize),
                new SolidBrush(Color.Orange),
                ObjectValues.InterfaceObjects.GameScorePosition);

            Buffer.Graphics.DrawString(
                $"Уровень: {ObjectValues.GameLevel}",
                new Font("Comic Sans MS", ObjectValues.FontSize),
                new SolidBrush(Color.Orange),
                ObjectValues.InterfaceObjects.GameLevelPosition);

            Buffer.Graphics.DrawString(
                $"Астероидов осталось: {ObjectValues.SpaceObjects.ObjectAsteroid.AsteroidCounter}",
                new Font("Comic Sans MS", ObjectValues.FontSize),
                new SolidBrush(Color.Orange),
                ObjectValues.InterfaceObjects.GameAsteroidCountPosition);

            Buffer.Render();
        }

        /// <summary>
        /// Обновление свойств объектов.
        /// </summary>

        private static void Update()
        {
            AsteroidBulletCollison();
            AsteroidShipCollision();
            BatteryShipCollision();
            ObjectValues.ShipObjects.DisplayAvailableShots();
            Clean();

            UpdateObjectCollection();
        }

        /// <summary>
        /// Удаление помеченных флагом объектов.
        /// </summary>

        private static void Clean()
        {
            foreach (var objects in removableCosmosObjects)
            {
                for (var i = 0; i < objects.Count; i++)
                {
                    if (!objects[i].DeleteFlag) continue;

                    objects[i].Image.Dispose();
                    objects.RemoveAt(objects.IndexOf(objects[i]));

                    if (objects != ObjectValues.SpaceObjects.ObjectAsteroid.asteroids) continue;
                    ObjectValues.SpaceObjects.ObjectAsteroid.AsteroidCounter--;
                }
            }
        }

        #region Отрисовка/обновление колекций объектов

        /// <summary>
        /// Обновление коллекций объектов.
        /// </summary>

        private static void UpdateObjectCollection()
        {
            foreach (var components in shipComponents)
            {
                components.Update();
            }

            foreach (var activeobj in removableCosmosObjects)
            {
                foreach (var obj in activeobj)
                {
                    obj.Update();
                }
            }
        }

        /// <summary>
        /// Отрисовка коллекций объектов
        /// </summary>

        private static void DrawObjectCollection()
        {
            foreach (var components in shipComponents)
            {
                components.Draw();
            }

            foreach (var activeObj in removableCosmosObjects)
            {
                foreach (var obj in activeObj)
                {
                    obj.Draw();
                }
            }
        }

        #endregion

        #region Objects Collisions (Столкновения объектов)

        /// <summary>
        /// Столкновение снаряда с астероидом.
        /// </summary>

        private static void AsteroidBulletCollison()
        {
            var tempCounter = new List<int>();

            for (var i = 0; i < ObjectValues.SpaceObjects.ObjectAsteroid.asteroids.Count; i++)
            {
                for (var j = 0; j < ObjectValues.ShipObjects.bullets.Count; j++)
                {
                    if (tempCounter.Count > 0 && tempCounter.Contains(j)) continue;

                    if (!ObjectValues.SpaceObjects.ObjectAsteroid.asteroids[i].Collision(ObjectValues.ShipObjects.bullets[j])) continue;

                    explosives.Add(ObjectCreator.AddGeneralObjects.CreateExplosion(ObjectValues.SpaceObjects.ObjectAsteroid.asteroids, i));

                    ObjectValues.GameScore += ObjectValues.SpaceObjects.ObjectAsteroid.asteroids[i].Size.Width;
                    ObjectValues.SpaceObjects.ObjectAsteroid.asteroids[i].DeleteFlag = true;

                    ObjectValues.ShipObjects.bullets[j].DeleteFlag = true;

                    tempCounter.Add(j);
                }
            }

            tempCounter.Clear();
        }

        /// <summary>
        /// Столкновение астероида с кораблем.
        /// </summary>

        private static void AsteroidShipCollision()
        {
            foreach (var obj in ObjectValues.SpaceObjects.ObjectAsteroid.asteroids)
            {
                if (!obj.Collision(ObjectValues.ShipObjects.ship)) continue;
                ObjectValues.ShipObjects.ResetForceFieldAnimation = true;
                ObjectValues.GameScore -= obj.Size.Width * ObjectValues.SpaceObjects.ObjectAsteroid.asteroids.Count;
                shipGetDamage = true;
                break;
            }

            if (!shipGetDamage) return;
            if (ObjectValues.ShipObjects.DamageShieldEnabled) return;

            GetDamage();
        }

        /// <summary>
        /// Действие при получение кораблем урона.
        /// </summary>

        private static void GetDamage()
        {
            var tempCount = ObjectValues.SpaceObjects.ObjectAsteroid.asteroids.Count;
            for (var i = 0; i < tempCount; i++)
            {
                explosives.Add(ObjectCreator.AddGeneralObjects.CreateExplosion(ObjectValues.SpaceObjects.ObjectAsteroid.asteroids, i));

                ObjectValues.SpaceObjects.ObjectAsteroid.asteroids[i].DeleteFlag = true;
            }
        }

        /// <summary>
        /// Столкновение батарейки с кораблем
        /// </summary>

        private static void BatteryShipCollision()
        {
            foreach (var obj in ObjectValues.ShipObjects.batteries)
            {
                if (!obj.Collision(ObjectValues.ShipObjects.ship)) continue;
                if (ObjectValues.ShipObjects.ship.Energy == ObjectValues.ShipObjects.ship.FullEnergy) continue;

                ObjectValues.ShipObjects.ResetForceFieldAnimation = true;
                ObjectValues.GameScore -= obj.Size.Width;
                shipGetCharge = true;
                obj.DeleteFlag = true;
            }
        }

        #endregion

        /// <summary>
        /// Проверка размера формы на наличие исключения.
        /// </summary>

        private static void WindowSizeCheck()
        {
            if (Width > 1000 || Height > 1000)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Выход из игры.
        /// </summary>

        private static void ExitGame()
        {
            Buffer.Dispose();
            _context.Dispose();
            gameTimer.Stop();
            GameForm.Dispose();
            GameForm.Close();
            GC.Collect();
            SplashScreen.MainForm.Show();
            SplashScreen.Init(SplashScreen.MainForm);
            SplashScreen.Draw();
            Program.CloseGame = false;
        }
    }
}

