using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace NewGame
{    
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }

}
