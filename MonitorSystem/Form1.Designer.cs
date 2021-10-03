
namespace MonitorSystem
{
    partial class FmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.rtbUpdateInfo = new System.Windows.Forms.RichTextBox();
            this.rtbEQWarning = new System.Windows.Forms.RichTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbMonitorMessage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbEQWarningInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlChartInfo = new System.Windows.Forms.Panel();
            this.cbxShowChart2 = new System.Windows.Forms.ComboBox();
            this.cbxShowChart1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlChartInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbUpdateInfo
            // 
            this.rtbUpdateInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbUpdateInfo.Location = new System.Drawing.Point(1, 59);
            this.rtbUpdateInfo.Name = "rtbUpdateInfo";
            this.rtbUpdateInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbUpdateInfo.Size = new System.Drawing.Size(313, 500);
            this.rtbUpdateInfo.TabIndex = 5;
            this.rtbUpdateInfo.Text = "";
            this.rtbUpdateInfo.WordWrap = false;
            // 
            // rtbEQWarning
            // 
            this.rtbEQWarning.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbEQWarning.Location = new System.Drawing.Point(1, 595);
            this.rtbEQWarning.Name = "rtbEQWarning";
            this.rtbEQWarning.Size = new System.Drawing.Size(313, 286);
            this.rtbEQWarning.TabIndex = 12;
            this.rtbEQWarning.Text = "";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.LightGray;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1304, 24);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.lbMonitorMessage);
            this.panel1.Location = new System.Drawing.Point(1, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 25);
            this.panel1.TabIndex = 14;
            // 
            // lbMonitorMessage
            // 
            this.lbMonitorMessage.AutoSize = true;
            this.lbMonitorMessage.Location = new System.Drawing.Point(87, 3);
            this.lbMonitorMessage.Name = "lbMonitorMessage";
            this.lbMonitorMessage.Size = new System.Drawing.Size(131, 19);
            this.lbMonitorMessage.TabIndex = 15;
            this.lbMonitorMessage.Text = "Monitor Message";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.lbEQWarningInfo);
            this.panel2.Location = new System.Drawing.Point(1, 567);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(313, 25);
            this.panel2.TabIndex = 16;
            // 
            // lbEQWarningInfo
            // 
            this.lbEQWarningInfo.AutoSize = true;
            this.lbEQWarningInfo.Location = new System.Drawing.Point(45, 3);
            this.lbEQWarningInfo.Name = "lbEQWarningInfo";
            this.lbEQWarningInfo.Size = new System.Drawing.Size(216, 19);
            this.lbEQWarningInfo.TabIndex = 15;
            this.lbEQWarningInfo.Text = "EQName : Warning Messages";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(320, 567);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(981, 25);
            this.panel3.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Chart Infomations";
            // 
            // pnlChartInfo
            // 
            this.pnlChartInfo.Controls.Add(this.cbxShowChart2);
            this.pnlChartInfo.Controls.Add(this.cbxShowChart1);
            this.pnlChartInfo.Location = new System.Drawing.Point(320, 592);
            this.pnlChartInfo.Name = "pnlChartInfo";
            this.pnlChartInfo.Size = new System.Drawing.Size(984, 289);
            this.pnlChartInfo.TabIndex = 19;
            // 
            // cbxShowChart2
            // 
            this.cbxShowChart2.FormattingEnabled = true;
            this.cbxShowChart2.Location = new System.Drawing.Point(492, 0);
            this.cbxShowChart2.Name = "cbxShowChart2";
            this.cbxShowChart2.Size = new System.Drawing.Size(490, 27);
            this.cbxShowChart2.TabIndex = 21;
            this.cbxShowChart2.SelectedIndexChanged += new System.EventHandler(this.cbxShowChart_SelectedIndexChanged);
            // 
            // cbxShowChart1
            // 
            this.cbxShowChart1.FormattingEnabled = true;
            this.cbxShowChart1.Location = new System.Drawing.Point(0, 0);
            this.cbxShowChart1.Name = "cbxShowChart1";
            this.cbxShowChart1.Size = new System.Drawing.Size(490, 27);
            this.cbxShowChart1.TabIndex = 20;
            this.cbxShowChart1.SelectedIndexChanged += new System.EventHandler(this.cbxShowChart_SelectedIndexChanged);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 884);
            this.Controls.Add(this.pnlChartInfo);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rtbEQWarning);
            this.Controls.Add(this.rtbUpdateInfo);
            this.Controls.Add(this.menuStrip);
            this.Name = "FmMain";
            this.Text = "MonitorSystem";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlChartInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.RichTextBox rtbUpdateInfo;
        private System.Windows.Forms.RichTextBox rtbEQWarning;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbMonitorMessage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbEQWarningInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlChartInfo;
        private System.Windows.Forms.ComboBox cbxShowChart2;
        private System.Windows.Forms.ComboBox cbxShowChart1;
    }
}

