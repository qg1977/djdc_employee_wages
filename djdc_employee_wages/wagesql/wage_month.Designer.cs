namespace djdc_employee_wages.wagesql
{
    partial class wage_month
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
            this.qg_label1 = new djdc_employee_wages.a_qg_trol.qg_label();
            this.qg_button1 = new djdc_employee_wages.a_qg_trol.qg_button();
            this.qg_button_quit1 = new djdc_employee_wages.a_qg_trol.qg_button_quit();
            this.qg_label2 = new djdc_employee_wages.a_qg_trol.qg_label();
            this.qg_text_spinner1 = new djdc_employee_wages.a_qg_trol.qg_text_spinner();
            this.qg_check1 = new djdc_employee_wages.a_qg_trol.qg_check();
            this.SuspendLayout();
            // 
            // qg_label1
            // 
            this.qg_label1.AutoSize = true;
            this.qg_label1.BackColor = System.Drawing.Color.Transparent;
            this.qg_label1.Location = new System.Drawing.Point(25, 48);
            this.qg_label1.Name = "qg_label1";
            this.qg_label1.Size = new System.Drawing.Size(65, 12);
            this.qg_label1.TabIndex = 0;
            this.qg_label1.Text = "工资月份：";
            // 
            // qg_button1
            // 
            this.qg_button1.Location = new System.Drawing.Point(84, 217);
            this.qg_button1.Name = "qg_button1";
            this.qg_button1.Size = new System.Drawing.Size(75, 23);
            this.qg_button1.TabIndex = 2;
            this.qg_button1.Text = "确  定";
            this.qg_button1.UseVisualStyleBackColor = false;
            this.qg_button1.Click += new System.EventHandler(this.qg_button1_Click);
            // 
            // qg_button_quit1
            // 
            this.qg_button_quit1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.qg_button_quit1.Location = new System.Drawing.Point(243, 217);
            this.qg_button_quit1.Name = "qg_button_quit1";
            this.qg_button_quit1.Size = new System.Drawing.Size(75, 23);
            this.qg_button_quit1.TabIndex = 3;
            this.qg_button_quit1.Text = "退  出";
            this.qg_button_quit1.UseVisualStyleBackColor = true;
            // 
            // qg_label2
            // 
            this.qg_label2.AutoSize = true;
            this.qg_label2.BackColor = System.Drawing.Color.Transparent;
            this.qg_label2.ForeColor = System.Drawing.Color.Red;
            this.qg_label2.Location = new System.Drawing.Point(25, 19);
            this.qg_label2.Name = "qg_label2";
            this.qg_label2.Size = new System.Drawing.Size(215, 12);
            this.qg_label2.TabIndex = 4;
            this.qg_label2.Text = "工资月份样式：201701(表示2017年1月)";
            // 
            // qg_text_spinner1
            // 
            this.qg_text_spinner1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.qg_text_spinner1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.qg_text_spinner1.Location = new System.Drawing.Point(88, 44);
            this.qg_text_spinner1.Name = "qg_text_spinner1";
            this.qg_text_spinner1.Size = new System.Drawing.Size(127, 21);
            this.qg_text_spinner1.TabIndex = 1;
            this.qg_text_spinner1.Tag = true;
            this.qg_text_spinner1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.qg_text_spinner1.Enter += new System.EventHandler(this.qg_text_spinner1_Enter);
            this.qg_text_spinner1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.qg_text_spinner1_KeyUp);
            this.qg_text_spinner1.Leave += new System.EventHandler(this.qg_text_spinner1_Leave);
            // 
            // qg_check1
            // 
            this.qg_check1.BackColor = System.Drawing.Color.Transparent;
            this.qg_check1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qg_check1.ForeColor = System.Drawing.Color.Red;
            this.qg_check1.Location = new System.Drawing.Point(26, 71);
            this.qg_check1.Name = "qg_check1";
            this.qg_check1.Size = new System.Drawing.Size(307, 140);
            this.qg_check1.TabIndex = 5;
            this.qg_check1.Text = "删除同月记录";
            this.qg_check1.UseVisualStyleBackColor = false;
            // 
            // wage_month
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 258);
            this.Controls.Add(this.qg_check1);
            this.Controls.Add(this.qg_text_spinner1);
            this.Controls.Add(this.qg_label2);
            this.Controls.Add(this.qg_button_quit1);
            this.Controls.Add(this.qg_button1);
            this.Controls.Add(this.qg_label1);
            this.Name = "wage_month";
            this.Text = "wage_month";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private a_qg_trol.qg_label qg_label1;
        private a_qg_trol.qg_button qg_button1;
        private a_qg_trol.qg_button_quit qg_button_quit1;
        private a_qg_trol.qg_label qg_label2;
        private a_qg_trol.qg_text_spinner qg_text_spinner1;
        private a_qg_trol.qg_check qg_check1;
    }
}