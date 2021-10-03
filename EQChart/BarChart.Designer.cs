
namespace EQChart
{
    partial class BarChart
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxMonitorItem = new System.Windows.Forms.ComboBox();
            this.lbSelectedInfo = new System.Windows.Forms.Label();
            this.tbBoundary = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxMonitorItem
            // 
            this.cbxMonitorItem.FormattingEnabled = true;
            this.cbxMonitorItem.Location = new System.Drawing.Point(0, 0);
            this.cbxMonitorItem.Name = "cbxMonitorItem";
            this.cbxMonitorItem.Size = new System.Drawing.Size(186, 27);
            this.cbxMonitorItem.TabIndex = 0;
            this.cbxMonitorItem.SelectedIndexChanged += new System.EventHandler(this.cbxMonitorItem_SelectedIndexChanged);
            // 
            // lbSelectedInfo
            // 
            this.lbSelectedInfo.AutoSize = true;
            this.lbSelectedInfo.Location = new System.Drawing.Point(87, 31);
            this.lbSelectedInfo.Name = "lbSelectedInfo";
            this.lbSelectedInfo.Size = new System.Drawing.Size(97, 19);
            this.lbSelectedInfo.TabIndex = 1;
            this.lbSelectedInfo.Text = "selected Info";
            // 
            // tbBoundary
            // 
            this.tbBoundary.Location = new System.Drawing.Point(416, 1);
            this.tbBoundary.Name = "tbBoundary";
            this.tbBoundary.Size = new System.Drawing.Size(69, 27);
            this.tbBoundary.TabIndex = 2;
            this.tbBoundary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbBoundary_KeyDown);
            this.tbBoundary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBoundary_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Boundary:";
            // 
            // BarChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbBoundary);
            this.Controls.Add(this.lbSelectedInfo);
            this.Controls.Add(this.cbxMonitorItem);
            this.Name = "BarChart";
            this.Size = new System.Drawing.Size(490, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxMonitorItem;
        private System.Windows.Forms.Label lbSelectedInfo;
        private System.Windows.Forms.TextBox tbBoundary;
        private System.Windows.Forms.Label label1;
    }
}
