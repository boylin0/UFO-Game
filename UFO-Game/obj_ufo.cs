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
        public int X = 0, Y = 0;
        public int Width = 50, Height = 50;
        public int DropSpeed = 2;
        public int FlyOffsetX = 0;
        public int lastTick = 0;
        public int OffsetInterval_X = 1000;
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

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }

    }
}
