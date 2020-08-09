
using System;
using System.Drawing;
using System.Text;

namespace task_1
{
    class Medecine : BaseObject
    {
        public static event Log Health;
        Bitmap imgMed;
        public bool Edge { get; private set; }

        public Medecine(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            imgMed = new Bitmap(Image.FromFile("img/Medecine.png"), Size.Width, Size.Height);
            Edge = false;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(imgMed, Pos.X, Pos.Y, Size.Width, Size.Width);
        }
        public override void Update()
        {
            Pos.X = Pos.X - 10;
            if (Pos.X < 0)
                Edge = true;
        }
        public void GetHealth()
        {
            Health?.Invoke(this, "Корабль восстановлен");
        }
    }
}
