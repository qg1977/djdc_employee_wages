namespace djdc_employee_wages
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.系统设置 = new System.Windows.Forms.ToolStripMenuItem();
            this.更改密码 = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统 = new System.Windows.Forms.ToolStripMenuItem();
            this.mdi_menu = new System.Windows.Forms.MenuStrip();
            this.statusStrip1.SuspendLayout();
            this.mdi_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 396);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(911, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 系统设置
            // 
            this.系统设置.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更改密码});
            this.系统设置.Name = "系统设置";
            this.系统设置.Size = new System.Drawing.Size(92, 21);
            this.系统设置.Text = "【系统设置】";
            // 
            // 更改密码
            // 
            this.更改密码.Name = "更改密码";
            this.更改密码.Size = new System.Drawing.Size(124, 22);
            this.更改密码.Text = "更改密码";
            this.更改密码.ToolTipText = "更改密码";
            this.更改密码.Click += new System.EventHandler(this.更改密码_Click);
            // 
            // 退出系统
            // 
            this.退出系统.Name = "退出系统";
            this.退出系统.Size = new System.Drawing.Size(92, 21);
            this.退出系统.Text = "【退出系统】";
            this.退出系统.Click += new System.EventHandler(this.退出系统_Click);
            // 
            // mdi_menu
            // 
            this.mdi_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置,
            this.退出系统});
            this.mdi_menu.Location = new System.Drawing.Point(0, 0);
            this.mdi_menu.Name = "mdi_menu";
            this.mdi_menu.Size = new System.Drawing.Size(911, 25);
            this.mdi_menu.TabIndex = 0;
            this.mdi_menu.Text = "menuStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 418);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mdi_menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mdi_menu;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mdi_menu.ResumeLayout(false);
            this.mdi_menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 系统设置;
        private System.Windows.Forms.ToolStripMenuItem 更改密码;
        private System.Windows.Forms.ToolStripMenuItem 退出系统;
        private System.Windows.Forms.MenuStrip mdi_menu;
    }
}