namespace djdc_employee_wages.wagesql
{
    partial class excel_to_sql
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.qg_bt_label1 = new djdc_employee_wages.a_qg_trol.qg_bt_label();
            this.qg_button_quit1 = new djdc_employee_wages.a_qg_trol.qg_button_quit();
            this.qg_button1 = new djdc_employee_wages.a_qg_trol.qg_button();
            this.qg_grid1 = new djdc_employee_wages.a_qg_trol.qg_grid();
            this.qg_button2 = new djdc_employee_wages.a_qg_trol.qg_button();
            ((System.ComponentModel.ISupportInitialize)(this.qg_grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // qg_bt_label1
            // 
            this.qg_bt_label1.AutoSize = true;
            this.qg_bt_label1.BackColor = System.Drawing.Color.Transparent;
            this.qg_bt_label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qg_bt_label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.qg_bt_label1.Location = new System.Drawing.Point(403, 4);
            this.qg_bt_label1.Name = "qg_bt_label1";
            this.qg_bt_label1.Size = new System.Drawing.Size(236, 27);
            this.qg_bt_label1.TabIndex = 0;
            this.qg_bt_label1.Text = "导入员工工资信息";
            // 
            // qg_button_quit1
            // 
            this.qg_button_quit1.Location = new System.Drawing.Point(887, 481);
            this.qg_button_quit1.Name = "qg_button_quit1";
            this.qg_button_quit1.Size = new System.Drawing.Size(75, 23);
            this.qg_button_quit1.TabIndex = 2;
            this.qg_button_quit1.Text = "qg_button_quit1";
            this.qg_button_quit1.UseVisualStyleBackColor = true;
            // 
            // qg_button1
            // 
            this.qg_button1.Location = new System.Drawing.Point(51, 469);
            this.qg_button1.Name = "qg_button1";
            this.qg_button1.Size = new System.Drawing.Size(187, 23);
            this.qg_button1.TabIndex = 3;
            this.qg_button1.Text = "第1步：从excel导出工资信息";
            this.qg_button1.UseVisualStyleBackColor = false;
            this.qg_button1.Click += new System.EventHandler(this.qg_button1_Click);
            // 
            // qg_grid1
            // 
            this.qg_grid1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.qg_grid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.qg_grid1.auto_jytt = true;
            this.qg_grid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(235)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.qg_grid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.qg_grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.qg_grid1.Location = new System.Drawing.Point(13, 48);
            this.qg_grid1.Name = "qg_grid1";
            this.qg_grid1.RowHeadersVisible = false;
            this.qg_grid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.qg_grid1.RowTemplate.Height = 30;
            this.qg_grid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.qg_grid1.Size = new System.Drawing.Size(1030, 415);
            this.qg_grid1.TabIndex = 4;
            this.qg_grid1.xz_jytt = false;
            // 
            // qg_button2
            // 
            this.qg_button2.Location = new System.Drawing.Point(300, 469);
            this.qg_button2.Name = "qg_button2";
            this.qg_button2.Size = new System.Drawing.Size(199, 23);
            this.qg_button2.TabIndex = 5;
            this.qg_button2.Text = "第2步：工资信息导入数据库";
            this.qg_button2.UseVisualStyleBackColor = false;
            this.qg_button2.Click += new System.EventHandler(this.qg_button2_Click);
            // 
            // excel_to_sql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 524);
            this.Controls.Add(this.qg_button2);
            this.Controls.Add(this.qg_grid1);
            this.Controls.Add(this.qg_button1);
            this.Controls.Add(this.qg_button_quit1);
            this.Controls.Add(this.qg_bt_label1);
            this.Name = "excel_to_sql";
            this.Text = "excel_to_sql";
            ((System.ComponentModel.ISupportInitialize)(this.qg_grid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private a_qg_trol.qg_bt_label qg_bt_label1;
        private a_qg_trol.qg_button_quit qg_button_quit1;
        private a_qg_trol.qg_button qg_button1;
        private a_qg_trol.qg_grid qg_grid1;
        private a_qg_trol.qg_button qg_button2;
    }
}