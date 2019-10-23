using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UFO_Game.Properties;
using System.ComponentModel;

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

                // Draw ufo
                foreach (obj_ufo oUfo in lstUFO)
                {
                    if (!oUfo.destroy)
                    {
                        gGraphics.FillRectangle(brush_red, oUfo.X, oUfo.Y + oUfo.Height + 5, (oUfo.Width * (oUfo.Life / (float)oUfo.MaxLife)), 5);
                    }
                    gGraphics.DrawImage(oUfo.Image, oUfo.X, oUfo.Y, oUfo.Width, oUfo.Height);
                }

                // Draw bullet
                foreach (obj_bullet oBullet in lstBullet)
                {
                    gGraphics.DrawImage(oBullet.Image, oBullet.X, oBullet.Y, oBullet.Width, oBullet.Height);
                }

                // Draw fort
                gGraphics.DrawImage(bFort, oFort.X, oFort.Y, oFort.Width, oFort.Height);

                // Draw gameover
                if (gameover)
                {
                    if (firstgame)
                        gGraphics.DrawImage(bGameTitle, oTitleLogo.X, oTitleLogo.Y, oTitleLogo.Width, oTitleLogo.Height);
                    else
                        gGraphics.DrawImage(bGameover, oTitleLogo.X, oTitleLogo.Y, oTitleLogo.Width, oTitleLogo.Height);

                    sFont = gGraphics.MeasureString(text_restart_prompt, fFont);
                    gGraphics.DrawString(text_restart_prompt,
                        fFont, Brushes.Black,
                        (pictureBox1.Width / 2) - (sFont.Width / 2),
                        Convert.ToInt32(((pictureBox1.Height / 2) - (oTitleLogo.Height / 2)) * 0.5) + oTitleLogo.Height + 50,
                        StringFormat.GenericTypographic);
                }

                // Text last height
                float textLastHeight = 0;

                // Draw score
                sFont = gGraphics.MeasureString(text_score, fFont);
                gGraphics.DrawString(text_score,
                    fFont, Brushes.Black,
                    pictureBox1.Width - sFont.Width,
                    0,
                    StringFormat.GenericTypographic);
                textLastHeight += sFont.Height;

                // Draw time
                sFont = gGraphics.MeasureString(text_gametime, fFont);
                gGraphics.DrawString(text_gametime,
                    fFont, Brushes.Black,
                    pictureBox1.Width - sFont.Width,
                    textLastHeight,
                    StringFormat.GenericTypographic);
                textLastHeight += sFont.Height;

                // Draw level
                sFont = gGraphics.MeasureString(text_level, fFont);
                gGraphics.DrawString(text_level,
                    fFont, Brushes.Black,
                    pictureBox1.Width - sFont.Width,
                    textLastHeight,
                    StringFormat.GenericTypographic);
                textLastHeight += sFont.Height;

                // Draw debug info
                sFont = gGraphics.MeasureString(text_debug, fFontMini);
                gGraphics.DrawString(text_debug,
                    fFontMini, Brushes.Red,
                    5,
                    pictureBox1.Height - sFont.Height - 5,
                    StringFormat.GenericTypographic);

            }

            pictureBox1.Image = bFrame;

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
            UFO_Generate();
        }


        private int resize_timestamp = 0;

        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            if (Environment.TickCount - resize_timestamp > 50)
            {
                //Gen_Texture_Floor(textureQuality);
                bFrame = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                oTitleLogo.X = (pictureBox1.Width / 2) - (370 / 2);
                oTitleLogo.Y = Convert.ToInt32(((pictureBox1.Height / 2) - (230 / 2)) * 0.5);
                resize_timestamp = Environment.TickCount;
            }

        }

    }
}
