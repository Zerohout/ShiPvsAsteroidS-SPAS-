using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShiPvsAsteroidS.MainForm
{
    static class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics buffer;
        public static Timer mainTimer;

        private static Image background;
        private static PointF position;
        private static float motionVector;
        public static Form MainForm;

        private static int Width { get; set; }
        private static int Height { get; set; }

        private static void Load()
        {
            background = Image.FromFile(@"res\Earthmap.jpg");
            position = new PointF(0, 0);
            motionVector = 2;
        }

        public static void Init(Form form)
        {

            Graphics g;
            Load();

            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.Width;
            Height = form.Height;
            MainForm = form;

            buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            mainTimer = new Timer
            {
                Interval = 50
            };

            mainTimer.Start();
            mainTimer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            buffer.Graphics.Clear(Color.Blue);

            buffer.Graphics.DrawImage(background, position);

            buffer.Render();
        }

        private static void Update()
        {
            position = new PointF(position.X -= motionVector, 0);
            if (position.X < (background.Width - (background.Width / 3)) * -1)
            {
                position.X = 0;
            }
        }
    }
}
