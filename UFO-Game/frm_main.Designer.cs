namespace UFO_Game
{
    partial class frm_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.graphicsTick = new System.Windows.Forms.Timer(this.components);
            this.btn_addUFO = new System.Windows.Forms.Button();
            this.btn_rmUFO = new System.Windows.Forms.Button();
            this.chk_autoDestroy = new System.Windows.Forms.CheckBox();
            this.gbox_option = new System.Windows.Forms.GroupBox();
            this.chk_godMod = new System.Windows.Forms.CheckBox();
            this.chk_ufosway = new System.Windows.Forms.CheckBox();
            this.chk_ufoAutoAdd = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ufoAddTick = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbox_option.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // graphicsTick
            // 
            this.graphicsTick.Enabled = true;
            this.graphicsTick.Interval = 1;
            this.graphicsTick.Tick += new System.EventHandler(this.GraphicsTick_Tick);
            // 
            // btn_addUFO
            // 
            this.btn_addUFO.Location = new System.Drawing.Point(28, 43);
            this.btn_addUFO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_addUFO.Name = "btn_addUFO";
            this.btn_addUFO.Size = new System.Drawing.Size(99, 22);
            this.btn_addUFO.TabIndex = 1;
            this.btn_addUFO.Text = "Add UFO";
            this.btn_addUFO.UseVisualStyleBackColor = true;
            this.btn_addUFO.Click += new System.EventHandler(this.btn_addUFO_Click);
            // 
            // btn_rmUFO
            // 
            this.btn_rmUFO.Location = new System.Drawing.Point(28, 73);
            this.btn_rmUFO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_rmUFO.Name = "btn_rmUFO";
            this.btn_rmUFO.Size = new System.Drawing.Size(99, 22);
            this.btn_rmUFO.TabIndex = 2;
            this.btn_rmUFO.Text = "Remove UFO";
            this.btn_rmUFO.UseVisualStyleBackColor = true;
            this.btn_rmUFO.Click += new System.EventHandler(this.Btn_rmUFO_Click);
            // 
            // chk_autoDestroy
            // 
            this.chk_autoDestroy.AutoSize = true;
            this.chk_autoDestroy.Checked = true;
            this.chk_autoDestroy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_autoDestroy.Location = new System.Drawing.Point(28, 110);
            this.chk_autoDestroy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_autoDestroy.Name = "chk_autoDestroy";
            this.chk_autoDestroy.Size = new System.Drawing.Size(99, 20);
            this.chk_autoDestroy.TabIndex = 3;
            this.chk_autoDestroy.Text = "Auto destroy";
            this.chk_autoDestroy.UseVisualStyleBackColor = true;
            // 
            // gbox_option
            // 
            this.gbox_option.BackColor = System.Drawing.Color.White;
            this.gbox_option.Controls.Add(this.chk_godMod);
            this.gbox_option.Controls.Add(this.chk_ufosway);
            this.gbox_option.Controls.Add(this.chk_ufoAutoAdd);
            this.gbox_option.Controls.Add(this.chk_autoDestroy);
            this.gbox_option.Controls.Add(this.btn_addUFO);
            this.gbox_option.Controls.Add(this.btn_rmUFO);
            this.gbox_option.Location = new System.Drawing.Point(16, 16);
            this.gbox_option.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbox_option.Name = "gbox_option";
            this.gbox_option.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbox_option.Size = new System.Drawing.Size(216, 248);
            this.gbox_option.TabIndex = 4;
            this.gbox_option.TabStop = false;
            // 
            // chk_godMod
            // 
            this.chk_godMod.AutoSize = true;
            this.chk_godMod.Location = new System.Drawing.Point(28, 200);
            this.chk_godMod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_godMod.Name = "chk_godMod";
            this.chk_godMod.Size = new System.Drawing.Size(90, 20);
            this.chk_godMod.TabIndex = 6;
            this.chk_godMod.Text = "God Mode";
            this.chk_godMod.UseVisualStyleBackColor = true;
            // 
            // chk_ufosway
            // 
            this.chk_ufosway.AutoSize = true;
            this.chk_ufosway.Checked = true;
            this.chk_ufosway.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ufosway.Location = new System.Drawing.Point(28, 170);
            this.chk_ufosway.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_ufosway.Name = "chk_ufosway";
            this.chk_ufosway.Size = new System.Drawing.Size(82, 20);
            this.chk_ufosway.TabIndex = 5;
            this.chk_ufosway.Text = "UFO sway";
            this.chk_ufosway.UseVisualStyleBackColor = true;
            // 
            // chk_ufoAutoAdd
            // 
            this.chk_ufoAutoAdd.AutoSize = true;
            this.chk_ufoAutoAdd.Checked = true;
            this.chk_ufoAutoAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ufoAutoAdd.Location = new System.Drawing.Point(28, 141);
            this.chk_ufoAutoAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_ufoAutoAdd.Name = "chk_ufoAutoAdd";
            this.chk_ufoAutoAdd.Size = new System.Drawing.Size(80, 20);
            this.chk_ufoAutoAdd.TabIndex = 4;
            this.chk_ufoAutoAdd.Text = "Auto add";
            this.chk_ufoAutoAdd.UseVisualStyleBackColor = true;
            this.chk_ufoAutoAdd.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "MENU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseEnter += new System.EventHandler(this.Label1_MouseEnter);
            // 
            // ufoAddTick
            // 
            this.ufoAddTick.Enabled = true;
            this.ufoAddTick.Interval = 1200;
            this.ufoAddTick.Tick += new System.EventHandler(this.UfoAddTick_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(678, 701);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 700);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbox_option);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_main";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gbox_option.ResumeLayout(false);
            this.gbox_option.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer graphicsTick;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_addUFO;
        private System.Windows.Forms.Button btn_rmUFO;
        private System.Windows.Forms.CheckBox chk_autoDestroy;
        private System.Windows.Forms.GroupBox gbox_option;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_ufoAutoAdd;
        private System.Windows.Forms.Timer ufoAddTick;
        private System.Windows.Forms.CheckBox chk_ufosway;
        private System.Windows.Forms.CheckBox chk_godMod;
    }
}

