using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UFO_Game.Properties;

namespace UFO_Game
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //initialize global variable
        Bitmap bFrame; //rendering frame
        Random rand = new Random(); //random generator

        //game info
        int gameScore = 0;

        //object list
        List<obj_ufo> ufoList = new List<obj_ufo> {};
        List<obj_ufo> obj_rmList = new List<obj_ufo>();

        //texture
        Bitmap bUFO;
        Bitmap bUFO_destroy;

        public Form1()
        {
            InitializeComponent();

            // initialize texture
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            LoadTexture(0.7);
        }

        private void LoadTexture(double quality) {
            bUFO = new Bitmap(Resources.ufo);
            bUFO = cls_graphics.ResizeBitmap(bUFO, Convert.ToInt32(bUFO.Width * quality), Convert.ToInt32(bUFO.Height * quality));
            bUFO_destroy = new Bitmap(Resources.ufo_destroy);
            bUFO_destroy = cls_graphics.ResizeBitmap(bUFO_destroy, Convert.ToInt32(bUFO_destroy.Width * quality), Convert.ToInt32(bUFO_destroy.Height * quality));
        }

        private void ObjectMove()
        {
            //Auto pause
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32()) return;

            try
            {
                foreach (obj_ufo oUfo in ufoList)
                {
                    //destroy object
                    if (oUfo.destroy)
                    {
                        if (Environment.TickCount - oUfo.destroyTime > 1000)
                        {
                            obj_rmList.Add(oUfo);
                        }
                        continue;
                    }

                    //move object
                    if (Environment.TickCount - oUfo.lastTick > oUfo.OffsetInterval_X)
                    {
                        oUfo.FlyOffsetX = rand.Next(-7, 7);
                        oUfo.lastTick = Environment.TickCount;
                    }
                    oUfo.Move(oUfo.FlyOffsetX * ((1 - ((Environment.TickCount - oUfo.lastTick) / oUfo.OffsetInterval_X))), oUfo.DropSpeed);

                    //touch bottom
                    if (oUfo.Y > this.Height)
                    {
                        if(chk_autoDestroy.Checked) obj_rmList.Add(oUfo);
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
                        oUfo.X = this.Width ;
                    }
                }

                foreach (obj_ufo oUfo in obj_rmList)
                {
                    ufoList.Remove(oUfo);
                }
            } catch {
                Debug.Print("Object Lose");
            }


            
        }

        int lastTick_fps = Environment.TickCount;


        private void GraphicsTick_Tick(object sender, EventArgs e)
        {
            //Auto pause
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32()) return;

            // Move Object
            ObjectMove();

            // Draw Object

            using (Graphics gGraphics = Graphics.FromImage(bFrame))
            {
                gGraphics.Clear(Color.White);
                foreach (obj_ufo oUfo in ufoList)
                {
                    gGraphics.DrawImage(oUfo.Image, oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height);
                }
                
            }
            pictureBox1.Image = bFrame;

            // Display performance
            int frameTime = Environment.TickCount - lastTick_fps;
            if(frameTime!=0) this.Text = String.Format("frame time: {0}ms | fps: {1} | UFO Count:{2} | Score:{3}", frameTime, 1000 / frameTime, ufoList.Count, gameScore );
            lastTick_fps = Environment.TickCount;
            
        }


        //Window Function
        private void Form1_Resize(object sender, EventArgs e)
        {
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void btn_addUFO_Click(object sender, EventArgs e)
        {
            obj_ufo objUfo = new obj_ufo(bUFO, 100, 60, rand.Next(0, this.Width));
            objUfo.DropSpeed = rand.Next(1,6);
            objUfo.OffsetInterval_X = rand.Next(350,2000);
            objUfo.Image = bUFO;
            ufoList.Add(objUfo);
        }

        private void Btn_rmUFO_Click(object sender, EventArgs e)
        {
            if(ufoList.Count > 0) ufoList.RemoveAt(ufoList.Count-1);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

            //destory detect
            MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
            int mousePosX = mouseEventArgs.X, mousePosY = mouseEventArgs.Y;
            bool isHit = false;

            foreach (obj_ufo oUfo in ufoList) {

                if (mousePosX > oUfo.X && mousePosX < oUfo.X + oUfo.Width 
                    && mousePosY > oUfo.Y && mousePosY < oUfo.Y + oUfo.Height)
                {
                    if (!oUfo.destroy) {
                        gameScore++;
                        oUfo.destroy = true;
                        oUfo.Image = bUFO_destroy;
                        oUfo.destroyTime = Environment.TickCount;
                        isHit = true;
                    }
                }
                
            }

            if(!isHit && gameScore > 0) gameScore--;
        }

        private void Label1_MouseEnter(object sender, EventArgs e)
        {
            gbox_option.Visible = true;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            gbox_option.Visible = false;
        }
    }
}
