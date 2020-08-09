using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace task_1
{
    class Game
    {
        private static Timer timer;
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(50, 50));
        private static Stopwatch sw = new Stopwatch();
        private static string log = string.Empty;       

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

        protected static int asteroidsStartCount = 2;
        public static BaseObject[] _objs;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static List<Asteroid> _asteroids = new List<Asteroid>();
        private static Medecine _med;
        protected static bool _medShown = false;
        
        public static void Load()
        {
            try
            {
                _objs = new BaseObject[30];                                
                var rnd = new Random();
                for (var i = 0; i < _objs.Length; i++)
                {
                    int r = rnd.Next(5, 50);
                    _objs[i] = new Star(new Point(500, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(2, 2));
                }
                for (var i = 0; i < asteroidsStartCount; i++)
                {
                    int r = rnd.Next(50, 80);
                    _asteroids.Add(new Asteroid(new Point(1000, rnd.Next(0, Game.Height - 100)), new Point(-r / 5, r), new Size(r, r)));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Init(Form form)
        {
            form.FormClosed += WriteEndOfGame;
            LogFileWriter($"{DateTime.Now} Начало игры");
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
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
            Ship.Damage += WriteLog;
            Bullet.Damage += WriteLog;
            Medecine.Health += WriteLog;            

            //Load();

            timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            sw.Start();
        }

        private static void WriteEndOfGame(object sender, FormClosedEventArgs e)
        {
            LogFileWriter($"{DateTime.Now} Конец игры");
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + 60, _ship.Rect.Y + 24), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up)
                _ship.Up();
            if (e.KeyCode == Keys.Down)
                _ship.Down();
            if (e.KeyCode == Keys.Right)
                _ship.Right();
            if (e.KeyCode == Keys.Left)
                _ship.Left();
        }


        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }

            foreach (Bullet b in _bullets)
                b.Draw();

            _med?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("Score:" + _ship.Score, SystemFonts.DefaultFont, Brushes.White, 65, 0);
            }
            Buffer.Graphics.DrawString(log, SystemFonts.DefaultFont, Brushes.White, 200, 0);
            Buffer.Render();
                        
        }

        public static void Update()
        {
            List<Bullet> bulletsDel = _bullets.FindAll(x => x.Del == true);
            for (int index = 0; index < bulletsDel.Count; index++)
            {                
                _bullets.Remove(bulletsDel[index]);
                bulletsDel[index] = null;
            }
            bulletsDel.Clear();

            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Bullet b in _bullets)
            {                
                b.Update();
            }
            _med?.Update();
            for (var i = 0; i < _asteroids.Count; i++)
            {   
                _asteroids[i].Update();

                for (int j = 0; j < _bullets.Count && i >= 0; j++)
                                        
                    if (_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();                        
                        _bullets[j].Crash();
                        _bullets[j] = null;
                        _bullets.RemoveAt(j);
                        
                        _ship?.AddScore(5);

                        ///////////////////////////
                        _asteroids[i] = null;
                        _asteroids.RemoveAt(i);
                        i--;
                        if (_asteroids.Count == 0)
                        {
                            AddAsteroids();                         
                        }                            

                        continue;
                    }
                
                if (_med != null && _med.Edge)
                {
                    _medShown = false;
                    _med = null;
                    sw.Restart();
                }
                    
                if (_med != null && _med.Collision(_ship))
                {
                    _med.GetHealth();
                    _ship.EnergyUp();
                    _medShown = false;
                    _med = null;
                    sw.Restart();
                    continue;
                }

                TimeSpan ss = sw.Elapsed;
                if (ss.Seconds >= 20 && !_medShown)
                {
                    Random rn = new Random();
                    int r = rn.Next(50, 80);
                    _med = new Medecine(new Point(1000, rn.Next(0, Game.Height - 100)), new Point(-r / 5, r), new Size(50, 50));
                    _medShown = !_medShown;
                }

                if (i >= 0)
                {
                    if (!_ship.Collision(_asteroids[i]))
                        continue;
                    _ship?.Crash();
                    var rnd = new Random();
                    _ship?.EnergyLow(rnd.Next(1, 10));
                    System.Media.SystemSounds.Asterisk.Play();

                    ///////////////////
                    _asteroids[i] = null;
                    _asteroids.RemoveAt(i);
                    i--;
                    if (_asteroids.Count == 0)
                        AddAsteroids();

                    if (_ship.Energy <= 0)
                        _ship?.Die();
                }
            }
        }

        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        private static void WriteLog(Object o, string s)
        {
            log = $"{DateTime.Now} {s}";
            LogFileWriter(log);
        }
        private static void Delete(Object o)
        {
            o = null;
            _bullets.Remove(o as Bullet);            
        }

        private static void LogFileWriter(string log)
        {
            string writePath = "log.txt";

            try
            {
                using (StreamWriter swr = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    swr.WriteLine(log);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void AddAsteroids()
        {
            try
            {
                asteroidsStartCount++;                
                var rnd = new Random();                
                for (var i = 0; i < asteroidsStartCount; i++)
                {
                    int r = rnd.Next(50, 80);
                    _asteroids.Add(new Asteroid(new Point(1000, rnd.Next(0, Game.Height - 100)), new Point(-r / 5, r), new Size(r, r)));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}

