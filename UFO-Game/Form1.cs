using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UFO_Game.Properties;

namespace UFO_Game
{
    public partial class Form1 : Form
    {
        //Windows API import
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
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
        List<obj_ufo> lstUFO = new List<obj_ufo>();
        List<obj_ufo> rmlstUFO = new List<obj_ufo>();
        List<obj_bullet> lstBullet = new List<obj_bullet> ();
        List<obj_bullet> rmlstBullet = new List<obj_bullet>();

        //Texture
        Bitmap bUFO;
        Bitmap bUFO_destroy;
        Bitmap bGameover;
        Bitmap bGameTitle;

        //Text
        String txt_restart_prompt = "Press [R] to start game";

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            gbox_option.Visible = false;
            gameover = true;
            firstgame = true;
        }

        private void LoadTexture(double quality)
        {
            bUFO = new Bitmap(Resources.ufo);
            bUFO = cls_graphics.ResizeBitmap(bUFO, Convert.ToInt32(bUFO.Width * quality), Convert.ToInt32(bUFO.Height * quality));
            bUFO_destroy = new Bitmap(Resources.ufo_destroy);
            bUFO_destroy = cls_graphics.ResizeBitmap(bUFO_destroy, Convert.ToInt32(bUFO_destroy.Width * quality), Convert.ToInt32(bUFO_destroy.Height * quality));
            bGameover = new Bitmap(Resources.gameover);
            bGameover = cls_graphics.ResizeBitmap(bGameover, Convert.ToInt32(bGameover.Width * quality), Convert.ToInt32(bGameover.Height * quality));
            bGameTitle = new Bitmap(Resources.gametitle);
            bGameTitle = cls_graphics.ResizeBitmap(bGameTitle, Convert.ToInt32(bGameTitle.Width * quality), Convert.ToInt32(bGameTitle.Height * quality));
        }

        private void InitializeGame()
        {
            //Clear objects
            lstUFO.Clear();
            rmlstUFO.Clear();
            lstBullet.Clear();
            rmlstBullet.Clear();

            // initialize texture
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            LoadTexture(0.7);
            gameScore = 0;

            //setup fort object
            oFort = new obj_fort();
            oFort.Height = 75;
            oFort.Width = 50;
            oFort.X = (pictureBox1.Width / 2) - (oFort.Width / 2);
            oFort.Y = pictureBox1.Height - oFort.Height - 50;
            oFort.Image = Resources.fort;

            gameover = false;
        }

        private void ObjectMove()
        {
            //Gameover - restart game
            if (gameover && (GetAsyncKeyState((int)0x52) & 0x1) != 0)
            {
                firstgame = false;
                txt_restart_prompt = "Press [R] to restart game";
                InitializeGame();
            }

            //Auto pause
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32() || gameover) return;

            //keyboard input
            if (GetAsyncKeyState((int)0x27) != 0) //right
            {
                oFort.X += 10;
            }
            if (GetAsyncKeyState((int)0x25) != 0) //left
            {
                oFort.X -= 10;
            }
            if (GetAsyncKeyState((int)0x20) != 0 //space
                && Environment.TickCount - oFort.timestamp_LastShoot > 300 | chk_godMod.Checked)
            {
                obj_bullet oBullet = new obj_bullet();
                oBullet.Image = Resources.bullet;
                oBullet.Height = 50;
                oBullet.Width = 30;
                oBullet.X = (oFort.X + (oFort.Width / 2)) - (oBullet.Width / 2);
                oBullet.Y = oFort.Y - (oBullet.Height / 2);
                lstBullet.Add(oBullet);
                oFort.timestamp_LastShoot = Environment.TickCount;
            }

            //Prevent fort out of bounds
            if (oFort.X > pictureBox1.Width - oFort.Width) oFort.X = pictureBox1.Width - oFort.Width;
            if (oFort.X < 0) oFort.X = 0;

            //UFO controller
            foreach (obj_ufo oUfo in lstUFO)
            {
                //destroy
                if (oUfo.destroy)
                {
                    if (Environment.TickCount - oUfo.destroyTime > 1000)
                    {
                        rmlstUFO.Add(oUfo);
                    }
                    continue;
                }

                //move object
                if (Environment.TickCount - oUfo.lastTick > oUfo.OffsetInterval_X)
                {
                    oUfo.FlySwaySpeed = rand.Next(-7, 7);
                    oUfo.lastTick = Environment.TickCount;
                }

                if (cls_algorithm.isCollision(new ObjectInfo(oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height), new ObjectInfo(oFort.X, oFort.Y, oFort.Width, oFort.Height)) == true)
                {
                    oUfo.destroy = true;
                    oUfo.Image = bUFO_destroy;
                    oUfo.destroyTime = Environment.TickCount;
                    gameover = true;
                }

                float rate = (float)((pictureBox1.Height - (oUfo.Y + oUfo.Height)) / pictureBox1.Height); //calculate rate to bottom
                oUfo.DropSpeed = oUfo.lowestSpeed + oUfo.dynamicSpeed * (1.0f - rate); //acceleration
                if (chk_ufosway.Checked)
                {
                    oUfo.Move(oUfo.FlySwaySpeed, oUfo.DropSpeed);
                } else
                {
                    oUfo.Move(0, oUfo.DropSpeed);
                }
                

                //touch bottom
                if (oUfo.Y > this.Height)
                {
                    if (chk_autoDestroy.Checked) rmlstUFO.Add(oUfo);
                    oUfo.Y = -oUfo.Height;
                }
                //touch right
                if (oUfo.X > this.Width)
                {
                    oUfo.X = -oUfo.Width;
                }
                //touch left
                if (oUfo.X < -oUfo.Width)
                {
                    oUfo.X = this.Width;
                }
            }

