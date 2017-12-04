namespace djdc_employee_wages.a_qg_trol
{
    partial class qg_grid
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // qg_grid
            // 
            this.RowTemplate.Height = 23;
            this.DataSourceChanged += new System.EventHandler(this.qg_grid_DataSourceChanged);
            this.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.qg_grid_CellBeginEdit);
            this.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.qg_grid_CellClick);
            this.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.qg_grid_CellFormatting);
            this.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.qg_grid_CellMouseClick);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
