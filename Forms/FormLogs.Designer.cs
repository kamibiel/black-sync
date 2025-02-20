namespace BlackSync.Forms
{
    partial class FormLogs
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
            gbLog = new GroupBox();
            txtLogs = new TextBox();
            gbLog.SuspendLayout();
            SuspendLayout();
            // 
            // gbLog
            // 
            gbLog.Controls.Add(txtLogs);
            gbLog.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbLog.Location = new Point(12, 12);
            gbLog.Name = "gbLog";
            gbLog.Size = new Size(369, 457);
            gbLog.TabIndex = 0;
            gbLog.TabStop = false;
            gbLog.Text = "Logs";
            // 
            // txtLogs
            // 
            txtLogs.Font = new Font("Arial", 8.25F);
            txtLogs.Location = new Point(6, 25);
            txtLogs.Multiline = true;
            txtLogs.Name = "txtLogs";
            txtLogs.ScrollBars = ScrollBars.Vertical;
            txtLogs.Size = new Size(357, 426);
            txtLogs.TabIndex = 0;
            // 
            // FormLogs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(393, 481);
            Controls.Add(gbLog);
            Name = "FormLogs";
            Text = "FormLogs";
            gbLog.ResumeLayout(false);
            gbLog.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbLog;
        private TextBox txtLogs;
    }
}