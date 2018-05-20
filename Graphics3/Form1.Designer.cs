namespace Graphics3
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midpointCircleAdditionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xaolinWuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thickLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Thick = new System.Windows.Forms.ToolStripTextBox();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(613, 348);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.Draw);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem1,
            this.modeToolStripMenuItem,
            this.cleanToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(652, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modeToolStripMenuItem1
            // 
            this.modeToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dDAToolStripMenuItem,
            this.midpointCircleAdditionsToolStripMenuItem,
            this.xaolinWuToolStripMenuItem,
            this.thickLineToolStripMenuItem});
            this.modeToolStripMenuItem1.Name = "modeToolStripMenuItem1";
            this.modeToolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.modeToolStripMenuItem1.Text = "Mode";
            // 
            // dDAToolStripMenuItem
            // 
            this.dDAToolStripMenuItem.Name = "dDAToolStripMenuItem";
            this.dDAToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.dDAToolStripMenuItem.Text = "DDA";
            this.dDAToolStripMenuItem.Click += new System.EventHandler(this.dDAToolStripMenuItem_Click);
            // 
            // midpointCircleAdditionsToolStripMenuItem
            // 
            this.midpointCircleAdditionsToolStripMenuItem.Name = "midpointCircleAdditionsToolStripMenuItem";
            this.midpointCircleAdditionsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.midpointCircleAdditionsToolStripMenuItem.Text = "Midpoint circle (additions)";
            this.midpointCircleAdditionsToolStripMenuItem.Click += new System.EventHandler(this.midpointCircleAdditionsToolStripMenuItem_Click);
            // 
            // xaolinWuToolStripMenuItem
            // 
            this.xaolinWuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineToolStripMenuItem,
            this.circleToolStripMenuItem});
            this.xaolinWuToolStripMenuItem.Name = "xaolinWuToolStripMenuItem";
            this.xaolinWuToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.xaolinWuToolStripMenuItem.Text = "Xaolin Wu";
            // 
            // thickLineToolStripMenuItem
            // 
            this.thickLineToolStripMenuItem.Name = "thickLineToolStripMenuItem";
            this.thickLineToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.thickLineToolStripMenuItem.Text = "Pixel copying";
            this.thickLineToolStripMenuItem.Click += new System.EventHandler(this.thickLineToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Thick});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.modeToolStripMenuItem.Text = "Thickness";
            // 
            // Thick
            // 
            this.Thick.Name = "Thick";
            this.Thick.Size = new System.Drawing.Size(100, 23);
            this.Thick.Text = "1";
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.lineToolStripMenuItem_Click);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.circleToolStripMenuItem_Click);
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.cleanToolStripMenuItem.Text = "Clean";
            this.cleanToolStripMenuItem.Click += new System.EventHandler(this.cleanToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 387);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Drawing";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem midpointCircleAdditionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xaolinWuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thickLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox Thick;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
    }
}

