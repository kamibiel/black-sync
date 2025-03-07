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
            cbMarcarTodas = new CheckBox();
            pbMigracao = new ProgressBar();
            gbLog = new GroupBox();
            txtLog = new TextBox();
            btnMigrar = new Button();
            btnVerificarTabelas = new Button();
            btnVerificarEstrutura = new Button();
            btnGerarScripts = new Button();
            gbFirebirdM.SuspendLayout();
            gbLog.SuspendLayout();
            SuspendLayout();
            // 
            // gbFirebirdM
            // 
            gbFirebirdM.Controls.Add(clbTabelasFirebird);
            gbFirebirdM.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbFirebirdM.Location = new Point(54, 12);
            gbFirebirdM.Name = "gbFirebirdM";
            gbFirebirdM.Size = new Size(222, 223);
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
            clbTabelasFirebird.Size = new Size(210, 199);
            clbTabelasFirebird.TabIndex = 0;
            // 
            // cbMarcarTodas
            // 
            cbMarcarTodas.AutoSize = true;
            cbMarcarTodas.Font = new Font("Arial", 9.75F);
            cbMarcarTodas.Location = new Point(51, 241);
            cbMarcarTodas.Name = "cbMarcarTodas";
            cbMarcarTodas.Size = new Size(187, 20);
            cbMarcarTodas.TabIndex = 2;
            cbMarcarTodas.Text = "Selecionar todas as tabelas";
            cbMarcarTodas.UseVisualStyleBackColor = true;
            cbMarcarTodas.Click += cbMarcarTodas_CheckedChanged;
            // 
            // pbMigracao
            // 
            pbMigracao.Location = new Point(54, 267);
            pbMigracao.Name = "pbMigracao";
            pbMigracao.Size = new Size(372, 10);
            pbMigracao.TabIndex = 3;
            // 
            // gbLog
            // 
            gbLog.Controls.Add(txtLog);
            gbLog.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbLog.Location = new Point(51, 281);
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
            btnMigrar.Location = new Point(299, 57);
            btnMigrar.Name = "btnMigrar";
            btnMigrar.Size = new Size(121, 37);
            btnMigrar.TabIndex = 5;
            btnMigrar.Text = "Migrar tabelas";
            btnMigrar.UseVisualStyleBackColor = true;
            btnMigrar.Click += btnMigrar_Click;
            // 
            // btnVerificarTabelas
            // 
            btnVerificarTabelas.FlatStyle = FlatStyle.Flat;
            btnVerificarTabelas.Font = new Font("Arial", 9.75F);
            btnVerificarTabelas.Location = new Point(299, 100);
            btnVerificarTabelas.Name = "btnVerificarTabelas";
            btnVerificarTabelas.Size = new Size(121, 37);
            btnVerificarTabelas.TabIndex = 6;
            btnVerificarTabelas.Text = "Verificar tabelas";
            btnVerificarTabelas.UseVisualStyleBackColor = true;
            btnVerificarTabelas.Click += btnVerificarTabelas_Click;
            // 
            // btnVerificarEstrutura
            // 
            btnVerificarEstrutura.FlatStyle = FlatStyle.Flat;
            btnVerificarEstrutura.Font = new Font("Arial", 9.75F);
            btnVerificarEstrutura.Location = new Point(299, 143);
            btnVerificarEstrutura.Name = "btnVerificarEstrutura";
            btnVerificarEstrutura.Size = new Size(121, 37);
            btnVerificarEstrutura.TabIndex = 19;
            btnVerificarEstrutura.Text = "Verificar estrutura";
            btnVerificarEstrutura.UseVisualStyleBackColor = true;
            // 
            // btnGerarScripts
            // 
            btnGerarScripts.FlatAppearance.BorderColor = Color.Red;
            btnGerarScripts.FlatStyle = FlatStyle.Flat;
            btnGerarScripts.Font = new Font("Arial", 9.75F);
            btnGerarScripts.Location = new Point(142, 440);
            btnGerarScripts.Name = "btnGerarScripts";
            btnGerarScripts.Size = new Size(182, 41);
            btnGerarScripts.TabIndex = 20;
            btnGerarScripts.Text = "Gerar Scripts";
            btnGerarScripts.UseVisualStyleBackColor = true;
            btnGerarScripts.Click += btnGerarScripts_Click;
            // 
            // FormMigracao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(474, 493);
            Controls.Add(btnGerarScripts);
            Controls.Add(btnVerificarEstrutura);
            Controls.Add(btnVerificarTabelas);
            Controls.Add(btnMigrar);
            Controls.Add(gbLog);
            Controls.Add(pbMigracao);
            Controls.Add(cbMarcarTodas);
            Controls.Add(gbFirebirdM);
            Name = "FormMigracao";
            Text = "FormMigracao";
            gbFirebirdM.ResumeLayout(false);
            gbLog.ResumeLayout(false);
            gbLog.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbFirebirdM;
        private CheckBox cbMarcarTodas;
        private ProgressBar pbMigracao;
        private GroupBox gbLog;
        private Button btnMigrar;
        private TextBox txtLog;
        private CheckedListBox clbTabelasFirebird;
        private Button btnVerificarTabelas;
        private Button btnVerificarEstrutura;
        private Button btnGerarScripts;
    }
}