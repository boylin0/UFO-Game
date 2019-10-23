using System;
using System.Collections.Generic;
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
        float textureQuality = 0.9f;
        uint gameTime = 0;

        //TickTimestamp
        int gameTime_timestamp = 0;
        int debug_timestamp = 0;

        //level info
        int swayIntervalMin = 1000, swayIntervalMax = 2300;

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
        Brush brush_red = new SolidBrush(Color.Red);

        //Text
        SizeF sFont;
        Font fFont = new System.Drawing.Font("Arial", 14);
        Font fFontMini = new System.Drawing.Font("Arial", 8);
        String text_restart_prompt = "Press [R] to start game\n\n[←] Move Left\n[→] Move Right\n[Space] Shoot\n[Z] SuperBullet";
        String text_score = "";
        String text_gametime = "";
        String text_level = "";
        String text_debug = "";

        private void InitializeGame()
        {
            // initialize texture
            bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            LoadTexture(textureQuality);

            // Clear objects
            lstUFO.Clear();
            rmlstUFO.Clear();
            lstBullet.Clear();
            rmlstBullet.Clear();

            // setup objects
            oTitleLogo = new obj_null((pictureBox1.Width / 2) - (370 / 2), Convert.ToInt32(((pictureBox1.Height / 2) - (230 / 2)) * 0.5), 370, 230);

            // setup fort object
            oFort = new obj_fort();
            oFort.Height = 60;
            oFort.Width = 60;
            oFort.X = (pictureBox1.Width / 2) - (oFort.Width / 2);
            oFort.Y = pictureBox1.Height - oFort.Height - 50;
            oFort.Image = bFort;

            // clear game information
            gameTime = 0;
            gameScore = 0;
            text_score = String.Format("Score: {0}", gameScore);
            text_gametime = String.Format("Time: {0}s", gameTime);
            text_level = "";
            gameover = false;

        }

        private void ObjectMove()
        {
            /*
            MousePos MouseP;
            MouseP.X = this.PointToClient(Cursor.Position).X;
            MouseP.Y = this.PointToClient(Cursor.Position).Y;
            */

            // Debug info
            int frameTime = Environment.TickCount - lastTick_fps;
            if (frameTime != 0 && Environment.TickCount - debug_timestamp > 100)
            {
                text_debug = String.Format(
                "frame time: {0}ms | fps: {1} | UFO Count: {2} | Bullet Count: {3} | Gameover: {4} | Score: {5}",
                   frameTime, 1000 / frameTime, lstUFO.Count, lstBullet.Count, gameover ? "true" : "false", gameScore);
                debug_timestamp = Environment.TickCount;
            }
            lastTick_fps = Environment.TickCount;

            // Gameover - Restart game
            if (gameover && (GetAsyncKeyState((int)0x52) & 0x1) != 0)
            {
                firstgame = false;
                text_restart_prompt = "Press [R] to restart game";
                
                InitializeGame();
            }

            // Auto pause
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32() || gameover) return;


            // calculate game time
            if ((Environment.TickCount - gameTime_timestamp > 1000) && !gameover)
            {
                text_gametime = String.Format("Time: {0}s", gameTime);
                gameTime++;
                gameTime_timestamp = Environment.TickCount;
            }


            /* Keyboard input */

            // Movement
            if (GetAsyncKeyState((int)Keys.Right) != 0) //Right - Move right
            {
                oFort.X += oFort.MaxSpeed;
                gbox_option.Visible = false;
            }
            if (GetAsyncKeyState((int)Keys.Left) != 0) //Left - Move left
            {
                oFort.X -= oFort.MaxSpeed;
                gbox_option.Visible = false;
            }
            if (GetAsyncKeyState((int)Keys.Up) != 0) //Up - Move Up
            {
                oFort.Y -= oFort.MaxSpeed / 2;
                gbox_option.Visible = false;
            }
            if (GetAsyncKeyState((int)Keys.Down) != 0) //Down - Move Down
            {
                oFort.Y += oFort.MaxSpeed;
                gbox_option.Visible = false;
            }

            // Shoot
            if ( GetAsyncKeyState((int)0x20) != 0 //Space - Shoot Key
                && Environment.TickCount - oFort.timestamp_LastShoot > 100 | chk_godMod.Checked)
            {
                int bulletWidth = oFort.FirePower;
                for (int i = 1; i <= bulletWidth; i++) {
                    obj_bullet oBullet = new obj_bullet();
                    oBullet.Image = bBullet;
                    oBullet.X = (oFort.X + (oFort.Width / 2)) - (oBullet.Width / 2) - ((25 * (bulletWidth + 1)) / 2) + (25 * i);
                    oBullet.Y = oFort.Y - (oBullet.Height / 2);
                    lstBullet.Add(oBullet);
                }

                oFort.timestamp_LastShoot = Environment.TickCount;
                gbox_option.Visible = false;
            }
            if (GetAsyncKeyState((int)0x5A) != 0 //Z - Super Shoot Key
                && Environment.TickCount - oFort.timestamp_LastShoot > 300 | chk_godMod.Checked)
            {
                obj_bullet oBullet = new obj_bullet();
                oBullet.Image = bBullet;
                oBullet.Width *= 5;
                oBullet.Height *= 5;
                oBullet.X = (oFort.X + (oFort.Width / 2)) - (oBullet.Width / 2);
                oBullet.isSuperBullet = true;
                oBullet.LowestSpeed = 5.0f;
                oBullet.AccelerationSpeed = 20.0f;
                oBullet.Y = oFort.Y - (oBullet.Height / 2);
                lstBullet.Add(oBullet);
                oFort.timestamp_LastShoot = Environment.TickCount;
                gbox_option.Visible = false;
            }

            /* Keyboard input end */

            // Prevent fort out of bounds
            if (oFort.X > pictureBox1.Width - oFort.Width) oFort.X = pictureBox1.Width - oFort.Width; //right
            if (oFort.X < 0) oFort.X = 0; //left
            if (oFort.Y < 0) oFort.Y = 0; //top
            if (oFort.Y > pictureBox1.Height - oFort.Height) oFort.Y = pictureBox1.Height - oFort.Height; //bottom

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
                if (Environment.TickCount - oUfo.LastSway_timestamp > oUfo.SwayInterval)
                {
                    oUfo.SwaySpeed = rand.Next(-5, 5);
                    oUfo.SwayInterval = rand.Next(300, 2300);
                    oUfo.LastSway_timestamp = Environment.TickCount;
                }
                // HACK - disable sway
                if (chk_ufosway.Checked)
                    oUfo.Move(oUfo.SwaySpeed, oUfo.DropSpeed);
                else
                    oUfo.Move(0, oUfo.DropSpeed);

                // Touch bottom
                if (oUfo.Y > this.Height)
                {
                    if (chk_autoDestroy.Checked)
                        rmlstUFO.Add(oUfo);
                    else
                        oUfo.Y = -oUfo.Height;
                }else
                // Touch right
                if (oUfo.X > this.Width)
                {
                    if(chk_edgeLoop.Checked)
                        oUfo.X = -oUfo.Width;
                    else
                        oUfo.SwaySpeed = -oUfo.SwaySpeed;
                }
                else
                // Touch left
                if (oUfo.X < -oUfo.Width)
                {
                    if (chk_edgeLoop.Checked)
                        oUfo.X = -oUfo.Width;
                    else
                        oUfo.SwaySpeed = -oUfo.SwaySpeed;
                }
            }

            //Bullet controller
            foreach (obj_bullet oBullet in lstBullet)
            {

                //move
                float rate = (float)((oBullet.Y + oBullet.Height + 300) / pictureBox1.Height); //calculate rate to top
                oBullet.Y -= oBullet.LowestSpeed + oBullet.AccelerationSpeed * (rate);

                //touch top
                if (oBullet.Y < -oBullet.Height)
                {
                    rmlstBullet.Add(oBullet);
                }

                foreach (obj_ufo oUfo in lstUFO)
                {
                    if (oUfo.destroy == false && cls_algorithm.isCollision(new obj_null(oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height), new obj_null(oBullet.X, oBullet.Y, oBullet.Width, oBullet.Height)) == true)
                    {
                        oUfo.Life = oUfo.Life - 1;
                        if (oUfo.Life <= 0) {
                            oUfo.destroy = true;
                            oUfo.Image = bUFO_destroy;
                            oUfo.destroyTime = Environment.TickCount;
                            gameScore++;
                            if (oFort.FirePower <= oFort.MaxFirePower)
                            {
                                oFort.FirePower = Convert.ToInt32((gameScore / 10) + 1);
                            }
                        }

                        if (!oBullet.isSuperBullet) rmlstBullet.Add(oBullet);

                        text_score = String.Format("Score: {0}", gameScore);
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

        void UFO_Generate() {
            if (GetForegroundWindow() != (IntPtr)this.Handle.ToInt32() || gameover) return;

            obj_ufo objUfo = new obj_ufo(bUFO, 60, 36, rand.Next(0, this.Width), -60);
            objUfo.LowestSpeed = 1.0f;
            objUfo.AccelerationSpeed = 5.0f;
            objUfo.SwayInterval = rand.Next(swayIntervalMin, swayIntervalMax);
            objUfo.Image = bUFO;
            
            int spawnMin = 1000, spawnMax = 3000;
            if (gameTime <= 10) // ? second
            {
                //level 1
                spawnMin = 500;
                spawnMax = 1200;
                swayIntervalMin = 500;
                swayIntervalMax = 1800;
                objUfo.SetLife(3);
                text_level = String.Format("Level: {0}", 1);
            }
            else if (gameTime <= 20)
            {
                //level 2
                spawnMin = 800;
                spawnMax = 1800;
                swayIntervalMin = 800;
                swayIntervalMax = 1800;
                objUfo.SetLife(5);
                text_level = String.Format("Level: {0}", 2);
            }
            else if (gameTime <= 30)
            {
                //level 3
                spawnMin = 500;
                spawnMax = 1800;
                swayIntervalMin = 500;
                swayIntervalMax = 2000;
                objUfo.SetLife(10);
                text_level = String.Format("Level: {0}", 3);
            }
            else if (gameTime <= 40)
            {
                //level 4
                spawnMin = 200;
                spawnMax = 1500;
                swayIntervalMin = 300;
                swayIntervalMax = 1500;
                objUfo.SetLife(15);
                text_level = String.Format("Level: {0}", 4);
            }
            else if (gameTime <= 50)
            {
                //level 5
                spawnMin = 100;
                spawnMax = 1000;
                swayIntervalMin = 100;
                swayIntervalMax = 800;
                objUfo.SetLife(20);
                text_level = String.Format("Level: {0}", "Final");
            }
            lstUFO.Add(objUfo);
            ufoAddTick.Interval = rand.Next(spawnMin, spawnMax);
        }

    }
}
