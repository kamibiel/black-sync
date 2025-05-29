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
            nEmpresaN = new NumericUpDown();
            nFilial = new NumericUpDown();
            nEmpresa = new NumericUpDown();
            label1 = new Label();
            lbEmpresa = new Label();
            lbFilial = new Label();
            btnAtualizarFilial = new Button();
            btnExcluirDados = new Button();
            btnAlterarNumeracaoDocumentos = new Button();
            btnExportarBanco = new Button();
            lbDivisor1 = new Label();
            pbGestao = new ProgressBar();
            lbDivisor2 = new Label();
            gbMigracaoZpl = new GroupBox();
            btnSelecionar = new Button();
            txtCaminhoArquivoAccess = new TextBox();
            btnConverterZpl = new Button();
            btnSelecionarArquivoAccess = new OpenFileDialog();
            gpPeriodo.SuspendLayout();
            gpBanco.SuspendLayout();
            gbDados.SuspendLayout();
            gbFilial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nEmpresaN).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nFilial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nEmpresa).BeginInit();
            gbMigracaoZpl.SuspendLayout();
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
            dtAte.TabIndex = 1;
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
            dtDe.TabIndex = 0;
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
            cbBanco.TabIndex = 2;
            // 
            // gbDados
            // 
            gbDados.Controls.Add(cbVendas);
            gbDados.Controls.Add(cbFinanceiro);
            gbDados.Controls.Add(cbEstoque);
            gbDados.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbDados.Location = new Point(37, 81);
            gbDados.Name = "gbDados";
            gbDados.Size = new Size(241, 72);
            gbDados.TabIndex = 5;
            gbDados.TabStop = false;
            gbDados.Text = "Categoria";
            // 
            // cbVendas
            // 
            cbVendas.AutoSize = true;
            cbVendas.Font = new Font("Arial", 9F);
            cbVendas.Location = new Point(168, 34);
            cbVendas.Name = "cbVendas";
            cbVendas.Size = new Size(67, 19);
            cbVendas.TabIndex = 5;
            cbVendas.Text = "Vendas";
            cbVendas.UseVisualStyleBackColor = true;
            // 
            // cbFinanceiro
            // 
            cbFinanceiro.AutoSize = true;
            cbFinanceiro.Font = new Font("Arial", 9F);
            cbFinanceiro.Location = new Point(84, 34);
            cbFinanceiro.Name = "cbFinanceiro";
            cbFinanceiro.Size = new Size(84, 19);
            cbFinanceiro.TabIndex = 4;
            cbFinanceiro.Text = "Financeiro";
            cbFinanceiro.UseVisualStyleBackColor = true;
            // 
            // cbEstoque
            // 
            cbEstoque.AutoSize = true;
            cbEstoque.Font = new Font("Arial", 9F);
            cbEstoque.Location = new Point(6, 34);
            cbEstoque.Name = "cbEstoque";
            cbEstoque.Size = new Size(72, 19);
            cbEstoque.TabIndex = 3;
            cbEstoque.Text = "Estoque";
            cbEstoque.UseVisualStyleBackColor = true;
            // 
            // btnReabrirDados
            // 
            btnReabrirDados.FlatStyle = FlatStyle.Flat;
            btnReabrirDados.Font = new Font("Arial", 9.75F);
            btnReabrirDados.Location = new Point(345, 159);
            btnReabrirDados.Name = "btnReabrirDados";
            btnReabrirDados.Size = new Size(101, 27);
            btnReabrirDados.TabIndex = 12;
            btnReabrirDados.Text = "Reabrir Dados";
            btnReabrirDados.UseVisualStyleBackColor = true;
            btnReabrirDados.Click += btnReabrirDados_Click;
            // 
            // btnFecharDados
            // 
            btnFecharDados.FlatStyle = FlatStyle.Flat;
            btnFecharDados.Font = new Font("Arial", 9.75F);
            btnFecharDados.Location = new Point(238, 159);
            btnFecharDados.Name = "btnFecharDados";
            btnFecharDados.Size = new Size(101, 27);
            btnFecharDados.TabIndex = 11;
            btnFecharDados.Text = "Fechar Dados";
            btnFecharDados.UseVisualStyleBackColor = true;
            btnFecharDados.Click += btnFecharDados_Click;
            // 
            // gbFilial
            // 
            gbFilial.Controls.Add(nEmpresaN);
            gbFilial.Controls.Add(nFilial);
            gbFilial.Controls.Add(nEmpresa);
            gbFilial.Controls.Add(label1);
            gbFilial.Controls.Add(lbEmpresa);
            gbFilial.Controls.Add(lbFilial);
            gbFilial.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbFilial.Location = new Point(284, 81);
            gbFilial.Name = "gbFilial";
            gbFilial.Size = new Size(241, 72);
            gbFilial.TabIndex = 6;
            gbFilial.TabStop = false;
            gbFilial.Text = "Dados da empresa";
            // 
            // nEmpresaN
            // 
            nEmpresaN.Font = new Font("Arial", 9F, FontStyle.Bold);
            nEmpresaN.Location = new Point(102, 40);
            nEmpresaN.Name = "nEmpresaN";
            nEmpresaN.Size = new Size(37, 21);
            nEmpresaN.TabIndex = 7;
            // 
            // nFilial
            // 
            nFilial.Font = new Font("Arial", 9F, FontStyle.Bold);
            nFilial.Location = new Point(184, 40);
            nFilial.Name = "nFilial";
            nFilial.Size = new Size(37, 21);
            nFilial.TabIndex = 8;
            // 
            // nEmpresa
            // 
            nEmpresa.Font = new Font("Arial", 9F, FontStyle.Bold);
            nEmpresa.Location = new Point(18, 40);
            nEmpresa.Name = "nEmpresa";
            nEmpresa.Size = new Size(37, 21);
            nEmpresa.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 9F);
            label1.Location = new Point(91, 22);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 13;
            label1.Text = "Empresa N:";
            // 
            // lbEmpresa
            // 
            lbEmpresa.AutoSize = true;
            lbEmpresa.Font = new Font("Arial", 9F);
            lbEmpresa.Location = new Point(5, 22);
            lbEmpresa.Name = "lbEmpresa";
            lbEmpresa.Size = new Size(70, 15);
            lbEmpresa.TabIndex = 10;
            lbEmpresa.Text = "Empresa A:";
            // 
            // lbFilial
            // 
            lbFilial.AutoSize = true;
            lbFilial.Font = new Font("Arial", 9F);
            lbFilial.Location = new Point(184, 22);
            lbFilial.Name = "lbFilial";
            lbFilial.Size = new Size(36, 15);
            lbFilial.TabIndex = 8;
            lbFilial.Text = "Filial:";
            // 
            // btnAtualizarFilial
            // 
            btnAtualizarFilial.FlatStyle = FlatStyle.Flat;
            btnAtualizarFilial.Font = new Font("Arial", 9.75F);
            btnAtualizarFilial.Location = new Point(24, 159);
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
            btnExcluirDados.Location = new Point(131, 159);
            btnExcluirDados.Name = "btnExcluirDados";
            btnExcluirDados.Size = new Size(101, 27);
            btnExcluirDados.TabIndex = 10;
            btnExcluirDados.Text = "Excluir Dados";
            btnExcluirDados.UseVisualStyleBackColor = true;
            btnExcluirDados.Click += btnExcluirDados_Click;
            // 
            // btnAlterarNumeracaoDocumentos
            // 
            btnAlterarNumeracaoDocumentos.FlatStyle = FlatStyle.Flat;
            btnAlterarNumeracaoDocumentos.Font = new Font("Arial", 9.75F);
            btnAlterarNumeracaoDocumentos.Location = new Point(452, 159);
            btnAlterarNumeracaoDocumentos.Name = "btnAlterarNumeracaoDocumentos";
            btnAlterarNumeracaoDocumentos.Size = new Size(107, 27);
            btnAlterarNumeracaoDocumentos.TabIndex = 13;
            btnAlterarNumeracaoDocumentos.Text = "Replace Dados";
            btnAlterarNumeracaoDocumentos.UseVisualStyleBackColor = true;
            btnAlterarNumeracaoDocumentos.Click += btnAlterarNumeracaoDocumento_Click;
            // 
            // btnExportarBanco
            // 
            btnExportarBanco.FlatStyle = FlatStyle.Flat;
            btnExportarBanco.Font = new Font("Arial", 9.75F);
            btnExportarBanco.Location = new Point(174, 335);
            btnExportarBanco.Name = "btnExportarBanco";
            btnExportarBanco.Size = new Size(208, 27);
            btnExportarBanco.TabIndex = 20;
            btnExportarBanco.Text = "Exportar Banco";
            btnExportarBanco.UseVisualStyleBackColor = true;
            btnExportarBanco.Visible = false;
            btnExportarBanco.Click += btnExportarBanco_Click;
            // 
            // lbDivisor1
            // 
            lbDivisor1.BackColor = Color.Black;
            lbDivisor1.BorderStyle = BorderStyle.Fixed3D;
            lbDivisor1.ForeColor = Color.Black;
            lbDivisor1.Location = new Point(22, 193);
            lbDivisor1.MaximumSize = new Size(0, 2);
            lbDivisor1.Name = "lbDivisor1";
            lbDivisor1.Size = new Size(543, 2);
            lbDivisor1.TabIndex = 17;
            // 
            // pbGestao
            // 
            pbGestao.Location = new Point(22, 318);
            pbGestao.Name = "pbGestao";
            pbGestao.Size = new Size(543, 11);
            pbGestao.TabIndex = 21;
            // 
            // lbDivisor2
            // 
            lbDivisor2.BackColor = Color.Black;
            lbDivisor2.BorderStyle = BorderStyle.Fixed3D;
            lbDivisor2.ForeColor = Color.Black;
            lbDivisor2.Location = new Point(22, 313);
            lbDivisor2.MaximumSize = new Size(0, 2);
            lbDivisor2.Name = "lbDivisor2";
            lbDivisor2.Size = new Size(543, 2);
            lbDivisor2.TabIndex = 22;
            // 
            // gbMigracaoZpl
            // 
            gbMigracaoZpl.Controls.Add(btnSelecionar);
            gbMigracaoZpl.Controls.Add(txtCaminhoArquivoAccess);
            gbMigracaoZpl.Controls.Add(btnConverterZpl);
            gbMigracaoZpl.Font = new Font("Arial", 12F, FontStyle.Bold);
            gbMigracaoZpl.Location = new Point(37, 206);
            gbMigracaoZpl.Name = "gbMigracaoZpl";
            gbMigracaoZpl.Size = new Size(241, 91);
            gbMigracaoZpl.TabIndex = 23;
            gbMigracaoZpl.TabStop = false;
            gbMigracaoZpl.Text = "Migração ZPL";
            // 
            // btnSelecionar
            // 
            btnSelecionar.FlatStyle = FlatStyle.Flat;
            btnSelecionar.Font = new Font("Arial", 9.75F);
            btnSelecionar.Location = new Point(15, 57);
            btnSelecionar.Name = "btnSelecionar";
            btnSelecionar.Size = new Size(101, 27);
            btnSelecionar.TabIndex = 26;
            btnSelecionar.Text = "Selecionar";
            btnSelecionar.UseVisualStyleBackColor = true;
            btnSelecionar.Click += btnSelecionarArquivoAccess_Click;
            // 
            // txtCaminhoArquivoAccess
            // 
            txtCaminhoArquivoAccess.Font = new Font("Arial", 9F);
            txtCaminhoArquivoAccess.Location = new Point(6, 25);
            txtCaminhoArquivoAccess.Name = "txtCaminhoArquivoAccess";
            txtCaminhoArquivoAccess.Size = new Size(229, 21);
            txtCaminhoArquivoAccess.TabIndex = 25;
            // 
            // btnConverterZpl
            // 
            btnConverterZpl.FlatStyle = FlatStyle.Flat;
            btnConverterZpl.Font = new Font("Arial", 9.75F);
            btnConverterZpl.Location = new Point(122, 57);
            btnConverterZpl.Name = "btnConverterZpl";
            btnConverterZpl.Size = new Size(101, 27);
            btnConverterZpl.TabIndex = 24;
            btnConverterZpl.Text = "Converter";
            btnConverterZpl.UseVisualStyleBackColor = true;
            btnConverterZpl.Click += btnConverterZpl_Click;
            // 
            // btnSelecionarArquivoAccess
            // 
            btnSelecionarArquivoAccess.FileName = "btnSelecionarArquivoAccess";
            // 
            // FormGestaoBancoDados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(594, 376);
            Controls.Add(gbMigracaoZpl);
            Controls.Add(lbDivisor2);
            Controls.Add(pbGestao);
            Controls.Add(lbDivisor1);
            Controls.Add(btnExportarBanco);
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
            ((System.ComponentModel.ISupportInitialize)nEmpresaN).EndInit();
            ((System.ComponentModel.ISupportInitialize)nFilial).EndInit();
            ((System.ComponentModel.ISupportInitialize)nEmpresa).EndInit();
            gbMigracaoZpl.ResumeLayout(false);
            gbMigracaoZpl.PerformLayout();
            ResumeLayout(false);
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
        private Button btnAlterarNumeracaoDocumentos;
        private Button btnExportarBanco;
        private Label lbDivisor1;
        private ProgressBar pbGestao;
        private NumericUpDown nEmpresaN;
        private Label label1;
        private Label lbDivisor2;
        private GroupBox gbMigracaoZpl;
        private Button btnConverterZpl;
        private OpenFileDialog btnSelecionarArquivoAccess;
        private TextBox txtCaminhoArquivoAccess;
        private Button btnSelecionar;
    }
}