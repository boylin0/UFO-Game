using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Game
{
    class obj_bullet
    {
        public Image Image;
        public float X = 0, Y = 0;
        public int Width = 30, Height = 40;
        public float LowestSpeed = 5.0f;
        public float AccelerationSpeed = 10.0f;
        public bool destroy = false;
        public int destroyTime = 0;
        public bool isSuperBullet = false;
    }
}
