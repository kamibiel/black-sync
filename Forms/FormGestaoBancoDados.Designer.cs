namespace BlackSync.Forms
{
    partial class FormGestaoBancoDados
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
            gpPeriodo = new GroupBox();
            dtAte = new DateTimePicker();
            dtDe = new DateTimePicker();
            lbAte = new Label();
            lbDe = new Label();
            gpBanco = new GroupBox();
            cbBanco = new ComboBox();
            gbDados = new GroupBox();
            cbVendas = new CheckBox();
            cbFinanceiro = new CheckBox();
            cbEstoque = new CheckBox();
            btnReabrirDados = new Button();
            btnFecharDados = new Button();
            gbFilial = new GroupBox();
            nFilial = new NumericUpDown();
            nEmpresa = new NumericUpDown();
            lbEmpresa = new Label();
            lbFilial = new Label();
            btnAtualizarFilial = new Button();
            btnExcluirDados = new Button();
            btnTruncate = new Button();
            btnLimpeza = new Button();
            btnAlterarNumeracaoDocumentos = new Button();
            btnExportarBanco = new Button();
            gbFirebird = new GroupBox();
            clbTabelasFirebird = new CheckedListBox();
            gbMySQL = new GroupBox();
            clbTabelasMySQL = new CheckedListBox();
            lbDivisor1 = new Label();
            lbDivisor2 = new Label();
            cbFirebird = new CheckBox();
            cbMySQL = new CheckBox();
            pbGestao = new ProgressBar();
            gpPeriodo.SuspendLayout();
            gpBanco.SuspendLayout();
            gbDados.SuspendLayout();
            gbFilial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nFilial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nEmpresa).BeginInit();
            gbFirebird.SuspendLayout();
            gbMySQL.SuspendLayout();
            SuspendLayout();
            // 
            // gpPeriodo
            // 
            gpPeriodo.Controls.Add(dtAte);
            gpPeriodo.Controls.Add(dtDe);
            gpPeriodo.Controls.Add(lbAte);
            gpPeriodo.Controls.Add(lbDe);
            gpPeriodo.Font = new Font("Arial", 12F, FontStyle.Bold);
            gpPeriodo.Location = new Point(37, 12);
            gpPeriodo.Name = "gpPeriodo";
            gpPeriodo.Size = new Size(241, 63);
            gpPeriodo.TabIndex = 0;
            gpPeriodo.TabStop = false;
            gpPeriodo.Text = "Período";
            // 
            // dtAte
            // 
            dtAte.CalendarFont = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtAte.Font = new Font("Arial", 8F);
            dtAte.Format = DateTimePickerFormat.Short;
            dtAte.Location = new Point(160, 29);
            dtAte.Name = "dtAte";
            dtAte.Size = new Size(75, 20);
            dtAte.TabIndex = 3;
            dtAte.Value = new DateTime(2025, 2, 26, 0, 0, 0, 0);
            // 
            // dtDe
            // 
            dtDe.CalendarFont = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtDe.Font = new Font("Arial", 8F);
            dtDe.Format = DateTimePickerFormat.Short;
            dtDe.Location = new Point(41, 29);
            dtDe.Name = "dtDe";
            dtDe.Size = new Size(75, 20);
            dtDe.TabIndex = 2;
            dtDe.Value = new DateTime(2025, 2, 26, 0, 0, 0, 0);
            // 
            // lbAte
            // 
            lbAte.AutoSize = true;
            lbAte.Font = new Font("Arial", 10F);
            lbAte.Location = new Point(123, 32);
            lbAte.Name = "lbAte";
            lbAte.Size = new Size(32, 16);
            lbAte.TabIndex = 1;
            lbAte.Text = "Até:";
            // 
            // lbDe
            // 
            lbDe.AutoSize = true;
            lbDe.Font = new Font("Arial", 10F);
            lbDe.Location = new Point(6, 32);
            lbDe.Name = "lbDe";
            lbDe.Size = new Size(29, 16);
            lbDe.TabIndex = 0;
            lbDe.Text = "De:";
            // 
            // gpBanco
            // 
            gpBanco.Controls.Add(cbBanco);
            gpBanco.Font = new Font("Arial", 12F, FontStyle.Bold);
            gpBanco.Location = new Point(284, 12);
            gpBanco.Name = "gpBanco";
            gpBanco.Size = new Size(241, 63);
            gpBanco.TabIndex = 4;
            gpBanco.TabStop = false;
            gpBanco.Text = "Alterar o banco";
            // 
            // cbBanco
            // 
            cbBanco.Font = new Font("Arial", 10F);
            cbBanco.FormattingEnabled = true;
            cbBanco.Items.AddRange(new object[] { "Ambos", "Firebird", "MySQL" });
            cbBanco.Location = new Point(6, 25);
            cbBanco.Name = "cbBanco";
            cbBanco.Size = new Size(229, 24);
            cbBanco.TabIndex = 0;
            // 
            // gbDados
            // 
            gbDados.Controls.Add(cbVendas);
            gbDados.Controls.Add(cbFinanceiro);
            gbDados.Controls.Add(cbEstoque);
            gbDados.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbDados.Location = new Point(37, 81);
            gbDados.Name = "gbDados";
            gbDados.Size = new Size(241, 59);
            gbDados.TabIndex = 5;
            gbDados.TabStop = false;
            gbDados.Text = "Dados";
            // 
            // cbVendas
            // 
            cbVendas.AutoSize = true;
            cbVendas.Font = new Font("Arial", 9F);
            cbVendas.Location = new Point(168, 25);
            cbVendas.Name = "cbVendas";
            cbVendas.Size = new Size(67, 19);
            cbVendas.TabIndex = 2;
            cbVendas.Text = "Vendas";
            cbVendas.UseVisualStyleBackColor = true;
            // 
            // cbFinanceiro
            // 
            cbFinanceiro.AutoSize = true;
            cbFinanceiro.Font = new Font("Arial", 9F);
            cbFinanceiro.Location = new Point(84, 25);
            cbFinanceiro.Name = "cbFinanceiro";
            cbFinanceiro.Size = new Size(84, 19);
            cbFinanceiro.TabIndex = 1;
            cbFinanceiro.Text = "Financeiro";
            cbFinanceiro.UseVisualStyleBackColor = true;
            // 
            // cbEstoque
            // 
            cbEstoque.AutoSize = true;
            cbEstoque.Font = new Font("Arial", 9F);
            cbEstoque.Location = new Point(6, 25);
            cbEstoque.Name = "cbEstoque";
            cbEstoque.Size = new Size(72, 19);
            cbEstoque.TabIndex = 0;
            cbEstoque.Text = "Estoque";
            cbEstoque.UseVisualStyleBackColor = true;
            // 
            // btnReabrirDados
            // 
            btnReabrirDados.FlatStyle = FlatStyle.Flat;
            btnReabrirDados.Font = new Font("Arial", 9.75F);
            btnReabrirDados.Location = new Point(335, 146);
            btnReabrirDados.Name = "btnReabrirDados";
            btnReabrirDados.Size = new Size(101, 27);
            btnReabrirDados.TabIndex = 6;
            btnReabrirDados.Text = "Reabrir Dados";
            btnReabrirDados.UseVisualStyleBackColor = true;
            btnReabrirDados.Click += btnReabrirDados_Click;
            // 
            // btnFecharDados
            // 
            btnFecharDados.FlatStyle = FlatStyle.Flat;
            btnFecharDados.Font = new Font("Arial", 9.75F);
            btnFecharDados.Location = new Point(228, 146);
            btnFecharDados.Name = "btnFecharDados";
            btnFecharDados.Size = new Size(101, 27);
            btnFecharDados.TabIndex = 7;
            btnFecharDados.Text = "Fechar Dados";
            btnFecharDados.UseVisualStyleBackColor = true;
            btnFecharDados.Click += btnFecharDados_Click;
            // 
            // gbFilial
            // 
            gbFilial.Controls.Add(nFilial);
            gbFilial.Controls.Add(nEmpresa);
            gbFilial.Controls.Add(lbEmpresa);
            gbFilial.Controls.Add(lbFilial);
            gbFilial.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbFilial.Location = new Point(284, 81);
            gbFilial.Name = "gbFilial";
            gbFilial.Size = new Size(241, 59);
            gbFilial.TabIndex = 6;
            gbFilial.TabStop = false;
            gbFilial.Text = "Dados da empresa";
            // 
            // nFilial
            // 
            nFilial.Location = new Point(187, 24);
            nFilial.Name = "nFilial";
            nFilial.Size = new Size(37, 26);
            nFilial.TabIndex = 12;
            // 
            // nEmpresa
            // 
            nEmpresa.Location = new Point(82, 24);
            nEmpresa.Name = "nEmpresa";
            nEmpresa.Size = new Size(37, 26);
            nEmpresa.TabIndex = 11;
            // 
            // lbEmpresa
            // 
            lbEmpresa.AutoSize = true;
            lbEmpresa.Font = new Font("Arial", 10F);
            lbEmpresa.Location = new Point(9, 28);
            lbEmpresa.Name = "lbEmpresa";
            lbEmpresa.Size = new Size(67, 16);
            lbEmpresa.TabIndex = 10;
            lbEmpresa.Text = "Empresa:";
            // 
            // lbFilial
            // 
            lbFilial.AutoSize = true;
            lbFilial.Font = new Font("Arial", 10F);
            lbFilial.Location = new Point(141, 28);
            lbFilial.Name = "lbFilial";
            lbFilial.Size = new Size(40, 16);
            lbFilial.TabIndex = 8;
            lbFilial.Text = "Filial:";
            // 
            // btnAtualizarFilial
            // 
            btnAtualizarFilial.FlatStyle = FlatStyle.Flat;
            btnAtualizarFilial.Font = new Font("Arial", 9.75F);
            btnAtualizarFilial.Location = new Point(14, 146);
            btnAtualizarFilial.Name = "btnAtualizarFilial";
            btnAtualizarFilial.Size = new Size(101, 27);
            btnAtualizarFilial.TabIndex = 9;
            btnAtualizarFilial.Text = "Atualizar Filial";
            btnAtualizarFilial.UseVisualStyleBackColor = true;
            btnAtualizarFilial.Click += btnAtualizarFilial_Click;
            // 
            // btnExcluirDados
            // 
            btnExcluirDados.FlatStyle = FlatStyle.Flat;
            btnExcluirDados.Font = new Font("Arial", 9.75F);
            btnExcluirDados.Location = new Point(121, 146);
            btnExcluirDados.Name = "btnExcluirDados";
            btnExcluirDados.Size = new Size(101, 27);
            btnExcluirDados.TabIndex = 11;
            btnExcluirDados.Text = "Excluir Dados";
            btnExcluirDados.UseVisualStyleBackColor = true;
            btnExcluirDados.Click += btnExcluirDados_Click;
            // 
            // btnTruncate
            // 
            btnTruncate.FlatAppearance.BorderColor = Color.Red;
            btnTruncate.FlatStyle = FlatStyle.Flat;
            btnTruncate.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTruncate.ForeColor = Color.Black;
            btnTruncate.Location = new Point(14, 456);
            btnTruncate.Name = "btnTruncate";
            btnTruncate.Size = new Size(264, 27);
            btnTruncate.TabIndex = 12;
            btnTruncate.Text = "Truncate";
            btnTruncate.UseVisualStyleBackColor = true;
            // 
            // btnLimpeza
            // 
            btnLimpeza.FlatStyle = FlatStyle.Flat;
            btnLimpeza.Font = new Font("Arial", 9.75F);
            btnLimpeza.Location = new Point(284, 456);
            btnLimpeza.Name = "btnLimpeza";
            btnLimpeza.Size = new Size(265, 27);
            btnLimpeza.TabIndex = 13;
            btnLimpeza.Text = "Limpeza do banco de dados";
            btnLimpeza.UseVisualStyleBackColor = true;
            // 
            // btnAlterarNumeracaoDocumentos
            // 
            btnAlterarNumeracaoDocumentos.FlatStyle = FlatStyle.Flat;
            btnAlterarNumeracaoDocumentos.Font = new Font("Arial", 9.75F);
            btnAlterarNumeracaoDocumentos.Location = new Point(442, 146);
            btnAlterarNumeracaoDocumentos.Name = "btnAlterarNumeracaoDocumentos";
            btnAlterarNumeracaoDocumentos.Size = new Size(107, 27);
            btnAlterarNumeracaoDocumentos.TabIndex = 10;
            btnAlterarNumeracaoDocumentos.Text = "Replace Dados";
            btnAlterarNumeracaoDocumentos.UseVisualStyleBackColor = true;
            // 
            // btnExportarBanco
            // 
            btnExportarBanco.FlatStyle = FlatStyle.Flat;
            btnExportarBanco.Font = new Font("Arial", 9.75F);
            btnExportarBanco.Location = new Point(160, 517);
            btnExportarBanco.Name = "btnExportarBanco";
            btnExportarBanco.Size = new Size(208, 27);
            btnExportarBanco.TabIndex = 14;
            btnExportarBanco.Text = "Exportar Banco";
            btnExportarBanco.UseVisualStyleBackColor = true;
            btnExportarBanco.Click += btnExportarBanco_Click;
            // 
            // gbFirebird
            // 
            gbFirebird.Controls.Add(clbTabelasFirebird);
            gbFirebird.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbFirebird.Location = new Point(59, 197);
            gbFirebird.Name = "gbFirebird";
            gbFirebird.Size = new Size(184, 223);
            gbFirebird.TabIndex = 15;
            gbFirebird.TabStop = false;
            gbFirebird.Text = "Tabelas Firebird";
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
            // gbMySQL
            // 
            gbMySQL.Controls.Add(clbTabelasMySQL);
            gbMySQL.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbMySQL.Location = new Point(306, 197);
            gbMySQL.Name = "gbMySQL";
            gbMySQL.Size = new Size(184, 223);
            gbMySQL.TabIndex = 16;
            gbMySQL.TabStop = false;
            gbMySQL.Text = "Tabelas MySQL";
            // 
            // clbTabelasMySQL
            // 
            clbTabelasMySQL.Font = new Font("Arial", 8.25F);
            clbTabelasMySQL.FormattingEnabled = true;
            clbTabelasMySQL.Location = new Point(6, 18);
            clbTabelasMySQL.Name = "clbTabelasMySQL";
            clbTabelasMySQL.Size = new Size(172, 199);
            clbTabelasMySQL.TabIndex = 0;
            // 
            // lbDivisor1
            // 
            lbDivisor1.BackColor = Color.Black;
            lbDivisor1.BorderStyle = BorderStyle.Fixed3D;
            lbDivisor1.ForeColor = Color.Black;
            lbDivisor1.Location = new Point(12, 180);
            lbDivisor1.MaximumSize = new Size(0, 2);
            lbDivisor1.Name = "lbDivisor1";
            lbDivisor1.Size = new Size(543, 2);
            lbDivisor1.TabIndex = 17;
            // 
            // lbDivisor2
            // 
            lbDivisor2.BorderStyle = BorderStyle.Fixed3D;
            lbDivisor2.Location = new Point(12, 495);
            lbDivisor2.MaximumSize = new Size(0, 2);
            lbDivisor2.Name = "lbDivisor2";
            lbDivisor2.Size = new Size(543, 2);
            lbDivisor2.TabIndex = 18;
            // 
            // cbFirebird
            // 
            cbFirebird.AutoSize = true;
            cbFirebird.Font = new Font("Arial", 9.75F);
            cbFirebird.Location = new Point(59, 426);
            cbFirebird.Name = "cbFirebird";
            cbFirebird.Size = new Size(187, 20);
            cbFirebird.TabIndex = 19;
            cbFirebird.Text = "Selecionar todas as tabelas";
            cbFirebird.UseVisualStyleBackColor = true;
            // 
            // cbMySQL
            // 
            cbMySQL.AutoSize = true;
            cbMySQL.Font = new Font("Arial", 9.75F);
            cbMySQL.Location = new Point(303, 426);
            cbMySQL.Name = "cbMySQL";
            cbMySQL.Size = new Size(187, 20);
            cbMySQL.TabIndex = 20;
            cbMySQL.Text = "Selecionar todas as tabelas";
            cbMySQL.UseVisualStyleBackColor = true;
            // 
            // pbGestao
            // 
            pbGestao.Location = new Point(12, 500);
            pbGestao.Name = "pbGestao";
            pbGestao.Size = new Size(543, 11);
            pbGestao.TabIndex = 21;
            // 
            // FormGestaoBancoDados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(571, 553);
            Controls.Add(pbGestao);
            Controls.Add(cbMySQL);
            Controls.Add(cbFirebird);
            Controls.Add(lbDivisor2);
            Controls.Add(lbDivisor1);
            Controls.Add(gbMySQL);
            Controls.Add(gbFirebird);
            Controls.Add(btnExportarBanco);
            Controls.Add(btnLimpeza);
            Controls.Add(btnTruncate);
            Controls.Add(btnExcluirDados);
            Controls.Add(btnAlterarNumeracaoDocumentos);
            Controls.Add(btnAtualizarFilial);
            Controls.Add(gbFilial);
            Controls.Add(btnFecharDados);
            Controls.Add(btnReabrirDados);
            Controls.Add(gbDados);
            Controls.Add(gpBanco);
            Controls.Add(gpPeriodo);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "FormGestaoBancoDados";
            Text = "FormGestaoBancoDados";
            gpPeriodo.ResumeLayout(false);
            gpPeriodo.PerformLayout();
            gpBanco.ResumeLayout(false);
            gbDados.ResumeLayout(false);
            gbDados.PerformLayout();
            gbFilial.ResumeLayout(false);
            gbFilial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nFilial).EndInit();
            ((System.ComponentModel.ISupportInitialize)nEmpresa).EndInit();
            gbFirebird.ResumeLayout(false);
            gbMySQL.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gpPeriodo;
        private Label lbDe;
        private DateTimePicker dtDe;
        private Label lbAte;
        private DateTimePicker dtAte;
        private GroupBox gpBanco;
        private ComboBox cbBanco;
        private GroupBox gbDados;
        private CheckBox cbFinanceiro;
        private CheckBox cbEstoque;
        private CheckBox cbVendas;
        private Button btnReabrirDados;
        private Button btnFecharDados;
        private GroupBox gbFilial;
        private NumericUpDown nFilial;
        private NumericUpDown nEmpresa;
        private Label lbEmpresa;
        private Label lbFilial;
        private Button btnAtualizarFilial;
        private Button btnExcluirDados;
        private Button btnTruncate;
        private Button btnLimpeza;
        private Button btnAlterarNumeracaoDocumentos;
        private Button btnExportarBanco;
        private GroupBox gbFirebird;
        private CheckedListBox clbTabelasFirebird;
        private GroupBox gbMySQL;
        private CheckedListBox clbTabelasMySQL;
        private Label lbDivisor1;
        private Label lbDivisor2;
        private CheckBox cbFirebird;
        private CheckBox cbMySQL;
        private ProgressBar pbGestao;
    }
}