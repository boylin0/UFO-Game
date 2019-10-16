using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Game
{
    class obj_ufo
    {
        public Image Image;
        public float X = 0, Y = 0;
        public int Width = 50, Height = 50;
        public float DropSpeed = 2;
        public float LowestSpeed = 0.5f;
        public float AccelerationSpeed = 5;
        public int FlySwaySpeed = 0;
        public int lastTick = 0;
        public int SwayInterval = 1000;
        public bool destroy = false;
        public int destroyTime = 0;
        
        public obj_ufo(Image image, int width = 50, int height = 50, int x = 0, int y = 0)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Image = image;
        }

        public void Move(float x, float y)
        {
            X += x;
            Y += y;
        }

    }

    class obj_bullet
    {
        public Image Image;
        public float X = 0, Y = 0;
        public int Width = 50, Height = 50;
        public float LowestSpeed = 3.5f;
        public float AccelerationSpeed = 15.0f;

        public bool destroy = false;
        public int destroyTime = 0;
    }
}
