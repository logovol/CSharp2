using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewGame
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        // Свойства
        // Ширина и высота игрового поля
        private static int width;
        private static int height;
        public static int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 0 || value > 1000)
                    throw new ArgumentOutOfRangeException(nameof(Width), "Значение ширины должно быть в диапозоне 0-1000");
                width = value;
            }
        }

        public static int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value >= 0 && value < 40)
                    value = 40;
                if (value <= 0 || value > 1000)
                    throw new ArgumentOutOfRangeException(nameof(Height), "Значение ширины должно быть в диапозоне 0-1000");
                height = value;
            }
        }        
        static Game()
        {
        }

        public static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(-4, 1));
            _asteroids = new Asteroid[3];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(500, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(50, 80);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }

        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new System.Drawing.Rectangle(0, 0, Width, Height));
            //
            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            for(int i=0; i<_asteroids.Length;i++)
            {
                _asteroids[i].Update();
                if (_asteroids[i].Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
                    Random rnd = new Random();
                    int r = rnd.Next(50, 80);
                    _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
                }
            }
            _bullet.Update();
        }
    }
}

