using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Game
{
    class cls_algorithm
    {
        //Windows API import
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public static bool isCollision(obj_null o1, obj_null o2) {
            if ( (o2.X + o2.Width < o1.X) || (o2.X > o1.X+o1.Width) ||
                (o2.Y + o2.Height < o1.Y) || (o2.Y > o1.Y + o1.Height) ) return false;
            return true;
        }

        public static bool isClick(obj_null o1, int X, int Y)
        {
            if (GetAsyncKeyState((int)0x1 & 0x1) == 0) return false;
            if (X < o1.X || X > o1.X+o1.Width || Y < o1.Y || Y > o1.Y + o1.Height  ) return false;
            return true;
        }

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

    }

    class obj_null
    {
        public Image Image;
        public float X = 0, Y = 0;
        public float Width = 0, Height = 0;

        public obj_null(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public obj_null(Image image,float x, float y, float width, float height)
        {
            Image = image;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
