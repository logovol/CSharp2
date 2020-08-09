using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace task_1
{
    class Asteroid : BaseObject
    {
        Bitmap img;
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
            img = new Bitmap(Image.FromFile("img/asteroid.png"), Size.Width, Size.Height);
        }
        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        ~Asteroid()
        {
            
        }       
        
    }

}
