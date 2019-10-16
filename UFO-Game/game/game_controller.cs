using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UFO_Game.Properties;

namespace UFO_Game
{
    public partial class frm_main
    {
        //Windows API import
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);


        //initialize global variable
        Bitmap bFrame; //rendering frame
        Random rand = new Random(); //random generator

        //Game infomation
        int gameScore = 0;
        bool gameover = false;
        bool firstgame = false;

        //Object list
        obj_fort oFort;
        obj_null oTitleLogo;
        List<obj_ufo> lstUFO = new List<obj_ufo>();
        List<obj_ufo> rmlstUFO = new List<obj_ufo>();
        List<obj_bullet> lstBullet = new List<obj_bullet>();
        List<obj_bullet> rmlstBullet = new List<obj_bullet>();

        //Texture
        Bitmap bUFO;
        Bitmap bUFO_destroy;
        Bitmap bFort;
        Bitmap bBullet;
        Bitmap bGameover;
        Bitmap bGameTitle;

        //Text
        Font fFont = new System.Drawing.Font("Arial", 16);
        String txt_restart_prompt = "Press [R] to start game";


        private void InitializeGame()
        {
            // initialize texture
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            LoadTexture(0.7);

            // Clear objects
            lstUFO.Clear();
            rmlstUFO.Clear();
            lstBullet.Clear();
            rmlstBullet.Clear();

            // setup fort object
            oFort = new obj_fort();
            oFort.Height = 75;
            oFort.Width = 50;
            oFort.X = (pictureBox1.Width / 2) - (oFort.Width / 2);
            oFort.Y = pictureBox1.Height - oFort.Height - 50;
            oFort.Image = bFort;

            // setup objects
            oTitleLogo = new obj_null((pictureBox1.Width / 2) - (370 / 2), Convert.ToInt32(((pictureBox1.Height / 2) - (230 / 2)) * 0.5), 370, 230);

            // clear game information
            gameScore = 0;
            gameover = false;
        }


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
        }

        private void ObjectMove()
        {
            // Gameover - Restart game
            if (gameover && (GetAsyncKeyState((int)0x52) & 0x1) != 0)
            {
                firstgame = false;
                txt_restart_prompt = "Press [R] to restart game";
                InitializeGame();
            }

            // Auto pause
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32() || gameover) return;

            // Keyboard input
            if (GetAsyncKeyState((int)0x27) != 0) //Right - Move right
            {
                oFort.X += 10;
            }
            if (GetAsyncKeyState((int)0x25) != 0) //Left - Move left
            {
                oFort.X -= 10;
            }
            if (GetAsyncKeyState((int)0x20) != 0 //Space - Shoot Key
                && Environment.TickCount - oFort.timestamp_LastShoot > 300 | chk_godMod.Checked)
            {
                obj_bullet oBullet = new obj_bullet();
                oBullet.Image = bBullet;
                oBullet.Height = 50;
                oBullet.Width = 30;
                oBullet.X = (oFort.X + (oFort.Width / 2)) - (oBullet.Width / 2);
                oBullet.Y = oFort.Y - (oBullet.Height / 2);
                lstBullet.Add(oBullet);
                oFort.timestamp_LastShoot = Environment.TickCount;
            }

            // Prevent fort out of bounds
            if (oFort.X > pictureBox1.Width - oFort.Width) oFort.X = pictureBox1.Width - oFort.Width;
            if (oFort.X < 0) oFort.X = 0;

            // UFO controller
            foreach (obj_ufo oUfo in lstUFO)
            {
                // Remove destroy UFO
                if (oUfo.destroy)
                {
                    if (Environment.TickCount - oUfo.destroyTime > 1000)
                    {
                        rmlstUFO.Add(oUfo);
                    }
                    continue;
                }


                // UFO collision to fort
                if (cls_algorithm.isCollision(new obj_null(oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height), new obj_null(oFort.X, oFort.Y, oFort.Width, oFort.Height)) == true)
                {
                    oUfo.destroy = true;
                    oUfo.Image = bUFO_destroy;
                    oUfo.destroyTime = Environment.TickCount;
                    gameover = true;
                }


                // UFO dropdown
                float rate = (float)((pictureBox1.Height - (oUfo.Y + oUfo.Height)) / pictureBox1.Height); //calculate rate to bottom
                oUfo.DropSpeed = oUfo.LowestSpeed + oUfo.AccelerationSpeed * (1.0f - rate); //acceleration

                // Random sway
                if (Environment.TickCount - oUfo.lastTick > oUfo.SwayInterval)
                {
                    oUfo.FlySwaySpeed = rand.Next(-7, 7);
                    oUfo.lastTick = Environment.TickCount;
                }
                // HACK - disable sway
                if (chk_ufosway.Checked)
                    oUfo.Move(oUfo.FlySwaySpeed, oUfo.DropSpeed);
                else
                    oUfo.Move(0, oUfo.DropSpeed);

                // Touch bottom
                if (oUfo.Y > this.Height)
                {
                    if (chk_autoDestroy.Checked) rmlstUFO.Add(oUfo);
                    oUfo.Y = -oUfo.Height;
                }
                // Touch right
                if (oUfo.X > this.Width)
                {
                    oUfo.X = -oUfo.Width;
                }
                // Touch left
                if (oUfo.X < -oUfo.Width)
                {
                    oUfo.X = this.Width;
                }
            }

            //Bullet controller
            foreach (obj_bullet oBullet in lstBullet)
            {

                //move
                float rate = (float)((pictureBox1.Height - oBullet.Y) / pictureBox1.Height); //calculate rate to top
                oBullet.Y -= oBullet.LowestSpeed + oBullet.AccelerationSpeed * (1.0f - rate);

                //touch top
                if (oBullet.Y < -oBullet.Height)
                {
                    rmlstBullet.Add(oBullet);
                }

                foreach (obj_ufo oUfo in lstUFO)
                {
                    if (oUfo.destroy == false && cls_algorithm.isCollision(new obj_null(oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height), new obj_null(oBullet.X, oBullet.Y, oBullet.Width, oBullet.Height)) == true)
                    {
                        oUfo.destroy = true;
                        oUfo.Image = bUFO_destroy;
                        oUfo.destroyTime = Environment.TickCount;
                        rmlstBullet.Add(oBullet);

                        gameScore++;
                    }
                }

            }

            //Remove Ufo
            foreach (obj_ufo oUfo in rmlstUFO)
            {
                lstUFO.Remove(oUfo);
            }

            //Remove bullet
            foreach (obj_bullet oBullet in rmlstBullet)
            {
                lstBullet.Remove(oBullet);
            }
        }

    }
}
