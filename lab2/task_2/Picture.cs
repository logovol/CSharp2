using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NewGame
{   
    class Picture : BaseObject
    {
        Bitmap img;

        public Picture(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            img = new Bitmap(Image.FromFile("earth.png"), Size.Width, Size.Height);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
