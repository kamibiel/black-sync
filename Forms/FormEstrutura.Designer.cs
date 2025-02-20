namespace BlackSync.Forms
{
    partial class FormEstrutura
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
            gbFirebird = new GroupBox();
            clbTabelasFirebird = new CheckedListBox();
            gbMySQL = new GroupBox();
            clbTabelasMySQL = new CheckedListBox();
            btnVerificarEstrutura = new Button();
            btnGerarScripts = new Button();
            cbSelecionarTabelas = new CheckBox();
            pbCarregamentoScripts = new ProgressBar();
            txtSaida = new TextBox();
            gbFirebird.SuspendLayout();
            gbMySQL.SuspendLayout();
            SuspendLayout();
            // 
            // gbFirebird
            // 
            gbFirebird.Controls.Add(clbTabelasFirebird);
            gbFirebird.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbFirebird.Location = new Point(12, 12);
            gbFirebird.Name = "gbFirebird";
            gbFirebird.Size = new Size(184, 223);
            gbFirebird.TabIndex = 0;
            gbFirebird.TabStop = false;
            gbFirebird.Text = "Tabelas Firebird";
            // 
            // clbTabelasFirebird
            // 
            clbTabelasFirebird.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clbTabelasFirebird.FormattingEnabled = true;
            clbTabelasFirebird.Location = new Point(3, 25);
            clbTabelasFirebird.Name = "clbTabelasFirebird";
            clbTabelasFirebird.Size = new Size(178, 184);
            clbTabelasFirebird.TabIndex = 1;
            // 
            // gbMySQL
            // 
            gbMySQL.Controls.Add(clbTabelasMySQL);
            gbMySQL.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbMySQL.Location = new Point(202, 12);
            gbMySQL.Name = "gbMySQL";
            gbMySQL.Size = new Size(184, 223);
            gbMySQL.TabIndex = 1;
            gbMySQL.TabStop = false;
            gbMySQL.Text = "Tabelas MySQL";
            // 
            // clbTabelasMySQL
            // 
            clbTabelasMySQL.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clbTabelasMySQL.FormattingEnabled = true;
            clbTabelasMySQL.Location = new Point(3, 25);
            clbTabelasMySQL.Name = "clbTabelasMySQL";
            clbTabelasMySQL.Size = new Size(178, 184);
            clbTabelasMySQL.TabIndex = 2;
            // 
            // btnVerificarEstrutura
            // 
            btnVerificarEstrutura.BackColor = Color.DarkViolet;
            btnVerificarEstrutura.FlatAppearance.BorderSize = 0;
            btnVerificarEstrutura.FlatStyle = FlatStyle.Flat;
            btnVerificarEstrutura.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerificarEstrutura.Location = new Point(202, 241);
            btnVerificarEstrutura.Name = "btnVerificarEstrutura";
            btnVerificarEstrutura.Size = new Size(164, 34);
            btnVerificarEstrutura.TabIndex = 4;
            btnVerificarEstrutura.Text = "Verificar Estrutura";
            btnVerificarEstrutura.UseVisualStyleBackColor = false;
            btnVerificarEstrutura.Click += btnVerificarEstrutura_Click;
            // 
            // btnGerarScripts
            // 
            btnGerarScripts.BackColor = Color.Transparent;
            btnGerarScripts.FlatStyle = FlatStyle.Flat;
            btnGerarScripts.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGerarScripts.Location = new Point(297, 346);
            btnGerarScripts.Name = "btnGerarScripts";
            btnGerarScripts.Size = new Size(86, 50);
            btnGerarScripts.TabIndex = 5;
            btnGerarScripts.Text = "Gerar Scripts";
            btnGerarScripts.UseVisualStyleBackColor = false;
            btnGerarScripts.Click += btnGerarScript_Click;
            // 
            // cbSelecionarTabelas
            // 
            cbSelecionarTabelas.AutoSize = true;
            cbSelecionarTabelas.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbSelecionarTabelas.Location = new Point(12, 250);
            cbSelecionarTabelas.Name = "cbSelecionarTabelas";
            cbSelecionarTabelas.Size = new Size(187, 20);
            cbSelecionarTabelas.TabIndex = 3;
            cbSelecionarTabelas.Text = "Selecionar todas as tabelas";
            cbSelecionarTabelas.UseVisualStyleBackColor = true;
            cbSelecionarTabelas.Click += cbSelecionarTabelas_CheckedChanged;
            // 
            // pbCarregamentoScripts
            // 
            pbCarregamentoScripts.Location = new Point(9, 461);
            pbCarregamentoScripts.Name = "pbCarregamentoScripts";
            pbCarregamentoScripts.Size = new Size(374, 11);
            pbCarregamentoScripts.TabIndex = 6;
            pbCarregamentoScripts.Visible = false;
            // 
            // txtSaida
            // 
            txtSaida.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSaida.Location = new Point(12, 281);
            txtSaida.Multiline = true;
            txtSaida.Name = "txtSaida";
            txtSaida.ReadOnly = true;
            txtSaida.ScrollBars = ScrollBars.Vertical;
            txtSaida.Size = new Size(282, 174);
            txtSaida.TabIndex = 7;
            // 
            // FormEstrutura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(393, 481);
            Controls.Add(txtSaida);
            Controls.Add(pbCarregamentoScripts);
            Controls.Add(cbSelecionarTabelas);
            Controls.Add(btnGerarScripts);
            Controls.Add(btnVerificarEstrutura);
            Controls.Add(gbMySQL);
            Controls.Add(gbFirebird);
            Name = "FormEstrutura";
            Text = "FormEstrutura";
            Load += FormEstrutura_Load;
            gbFirebird.ResumeLayout(false);
            gbMySQL.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbFirebird;
        private CheckedListBox clbTabelasFirebird;
        private GroupBox gbMySQL;
        private CheckedListBox clbTabelasMySQL;
        private Button btnVerificarEstrutura;
        private Button btnGerarScripts;
        private CheckBox cbSelecionarTabelas;
        private ProgressBar pbCarregamentoScripts;
        private TextBox txtSaida;
    }
}