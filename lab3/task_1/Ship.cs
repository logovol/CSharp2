using System;
using System.Drawing;
using System.Globalization;

namespace task_1
{
    class Ship : BaseObject
    {
        Bitmap imgShip;
        public static event Message MessageDie;
        public static event Log Damage;

        private int _energy = 100;
        private int _score = 0;
        public int Energy => _energy;
        public int Score => _score;

        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public void EnergyUp()
        {
            _energy = 100;
        }

        public void AddScore(int n)
        {
            _score += n;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            imgShip = new Bitmap(Image.FromFile("img/Shuttle.png"), Size.Width, Size.Height);
        }
        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawImage(imgShip, Pos.X, Pos.Y, Size.Width, Size.Width);
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height - this.Size.Height - 5) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }
        public void Right()
        {
            if (Pos.X < Game.Width - this.Size.Width) Pos.X = Pos.X + Dir.X;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }

        public void Crash()
        {
            Damage?.Invoke(this, "Удар об астерид");
        }
    }

}
