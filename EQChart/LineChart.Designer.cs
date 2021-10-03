
namespace EQChart
{
    partial class LineChart
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
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.chkShowLast1HourInfo = new System.Windows.Forms.CheckBox();
            this.btnRefreshChart = new System.Windows.Forms.Button();
            this.pnlHint = new System.Windows.Forms.Panel();
            this.lbHint = new System.Windows.Forms.Label();
            this.pnlHint.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "\"HH:mm:ss\"";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(87, 218);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(119, 27);
            this.dtpStartTime.TabIndex = 0;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "\"HH:mm:ss\"";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(356, 218);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(118, 27);
            this.dtpEndTime.TabIndex = 1;
            // 
            // chkShowLast1HourInfo
            // 
            this.chkShowLast1HourInfo.AutoSize = true;
            this.chkShowLast1HourInfo.Checked = true;
            this.chkShowLast1HourInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLast1HourInfo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowLast1HourInfo.Location = new System.Drawing.Point(1, 213);
            this.chkShowLast1HourInfo.Name = "chkShowLast1HourInfo";
            this.chkShowLast1HourInfo.Size = new System.Drawing.Size(80, 38);
            this.chkShowLast1HourInfo.TabIndex = 2;
            this.chkShowLast1HourInfo.Text = "Current \r\n1 hour";
            this.chkShowLast1HourInfo.UseVisualStyleBackColor = true;
            this.chkShowLast1HourInfo.CheckedChanged += new System.EventHandler(this.chkShowLast1HourInfo_CheckedChanged);
            // 
            // btnRefreshChart
            // 
            this.btnRefreshChart.Location = new System.Drawing.Point(230, 218);
            this.btnRefreshChart.Name = "btnRefreshChart";
            this.btnRefreshChart.Size = new System.Drawing.Size(102, 27);
            this.btnRefreshChart.TabIndex = 3;
            this.btnRefreshChart.Text = "Refresh";
            this.btnRefreshChart.UseVisualStyleBackColor = true;
            this.btnRefreshChart.Click += new System.EventHandler(this.btnRefreshChart_Click);
            // 
            // pnlHint
            // 
            this.pnlHint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlHint.Controls.Add(this.lbHint);
            this.pnlHint.Location = new System.Drawing.Point(145, 62);
            this.pnlHint.Name = "pnlHint";
            this.pnlHint.Size = new System.Drawing.Size(174, 66);
            this.pnlHint.TabIndex = 4;
            this.pnlHint.Visible = false;
            // 
            // lbHint
            // 
            this.lbHint.AutoSize = true;
            this.lbHint.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbHint.Location = new System.Drawing.Point(6, 5);
            this.lbHint.Name = "lbHint";
            this.lbHint.Size = new System.Drawing.Size(0, 17);
            this.lbHint.TabIndex = 0;
            // 
            // LineChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlHint);
            this.Controls.Add(this.btnRefreshChart);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.chkShowLast1HourInfo);
            this.Name = "LineChart";
            this.Size = new System.Drawing.Size(490, 250);
            this.pnlHint.ResumeLayout(false);
            this.pnlHint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.CheckBox chkShowLast1HourInfo;
        private System.Windows.Forms.Button btnRefreshChart;
        private System.Windows.Forms.Panel pnlHint;
        private System.Windows.Forms.Label lbHint;
    }
}
