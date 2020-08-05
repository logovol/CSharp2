using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace task_1
{    
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }

}
