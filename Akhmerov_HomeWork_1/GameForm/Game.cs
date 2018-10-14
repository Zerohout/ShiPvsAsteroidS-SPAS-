using ObjectCreator = Akhmerov_HomeWork_1.Objects.ObjectCreator;
using ObjectValues = Akhmerov_HomeWork_1.Objects.ObjectValues;

namespace Akhmerov_HomeWork_1.GameForm
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Objects;
    using Objects.Controlable;

    using static ObjectCreator.AddGeneralObjects;
    
    using static ObjectValues;
    using static ObjectValues.SpaceObjects.ObjectAsteroid;
    using static ObjectValues.SpaceObjects.ObjectStar;
    using static ObjectValues.ShipObjects;
    using static ObjectValues.InterfaceObjects;

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
                ship,
                engineFire,
                forceFieldCharge,
                forceField
            };

            removableCosmosObjects = new List<List<BaseObject>>
            {
                stars,
                asteroids,
                bullets,
                availabelBullets,
                explosives,
                batteries
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
            SetStartValues();
            _context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            Width = form.Width;
            Height = form.Height;
            GameForm = form;

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            gameTimer = new Timer { Interval = Interval };
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

            PauseAfterEndLevel();

            BatteryTimerEvent();
            AsteroidTimerEvent();
            ForceFieldTimerEvents();
            DamageShieldTimerEvent();
            
            SpawnStars();
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
                $"Очки: {GameScore}",
                new Font("Comic Sans MS", FontSize),
                new SolidBrush(Color.Orange),
                GameScorePosition);

            Buffer.Graphics.DrawString(
                $"Уровень: {GameLevel}",
                new Font("Comic Sans MS", FontSize),
                new SolidBrush(Color.Orange),
                GameLevelPosition);

            Buffer.Graphics.DrawString(
                $"Астероидов осталось: {AsteroidCounter}",
                new Font("Comic Sans MS", FontSize),
                new SolidBrush(Color.Orange),
                GameAsteroidCountPosition);

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
            DisplayAvailableShots();
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

                    if (objects != asteroids) continue;
                    AsteroidCounter--;
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

            for (var i = 0; i < asteroids.Count; i++)
            {
                for (var j = 0; j < bullets.Count; j++)
                {
                    if (tempCounter.Count > 0 && tempCounter.Contains(j)) continue;

                    if (!asteroids[i].Collision(bullets[j])) continue;

                    explosives.Add(CreateExplosion(asteroids, i));

                    GameScore += asteroids[i].Size.Width;
                    asteroids[i].DeleteFlag = true;

                    bullets[j].DeleteFlag = true;

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
            foreach (var obj in asteroids)
            {
                if (!obj.Collision(ship)) continue;
                ResetForceFieldAnimation = true;
                GameScore -= obj.Size.Width * asteroids.Count;
                shipGetDamage = true;
                break;
            }

            if (!shipGetDamage) return;
            if (DamageShieldEnabled) return;

            GetDamage();
        }

        /// <summary>
        /// Действие при получение кораблем урона.
        /// </summary>

        private static void GetDamage()
        {
            var tempCount = asteroids.Count;
            for (var i = 0; i < tempCount; i++)
            {
                explosives.Add(CreateExplosion(asteroids, i));

                asteroids[i].DeleteFlag = true;
            }
        }

        /// <summary>
        /// Столкновение батарейки с кораблем
        /// </summary>

        private static void BatteryShipCollision()
        {
            foreach (var obj in batteries)
            {
                if (!obj.Collision(ship)) continue;
                if (ship.Energy == ship.FullEnergy) continue;

                ResetForceFieldAnimation = true;
                GameScore -= obj.Size.Width;
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

