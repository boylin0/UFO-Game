using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO_Game.Properties;

namespace UFO_Game
{
    public partial class frm_main
    {
        private void LoadTexture(double quality)
        {
            bUFO = new Bitmap(Resources.ufo);
            bUFO = cls_algorithm.ResizeBitmap(bUFO, Convert.ToInt32(bUFO.Width * quality), Convert.ToInt32(bUFO.Height * quality));

            bUFO_destroy = new Bitmap(Resources.ufo_destroy);
            bUFO_destroy = cls_algorithm.ResizeBitmap(bUFO_destroy, Convert.ToInt32(bUFO_destroy.Width * quality), Convert.ToInt32(bUFO_destroy.Height * quality));

            bFort = new Bitmap(Resources.fort);
            bFort = cls_algorithm.ResizeBitmap(bFort, Convert.ToInt32(bFort.Width * quality), Convert.ToInt32(bFort.Height * quality));

            bBullet = new Bitmap(Resources.bullet);
            bBullet = cls_algorithm.ResizeBitmap(bBullet, Convert.ToInt32(bBullet.Width * quality), Convert.ToInt32(bBullet.Height * quality));

            bGameover = new Bitmap(Resources.gameover);
            bGameover = cls_algorithm.ResizeBitmap(bGameover, Convert.ToInt32(bGameover.Width * quality), Convert.ToInt32(bGameover.Height * quality));

            bGameTitle = new Bitmap(Resources.gametitle);
            bGameTitle = cls_algorithm.ResizeBitmap(bGameTitle, Convert.ToInt32(bGameTitle.Width * quality), Convert.ToInt32(bGameTitle.Height * quality));

            //Gen_Texture_Floor(quality);


        }

        private void Gen_Texture_Floor(double quality)
        {
            //Bitmap bFloor_t = new Bitmap(Resources.floor);
            //bFloor_t = cls_algorithm.ResizeBitmap(bFloor_t, Convert.ToInt32(bFloor_t.Width * quality), Convert.ToInt32(bFloor_t.Height * quality));

            /*
            using (Graphics gGraphics = Graphics.FromImage(bFloor))
            {
                gGraphics.Clear(Color.Green);
                
                for (int i = 0; i <= (int)(bFloor.Width / 20.0f + 0.5f); i++)
                {
                    for (int j = 0; j <= (int)(bFloor.Height / 20.0f + 0.5f); j++)
                    {
                        gGraphics.DrawImage(bFloor_t, i * 20, j * 20, 20, 20);
                    }
                }
                
            }*/
            //Debug.s
        }
    }
}
