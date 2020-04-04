namespace BattleshipsGUI
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miStart = new System.Windows.Forms.ToolStripMenuItem();
            this.miNext = new System.Windows.Forms.ToolStripMenuItem();
            this.nastaveníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowShips = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.miInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.miInt01 = new System.Windows.Forms.ToolStripMenuItem();
            this.miInt025 = new System.Windows.Forms.ToolStripMenuItem();
            this.miInt05 = new System.Windows.Forms.ToolStripMenuItem();
            this.miInt1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miInt2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miInt3 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labP1 = new System.Windows.Forms.Label();
            this.labP2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Shoot);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStart,
            this.miNext,
            this.nastaveníToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miStart
            // 
            this.miStart.Name = "miStart";
            this.miStart.Size = new System.Drawing.Size(112, 20);
            this.miStart.Text = "Začít / restartovat";
            this.miStart.Click += new System.EventHandler(this.StartGame);
            // 
            // miNext
            // 
            this.miNext.Name = "miNext";
            this.miNext.Size = new System.Drawing.Size(70, 20);
            this.miNext.Text = "Další krok";
            this.miNext.Click += new System.EventHandler(this.Shoot);
            // 
            // nastaveníToolStripMenuItem
            // 
            this.nastaveníToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miShowShips,
            this.toolStripSeparator1,
            this.miAuto,
            this.miInterval});
            this.nastaveníToolStripMenuItem.Enabled = false;
            this.nastaveníToolStripMenuItem.Name = "nastaveníToolStripMenuItem";
            this.nastaveníToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.nastaveníToolStripMenuItem.Text = "Nastavení";
            // 
            // miShowShips
            // 
            this.miShowShips.Checked = true;
            this.miShowShips.CheckOnClick = true;
            this.miShowShips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miShowShips.Name = "miShowShips";
            this.miShowShips.Size = new System.Drawing.Size(162, 22);
            this.miShowShips.Text = "Zobrazit lodě";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // miAuto
            // 
            this.miAuto.CheckOnClick = true;
            this.miAuto.Name = "miAuto";
            this.miAuto.Size = new System.Drawing.Size(162, 22);
            this.miAuto.Text = "Automatická hra";
            // 
            // miInterval
            // 
            this.miInterval.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInt01,
            this.miInt025,
            this.miInt05,
            this.miInt1,
            this.miInt2,
            this.miInt3});
            this.miInterval.Name = "miInterval";
            this.miInterval.Size = new System.Drawing.Size(162, 22);
            this.miInterval.Text = "Interval";
            // 
            // miInt01
            // 
            this.miInt01.Name = "miInt01";
            this.miInt01.Size = new System.Drawing.Size(111, 22);
            this.miInt01.Text = "100 ms";
            // 
            // miInt025
            // 
            this.miInt025.Name = "miInt025";
            this.miInt025.Size = new System.Drawing.Size(111, 22);
            this.miInt025.Text = "250 ms";
            // 
            // miInt05
            // 
            this.miInt05.Name = "miInt05";
            this.miInt05.Size = new System.Drawing.Size(111, 22);
            this.miInt05.Text = "500 ms";
            // 
            // miInt1
            // 
            this.miInt1.Name = "miInt1";
            this.miInt1.Size = new System.Drawing.Size(111, 22);
            this.miInt1.Text = "1 s";
            // 
            // miInt2
            // 
            this.miInt2.Name = "miInt2";
            this.miInt2.Size = new System.Drawing.Size(111, 22);
            this.miInt2.Text = "2 s";
            // 
            // miInt3
            // 
            this.miInt3.Name = "miInt3";
            this.miInt3.Size = new System.Drawing.Size(111, 22);
            this.miInt3.Text = "3 s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hráč 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hráč 2";
            // 
            // labP1
            // 
            this.labP1.AutoSize = true;
            this.labP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labP1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labP1.Location = new System.Drawing.Point(156, 24);
            this.labP1.Name = "labP1";
            this.labP1.Size = new System.Drawing.Size(52, 13);
            this.labP1.TabIndex = 4;
            this.labP1.Text = "Na tahu";
            // 
            // labP2
            // 
            this.labP2.AutoSize = true;
            this.labP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labP2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labP2.Location = new System.Drawing.Point(528, 24);
            this.labP2.Name = "labP2";
            this.labP2.Size = new System.Drawing.Size(52, 13);
            this.labP2.TabIndex = 5;
            this.labP2.Text = "Na tahu";
            this.labP2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 361);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(383, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(361, 361);
            this.panel2.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labP2);
            this.Controls.Add(this.labP1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miStart;
        private System.Windows.Forms.ToolStripMenuItem miNext;
        private System.Windows.Forms.ToolStripMenuItem nastaveníToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miShowShips;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miAuto;
        private System.Windows.Forms.ToolStripMenuItem miInterval;
        private System.Windows.Forms.ToolStripMenuItem miInt01;
        private System.Windows.Forms.ToolStripMenuItem miInt025;
        private System.Windows.Forms.ToolStripMenuItem miInt05;
        private System.Windows.Forms.ToolStripMenuItem miInt1;
        private System.Windows.Forms.ToolStripMenuItem miInt2;
        private System.Windows.Forms.ToolStripMenuItem miInt3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labP1;
        private System.Windows.Forms.Label labP2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