            //Bullet controller
            foreach (obj_bullet oBullet in lstBullet)
            {

                //move
                oBullet.Y -= oBullet.BulletSpeed;

                //touch top
                if (oBullet.Y < -oBullet.Width)
                {
                    rmlstBullet.Add(oBullet);
                }

                foreach (obj_ufo oUfo in lstUFO)
                {
                    if (oUfo.destroy == false && cls_algorithm.isCollision(new ObjectInfo(oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height), new ObjectInfo(oBullet.X, oBullet.Y, oBullet.Width, oBullet.Height)) == true)
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

        Font fFont = new System.Drawing.Font("Arial", 16);
        int lastTick_fps = Environment.TickCount;
        private void GraphicsTick_Tick(object sender, EventArgs e)
        {
            //Auto pause
            //if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32() || gameover) return;

            // Move Object
            ObjectMove();

            // Draw Object

            using (Graphics gGraphics = Graphics.FromImage(bFrame))
            {
                gGraphics.Clear(Color.White);

                //draw ufo
                foreach (obj_ufo oUfo in lstUFO)
                {
                    gGraphics.DrawImage(oUfo.Image, oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height);
                }

                //draw bullet
                foreach (obj_bullet oBullet in lstBullet)
                {
                    gGraphics.DrawImage(oBullet.Image, oBullet.X, oBullet.Y, oBullet.Width, oBullet.Height);
                }

                //draw fort
                gGraphics.DrawImage(oFort.Image, oFort.X, oFort.Y, oFort.Width, oFort.Height);

                //draw gameover
                if (gameover)
                {
                    if (firstgame)
                        gGraphics.DrawImage(bGameTitle, (pictureBox1.Width / 2) - (bGameTitle.Width / 2), Convert.ToInt32(((pictureBox1.Height / 2) - (bGameTitle.Height / 2)) * 0.5), bGameTitle.Width, bGameTitle.Height);
                    else
                        gGraphics.DrawImage(bGameover, (pictureBox1.Width / 2) - (bGameover.Width / 2), Convert.ToInt32(((pictureBox1.Height / 2) - (bGameover.Height / 2)) * 0.5), bGameover.Width, bGameover.Height);
                    
                    SizeF sFont = gGraphics.MeasureString(txt_restart_prompt, fFont);
                    gGraphics.DrawString(txt_restart_prompt,
                        fFont, Brushes.Black,
                        (pictureBox1.Width / 2) - (sFont.Width / 2),
                        Convert.ToInt32(((pictureBox1.Height / 2) - (bGameover.Height / 2)) * 0.5) + bGameover.Height + 30,
                        StringFormat.GenericTypographic);
                    
                }

            }
            pictureBox1.Image = bFrame;

            // Display performance
            int frameTime = Environment.TickCount - lastTick_fps;
            if (frameTime != 0) this.Text = String.Format("frame time: {0}ms | fps: {1} | UFO Count:{2} | Bullet Count:{3} | Gameover:{4} | Score:{5}",
                   frameTime, 1000 / frameTime, lstUFO.Count, lstBullet.Count, gameover ? "true" : "false", gameScore);
            lastTick_fps = Environment.TickCount;


        }


        //Window Function
        private void Form1_Resize(object sender, EventArgs e)
        {
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            oFort.Y = pictureBox1.Height - oFort.Height - 50;
        }

        private void btn_addUFO_Click(object sender, EventArgs e)
        {
            obj_ufo objUfo = new obj_ufo(bUFO, 100, 60, rand.Next(0, this.Width), -60);
            objUfo.lowestSpeed = 1.0f;
            objUfo.dynamicSpeed = 15.0f;

            objUfo.OffsetInterval_X = rand.Next(350, 2000);
            objUfo.Image = bUFO;
            lstUFO.Add(objUfo);
        }

        private void Btn_rmUFO_Click(object sender, EventArgs e)
        {
            if (lstUFO.Count > 0) lstUFO.RemoveAt(lstUFO.Count - 1);
        }

        private void Label1_MouseEnter(object sender, EventArgs e)
        {
            gbox_option.Visible = true;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            gbox_option.Visible = false;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ufoAddTick.Enabled = chk_ufoAutoAdd.Checked;
        }

        private void UfoAddTick_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32() || gameover) return;
            btn_addUFO_Click(null, null);
            ufoAddTick.Interval = rand.Next(100, 300);
        }
    }
}
