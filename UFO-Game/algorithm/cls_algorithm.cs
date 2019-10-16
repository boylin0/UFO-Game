using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO_Game
{
    class cls_algorithm
    {
        
        public static bool isCollision(ObjectInfo o1, ObjectInfo o2) {
            
            if ( (o2.X + o2.Width < o1.X) || (o2.X > o1.X+o1.Width) || (o2.Y + o2.Height < o1.Y) || (o2.Y > o1.Y + o1.Height) )
            {
                    return false;
            }


            return true;
        }
    }

    class ObjectInfo
    {
        public float X = 0, Y = 0;
        public float Width = 0, Height = 0;

        public ObjectInfo(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
