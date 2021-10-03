
namespace EQChart
{
    partial class PieChart
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbTotalNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTotalNum
            // 
            this.lbTotalNum.AutoSize = true;
            this.lbTotalNum.Location = new System.Drawing.Point(32, 228);
            this.lbTotalNum.Name = "lbTotalNum";
            this.lbTotalNum.Size = new System.Drawing.Size(110, 19);
            this.lbTotalNum.TabIndex = 0;
            this.lbTotalNum.Text = "本日生產數量 : ";
            // 
            // PieChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTotalNum);
            this.Name = "PieChart";
            this.Size = new System.Drawing.Size(490, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTotalNum;
    }
}
