namespace djdc_employee_wages
{
    partial class sign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sign));
            this.qg_label1 = new djdc_employee_wages.a_qg_trol.qg_label();
            this.qg_label2 = new djdc_employee_wages.a_qg_trol.qg_label();
            this.qg_label3 = new djdc_employee_wages.a_qg_trol.qg_label();
            this.text_user = new djdc_employee_wages.a_qg_trol.qg_text();
            this.text_pass = new djdc_employee_wages.a_qg_trol.qg_text();
            this.qg_button1 = new djdc_employee_wages.a_qg_trol.qg_button();
            this.qg_button_quit1 = new djdc_employee_wages.a_qg_trol.qg_button_quit();
            this.SuspendLayout();
            // 
            // qg_label1
            // 
            this.qg_label1.BackColor = System.Drawing.Color.Transparent;
            this.qg_label1.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qg_label1.ForeColor = System.Drawing.Color.Yellow;
            this.qg_label1.Location = new System.Drawing.Point(53, 20);
            this.qg_label1.Name = "qg_label1";
            this.qg_label1.Size = new System.Drawing.Size(268, 93);
            this.qg_label1.TabIndex = 0;
            this.qg_label1.Text = "丹江口水力发电厂工资统计系统";
            this.qg_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // qg_label2
            // 
            this.qg_label2.AutoSize = true;
            this.qg_label2.BackColor = System.Drawing.Color.Transparent;
            this.qg_label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qg_label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.qg_label2.Location = new System.Drawing.Point(27, 134);
            this.qg_label2.Name = "qg_label2";
            this.qg_label2.Size = new System.Drawing.Size(76, 16);
            this.qg_label2.TabIndex = 1;
            this.qg_label2.Text = "登录名：";
            // 
            // qg_label3
            // 
            this.qg_label3.AutoSize = true;
            this.qg_label3.BackColor = System.Drawing.Color.Transparent;
            this.qg_label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qg_label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.qg_label3.Location = new System.Drawing.Point(26, 184);
            this.qg_label3.Name = "qg_label3";
            this.qg_label3.Size = new System.Drawing.Size(77, 16);
            this.qg_label3.TabIndex = 2;
            this.qg_label3.Text = "密  码：";
            // 
            // text_user
            // 
            this.text_user.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.text_user.Location = new System.Drawing.Point(110, 131);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(170, 21);
            this.text_user.TabIndex = 3;
            // 
            // text_pass
            // 
            this.text_pass.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.text_pass.Location = new System.Drawing.Point(109, 182);
            this.text_pass.Name = "text_pass";
            this.text_pass.PasswordChar = '*';
            this.text_pass.Size = new System.Drawing.Size(170, 21);
            this.text_pass.TabIndex = 4;
            // 
            // qg_button1
            // 
            this.qg_button1.Location = new System.Drawing.Point(48, 240);
            this.qg_button1.Name = "qg_button1";
            this.qg_button1.Size = new System.Drawing.Size(75, 23);
            this.qg_button1.TabIndex = 5;
            this.qg_button1.Text = "登  录";
            this.qg_button1.UseVisualStyleBackColor = false;
            this.qg_button1.Click += new System.EventHandler(this.qg_button1_Click);
            // 
            // qg_button_quit1
            // 
            this.qg_button_quit1.Location = new System.Drawing.Point(204, 240);
            this.qg_button_quit1.Name = "qg_button_quit1";
            this.qg_button_quit1.Size = new System.Drawing.Size(75, 23);
            this.qg_button_quit1.TabIndex = 6;
            this.qg_button_quit1.Text = "qg_button_quit1";
            this.qg_button_quit1.UseVisualStyleBackColor = true;
            // 
            // sign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.qg_button_quit1);
            this.Controls.Add(this.qg_button1);
            this.Controls.Add(this.text_pass);
            this.Controls.Add(this.text_user);
            this.Controls.Add(this.qg_label3);
            this.Controls.Add(this.qg_label2);
            this.Controls.Add(this.qg_label1);
            this.Name = "sign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工资统计系统登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private a_qg_trol.qg_label qg_label1;
        private a_qg_trol.qg_label qg_label2;
        private a_qg_trol.qg_label qg_label3;
        private a_qg_trol.qg_text text_user;
        private a_qg_trol.qg_text text_pass;
        private a_qg_trol.qg_button qg_button1;
        private a_qg_trol.qg_button_quit qg_button_quit1;
    }
}