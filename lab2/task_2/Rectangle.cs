using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NewGame
{
    class Rectangle : BaseObject
    {

        public Rectangle(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
