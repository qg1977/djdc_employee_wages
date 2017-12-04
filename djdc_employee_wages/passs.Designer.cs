namespace djdc_employee_wages
{
    partial class passs
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
            this.qg_text1 = new djdc_employee_wages.a_qg_trol.qg_text();
            this.qg_button1 = new djdc_employee_wages.a_qg_trol.qg_button();
            this.qg_read_text1 = new djdc_employee_wages.a_qg_trol.qg_read_text();
            this.SuspendLayout();
            // 
            // qg_text1
            // 
            this.qg_text1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.qg_text1.Location = new System.Drawing.Point(48, 30);
            this.qg_text1.Name = "qg_text1";
            this.qg_text1.Size = new System.Drawing.Size(183, 21);
            this.qg_text1.TabIndex = 0;
            // 
            // qg_button1
            // 
            this.qg_button1.Location = new System.Drawing.Point(59, 130);
            this.qg_button1.Name = "qg_button1";
            this.qg_button1.Size = new System.Drawing.Size(75, 23);
            this.qg_button1.TabIndex = 1;
            this.qg_button1.Text = "qg_button1";
            this.qg_button1.UseVisualStyleBackColor = false;
            this.qg_button1.Click += new System.EventHandler(this.qg_button1_Click);
            // 
            // qg_read_text1
            // 
            this.qg_read_text1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(170)))));
            this.qg_read_text1.DataDicEntry = null;
            this.qg_read_text1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.qg_read_text1.Location = new System.Drawing.Point(48, 83);
            this.qg_read_text1.Name = "qg_read_text1";
            this.qg_read_text1.NeedChineseNumerals = false;
            this.qg_read_text1.Precision = 2;
            this.qg_read_text1.ReadOnly = true;
            this.qg_read_text1.Size = new System.Drawing.Size(100, 21);
            this.qg_read_text1.TabIndex = 2;
            this.qg_read_text1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // passs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.qg_read_text1);
            this.Controls.Add(this.qg_button1);
            this.Controls.Add(this.qg_text1);
            this.Name = "passs";
            this.Text = "passs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private a_qg_trol.qg_text qg_text1;
        private a_qg_trol.qg_button qg_button1;
        private a_qg_trol.qg_read_text qg_read_text1;
    }
}