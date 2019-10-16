using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UFO_Game.Properties;

namespace UFO_Game
{
    public partial class frm_main : Form
    {

        public frm_main()
        {
            InitializeComponent();
            InitializeGame();

            // First game initialize
            gbox_option.Visible = false;
            gameover = true;
            firstgame = true;
        }


        // Renderer
        int lastTick_fps = Environment.TickCount;
        private void GraphicsTick_Tick(object sender, EventArgs e)
        {
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
                gGraphics.DrawImage(bFort, oFort.X, oFort.Y, oFort.Width, oFort.Height);

                //draw gameover
                if (gameover)
                {
                    if (firstgame)
                        gGraphics.DrawImage(bGameTitle, oTitleLogo.X, oTitleLogo.Y, oTitleLogo.Width, oTitleLogo.Height);
                    else
                        gGraphics.DrawImage(bGameover, oTitleLogo.X, oTitleLogo.Y, oTitleLogo.Width, oTitleLogo.Height);

                    SizeF sFont = gGraphics.MeasureString(txt_restart_prompt, fFont);
                    gGraphics.DrawString(txt_restart_prompt,
                        fFont, Brushes.Black,
                        (pictureBox1.Width / 2) - (sFont.Width / 2),
                        Convert.ToInt32(((pictureBox1.Height / 2) - (oTitleLogo.Height / 2)) * 0.5) + oTitleLogo.Height + 50,
                        StringFormat.GenericTypographic);
                    
                }
            }

            pictureBox1.Image = bFrame;

            // Display Info
            int frameTime = Environment.TickCount - lastTick_fps;
            if (frameTime != 0) this.Text = String.Format(
                "frame time: {0}ms | fps: {1} | UFO Count:{2} | Bullet Count:{3} | Gameover:{4} | Score:{5}",
                   frameTime, 1000 / frameTime, lstUFO.Count, lstBullet.Count, gameover ? "true" : "false", gameScore);
            lastTick_fps = Environment.TickCount;


        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            oFort.Y = pictureBox1.Height - oFort.Height - 50;
        }

        private void btn_addUFO_Click(object sender, EventArgs e)
        {
            obj_ufo objUfo = new obj_ufo(bUFO, 60, 36, rand.Next(0, this.Width), -60);
            objUfo.LowestSpeed = 1.0f;
            objUfo.AccelerationSpeed = 15.0f;

            objUfo.SwayInterval = rand.Next(350, 2000);
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
