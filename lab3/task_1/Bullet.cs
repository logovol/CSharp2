using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace task_1
{
    class Bullet : BaseObject
    {
        public static event Log Damage;
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {            
            Pos.X = Pos.X + 20;
        }
        public void Crash()
        {
            Damage?.Invoke(this, "Сбит астероид");
        }
    }
}
