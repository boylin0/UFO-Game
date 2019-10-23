using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Game
{
    class obj_fort
    {
        public Image Image;
        public float X = 0, Y = 0;
        public float Width = 50, Height = 50;
        public float Speed_X = 0.0f , Speed_Y = 0.0f;
        public int FirePower = 1;
        public int MaxFirePower = 8;
        public float MaxSpeed = 10.0f;
        public int timestamp_LastShoot = 0;
        public void Move(float x, float y)
        {
            X += (float)x;
            Y += (float)y;
        }
    }
}
