
namespace EQControl
{
    partial class EQControl
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
            this.lbEQID = new System.Windows.Forms.Label();
            this.lbEQName = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbEQID
            // 
            this.lbEQID.AutoSize = true;
            this.lbEQID.Enabled = false;
            this.lbEQID.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbEQID.Location = new System.Drawing.Point(44, 16);
            this.lbEQID.Name = "lbEQID";
            this.lbEQID.Size = new System.Drawing.Size(75, 29);
            this.lbEQID.TabIndex = 0;
            this.lbEQID.Text = "EQ ID";
            // 
            // lbEQName
            // 
            this.lbEQName.AutoSize = true;
            this.lbEQName.Enabled = false;
            this.lbEQName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbEQName.Location = new System.Drawing.Point(29, 56);
            this.lbEQName.Name = "lbEQName";
            this.lbEQName.Size = new System.Drawing.Size(105, 29);
            this.lbEQName.TabIndex = 1;
            this.lbEQName.Text = "機台名稱";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Maroon;
            this.btnReset.Location = new System.Drawing.Point(138, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(29, 29);
            this.btnReset.TabIndex = 2;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // EQControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lbEQName);
            this.Controls.Add(this.lbEQID);
            this.Name = "EQControl";
            this.Size = new System.Drawing.Size(165, 98);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbEQID;
        private System.Windows.Forms.Label lbEQName;
        private System.Windows.Forms.Button btnReset;
    }
}
