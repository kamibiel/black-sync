namespace BlackSync.Forms
{
    partial class FormMigracao
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
            gbFirebirdM = new GroupBox();
            clbTabelasFirebird = new CheckedListBox();
            gbMySQLM = new GroupBox();
            clbTabelasMySQL = new CheckedListBox();
            cbMarcarTodas = new CheckBox();
            pbMigracao = new ProgressBar();
            gbLog = new GroupBox();
            txtLog = new TextBox();
            btnMigrar = new Button();
            btnGerarScripts = new Button();
            gbFirebirdM.SuspendLayout();
            gbMySQLM.SuspendLayout();
            gbLog.SuspendLayout();
            SuspendLayout();
            // 
            // gbFirebirdM
            // 
            gbFirebirdM.Controls.Add(clbTabelasFirebird);
            gbFirebirdM.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbFirebirdM.Location = new Point(12, 12);
            gbFirebirdM.Name = "gbFirebirdM";
            gbFirebirdM.Size = new Size(184, 223);
            gbFirebirdM.TabIndex = 0;
            gbFirebirdM.TabStop = false;
            gbFirebirdM.Text = "Tabelas Firebird";
            // 
            // clbTabelasFirebird
            // 
            clbTabelasFirebird.Font = new Font("Arial", 8.25F);
            clbTabelasFirebird.FormattingEnabled = true;
            clbTabelasFirebird.Location = new Point(6, 18);
            clbTabelasFirebird.Name = "clbTabelasFirebird";
            clbTabelasFirebird.Size = new Size(172, 199);
            clbTabelasFirebird.TabIndex = 0;
            // 
            // gbMySQLM
            // 
            gbMySQLM.Controls.Add(clbTabelasMySQL);
            gbMySQLM.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbMySQLM.Location = new Point(202, 12);
            gbMySQLM.Name = "gbMySQLM";
            gbMySQLM.Size = new Size(184, 223);
            gbMySQLM.TabIndex = 1;
            gbMySQLM.TabStop = false;
            gbMySQLM.Text = "Tabelas MySQL";
            // 
            // clbTabelasMySQL
            // 
            clbTabelasMySQL.Font = new Font("Arial", 8.25F);
            clbTabelasMySQL.FormattingEnabled = true;
            clbTabelasMySQL.Location = new Point(6, 18);
            clbTabelasMySQL.Name = "clbTabelasMySQL";
            clbTabelasMySQL.Size = new Size(172, 199);
            clbTabelasMySQL.TabIndex = 1;
            // 
            // cbMarcarTodas
            // 
            cbMarcarTodas.AutoSize = true;
            cbMarcarTodas.Font = new Font("Arial", 9.75F);
            cbMarcarTodas.Location = new Point(11, 240);
            cbMarcarTodas.Name = "cbMarcarTodas";
            cbMarcarTodas.Size = new Size(166, 20);
            cbMarcarTodas.TabIndex = 2;
            cbMarcarTodas.Text = "Marcar todas as tabelas";
            cbMarcarTodas.UseVisualStyleBackColor = true;
            cbMarcarTodas.Click += cbMarcarTodas_CheckedChanged;
            // 
            // pbMigracao
            // 
            pbMigracao.Location = new Point(202, 243);
            pbMigracao.Name = "pbMigracao";
            pbMigracao.Size = new Size(184, 10);
            pbMigracao.TabIndex = 3;
            // 
            // gbLog
            // 
            gbLog.Controls.Add(txtLog);
            gbLog.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbLog.Location = new Point(11, 268);
            gbLog.Name = "gbLog";
            gbLog.Size = new Size(375, 153);
            gbLog.TabIndex = 4;
            gbLog.TabStop = false;
            gbLog.Text = "Log de verificação";
            // 
            // txtLog
            // 
            txtLog.Font = new Font("Arial", 8.25F);
            txtLog.Location = new Point(6, 25);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(363, 122);
            txtLog.TabIndex = 0;
            // 
            // btnMigrar
            // 
            btnMigrar.FlatStyle = FlatStyle.Flat;
            btnMigrar.Font = new Font("Arial", 9.75F);
            btnMigrar.Location = new Point(11, 427);
            btnMigrar.Name = "btnMigrar";
            btnMigrar.Size = new Size(127, 37);
            btnMigrar.TabIndex = 5;
            btnMigrar.Text = "Migrar tabelas";
            btnMigrar.UseVisualStyleBackColor = true;
            btnMigrar.Click += btnMigrar_Click;
            // 
            // btnGerarScripts
            // 
            btnGerarScripts.FlatStyle = FlatStyle.Flat;
            btnGerarScripts.Font = new Font("Arial", 9.75F);
            btnGerarScripts.Location = new Point(259, 427);
            btnGerarScripts.Name = "btnGerarScripts";
            btnGerarScripts.Size = new Size(127, 37);
            btnGerarScripts.TabIndex = 6;
            btnGerarScripts.Text = "Gerar scripts";
            btnGerarScripts.UseVisualStyleBackColor = true;
            // 
            // FormMigracao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(393, 481);
            Controls.Add(btnGerarScripts);
            Controls.Add(btnMigrar);
            Controls.Add(gbLog);
            Controls.Add(pbMigracao);
            Controls.Add(cbMarcarTodas);
            Controls.Add(gbMySQLM);
            Controls.Add(gbFirebirdM);
            Name = "FormMigracao";
            Text = "FormMigracao";
            gbFirebirdM.ResumeLayout(false);
            gbMySQLM.ResumeLayout(false);
            gbLog.ResumeLayout(false);
            gbLog.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbFirebirdM;
        private GroupBox gbMySQLM;
        private CheckBox cbMarcarTodas;
        private ProgressBar pbMigracao;
        private GroupBox gbLog;
        private Button btnMigrar;
        private TextBox txtLog;
        private Button btnGerarScripts;
        private CheckedListBox clbTabelasFirebird;
        private CheckedListBox clbTabelasMySQL;
    }
}