using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.IO;
using System.Collections.Generic;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game() { }
        static Random r = new Random();
        public static Form f;
        public static Timer timer;
        public static int LifeCount { get; set; } = 9;
        public static int Ammo { get; set; } = 3;
        public static int AsteroidsCount { get; set; } = 3;
        public static List<Image> Scores { get => scores; set => scores = value; }

        private static System.Media.SoundPlayer soundPlayer;
        private static List<Image> scores = new List<Image>();
        public static Log l = Log;
        public static void Init(Form form, CheckBox sound)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width < 1000 && form.ClientSize.Width > 0 ? form.ClientSize.Width : throw new ArgumentOutOfRangeException();
            Height = form.ClientSize.Height < 1000 && form.ClientSize.Height > 0 ? form.ClientSize.Height : throw new ArgumentOutOfRangeException();
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            form.Icon = Icon.ExtractAssociatedIcon(@"Resources/icon.ico");

            Load();
            timer = new Timer { Interval = 10 };
            timer.Start();
            timer.Tick += Timer_Tick;
            f = form;
            form.KeyDown += Form_KeyDown;
            System.Media.SoundPlayer soundPlayer = sound.Checked ? new System.Media.SoundPlayer(@"Resources/Nyan.wav") : null;
            soundPlayer?.PlayLooping();
            l("Инициализация игры выполнена");
        }
        /// <summary>
        /// обрабатываем нажатия кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Control) && Ammo != 0)
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + 80, _ship.Rect.Y + 4), new Point(2, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) _ship.Down();
            l($"Нажата кнопка {e.KeyCode}");
        }
        /// <summary>
        /// отрисовываем все объекты
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.DarkBlue);
            foreach (var s in _objs) s?.Draw();
            foreach (var a in _asteroids) a?.Draw();
            foreach (var b in _bullets) b?.Draw();
            _ship.Draw();
            _health?.Draw();
        }
        /// <summary>
        /// обновляем позицию всех объектов и обрабатываем столкновения
        /// </summary>
        public static void Update()
        {
            foreach (var stars in _objs) stars?.Update();

            for (int i = _asteroids.Count-1; i >= 0; i--)
            {
                _asteroids[i]?.Update();
                if (_asteroids[i] !=null && _asteroids[i].Collision(_ship)) //столкновение астероида с кораблем
                {
                    _ship = new Ship(new Point(0, r.Next(70, Height - 40)), new Point(0, 1), new Size(90, 40));
                    LifeCount--;
                }

                for (int j = _bullets.Count-1; j >=0; j--) //цикл для столкновения астероида с пулей
                {
                    _bullets[j]?.Update();
                    if (_bullets[j] != null && _asteroids[i] != null && _asteroids[i].Collision(_bullets[j]))
                    {
                        //_asteroids[i].Dispose();
                        //_asteroids.RemoveAt(i);
                        //_bullets.RemoveAt(j);
                        _asteroids[i] = null;
                        _bullets[j] = null;
                    }
                }
            }
            _asteroids.RemoveAll(item => item == null);
            if (_asteroids.Count <= 0)
            {
                AsteroidsCount++;
                AddAsteroids();
            }
            _health?.Update();
            if (_health.Collision(_ship)) //для столкновения кота и еды
            {
                if (_health.toScore != null) Scores.Add(_health.toScore);
                _health = new Health(new Point(-100, -100), new Point(-5, 0), new Size(60, 60));
                l.Invoke("Кот столкнулся с едой");
            }
            _ship.Update();
        }

        public static BaseObject[] _objs;
        public static Ship _ship;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static List<Asteroid> _asteroids = new List<Asteroid>();
        public static Glasses _glasses;
        public static Health _health;

        /// <summary>
        /// создаём объекты
        /// </summary>
        public static void Load()
        {
            int r;
            Random random = new Random();
            _objs = new BaseObject[30];
            _ship = new Ship(new Point(0, Height / 2), new Point(5, 5), new Size(90, 40));
            _glasses = new Glasses(new Point(Width / 2, 0), new Point(0, 5));
            _health = new Health(new Point(-70, random.Next(100, Height - 100)), new Point(0, 0), new Size(60, 60));
            for (int i = 0; i < _objs.Length; i++)
            {
                r = Game.r.Next(15, 35);
                _objs[i] = new Star(
                    new Point(Game.r.Next(50, Width), Game.r.Next(50, Height)),
                    new Point(45 - i, 45 - i),
                    new Size(r, r));
            }
            AddAsteroids();
            l("Объекты загружены");
        }
        /// <summary>
        /// добавляем астероиды
        /// </summary>
        public static void AddAsteroids()
        {
            for (int i = 0; i < AsteroidsCount; i++)
            {
                Random random = new Random();
                int r = random.Next(5, 10) / 2;
                _asteroids.Add(new Asteroid(
                    new Point(Game.r.Next(300, Width), Game.r.Next(50, Height)),
                    new Point(-r / 2, r), //(-r / 2, r)
                    new Size(70, 70)));
            }
        }
        /// <summary>
        /// игровой процесс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
            ChangeFormText();
            CheckScore();
            Buffer.Render();
        }

        /// <summary>
        /// провера счёта
        /// </summary>
        private static void CheckScore()
        {

            if (LifeCount > 0)
            {
                Image life = Image.FromFile(@"Resources/life.png");
                int width = 5;
                for (int i = 0; i < LifeCount; i++)
                {
                    Buffer.Graphics.DrawImage(life, width, 20, 30, 30);
                    width += life.Width;
                }
            }
            if (LifeCount <= 0)
            {
                timer.Tick -= Timer_Tick;
                Buffer.Render();
                Thread.Sleep(2000);
                soundPlayer?.Stop();
                timer.Tick += GameOver_Tick;
            }

            for (int i = 0; i < Scores.Count; i++)
            {

                Buffer.Graphics.DrawImage(Scores[i], i * 30, 60, 30, 30);
            }
        }
        static Image asteroid5 = Image.FromFile(@"Resources/asteroid5.png");
        /// <summary>
        /// отображение экрана game over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void GameOver_Tick(object sender, EventArgs e)
        {
            Buffer.Graphics.Clear(Color.Aqua);
            Buffer.Graphics.DrawImage(asteroid5, Width / 2 - 50, Height / 2 - 50, 100, 100);

            Buffer.Graphics.DrawString($"Ваш счёт: {scores.Count}\nИгра окончена\nДогик победил", new Font("Arial", 25), Brushes.White, new Point(Width / 3, Height - Height / 3 + 50));
            _glasses.Draw();
            _glasses.Update();
            Buffer.Render();
        }
        /// <summary>
        /// дублирование счёта в заголовок
        /// </summary>
        private static void ChangeFormText()
        {
            f.Text = $"Патронов: {Ammo} Жизней: {LifeCount} Счёт: {Scores.Count}";
        }
        static void Log(string str)
        {
            string file = @"log.txt";
            string content = File.ReadAllText(file);
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine($"{content}{DateTime.Now} {str}");
            sw.Close();
        }
    }
}