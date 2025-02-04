namespace BlackSync.Forms
{
    partial class FormConfig
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
            lbServidor = new Label();
            lbBanco = new Label();
            lbSenha = new Label();
            lbUsuario = new Label();
            txtServidor = new TextBox();
            txtUsuario = new TextBox();
            txtBanco = new TextBox();
            txtSenha = new TextBox();
            btnTestar = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            txtDSN = new TextBox();
            lbDSN = new Label();
            button1 = new Button();
            btnSalvar = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // lbServidor
            // 
            lbServidor.AutoSize = true;
            lbServidor.Font = new Font("Segoe UI", 11F);
            lbServidor.Location = new Point(6, 29);
            lbServidor.Name = "lbServidor";
            lbServidor.Size = new Size(64, 20);
            lbServidor.TabIndex = 0;
            lbServidor.Text = "Servidor";
            // 
            // lbBanco
            // 
            lbBanco.AutoSize = true;
            lbBanco.FlatStyle = FlatStyle.System;
            lbBanco.Font = new Font("Segoe UI", 11F);
            lbBanco.Location = new Point(6, 59);
            lbBanco.Name = "lbBanco";
            lbBanco.Size = new Size(118, 20);
            lbBanco.TabIndex = 1;
            lbBanco.Text = "Banco de Dados";
            // 
            // lbSenha
            // 
            lbSenha.AutoSize = true;
            lbSenha.FlatStyle = FlatStyle.System;
            lbSenha.Font = new Font("Segoe UI", 11F);
            lbSenha.Location = new Point(6, 125);
            lbSenha.Name = "lbSenha";
            lbSenha.Size = new Size(49, 20);
            lbSenha.TabIndex = 3;
            lbSenha.Text = "Senha";
            // 
            // lbUsuario
            // 
            lbUsuario.AutoSize = true;
            lbUsuario.Font = new Font("Segoe UI", 11F);
            lbUsuario.Location = new Point(6, 92);
            lbUsuario.Name = "lbUsuario";
            lbUsuario.Size = new Size(59, 20);
            lbUsuario.TabIndex = 2;
            lbUsuario.Text = "Usuário";
            // 
            // txtServidor
            // 
            txtServidor.Location = new Point(127, 23);
            txtServidor.Name = "txtServidor";
            txtServidor.Size = new Size(151, 27);
            txtServidor.TabIndex = 1;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(127, 89);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(151, 27);
            txtUsuario.TabIndex = 3;
            // 
            // txtBanco
            // 
            txtBanco.Location = new Point(127, 56);
            txtBanco.Name = "txtBanco";
            txtBanco.Size = new Size(151, 27);
            txtBanco.TabIndex = 2;
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(127, 122);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(151, 27);
            txtSenha.TabIndex = 4;
            txtSenha.UseSystemPasswordChar = true;
            // 
            // btnTestar
            // 
            btnTestar.BackColor = Color.Crimson;
            btnTestar.FlatAppearance.BorderSize = 0;
            btnTestar.FlatStyle = FlatStyle.Flat;
            btnTestar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTestar.ForeColor = Color.Honeydew;
            btnTestar.Location = new Point(104, 161);
            btnTestar.Name = "btnTestar";
            btnTestar.Size = new Size(128, 28);
            btnTestar.TabIndex = 5;
            btnTestar.Text = "Testar conexão";
            btnTestar.UseVisualStyleBackColor = false;
            btnTestar.Click += btnTestar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbServidor);
            groupBox1.Controls.Add(btnTestar);
            groupBox1.Controls.Add(txtServidor);
            groupBox1.Controls.Add(txtBanco);
            groupBox1.Controls.Add(lbSenha);
            groupBox1.Controls.Add(txtSenha);
            groupBox1.Controls.Add(lbBanco);
            groupBox1.Controls.Add(txtUsuario);
            groupBox1.Controls.Add(lbUsuario);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(318, 195);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Configuração MySQL";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDSN);
            groupBox2.Controls.Add(lbDSN);
            groupBox2.Controls.Add(button1);
            groupBox2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox2.Location = new Point(355, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(355, 195);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Configuração Firebird";
            // 
            // txtDSN
            // 
            txtDSN.Location = new Point(123, 73);
            txtDSN.Name = "txtDSN";
            txtDSN.Size = new Size(226, 27);
            txtDSN.TabIndex = 6;
            // 
            // lbDSN
            // 
            lbDSN.AutoSize = true;
            lbDSN.Font = new Font("Segoe UI", 11F);
            lbDSN.Location = new Point(20, 79);
            lbDSN.Name = "lbDSN";
            lbDSN.Size = new Size(39, 20);
            lbDSN.TabIndex = 0;
            lbDSN.Text = "DSN";
            // 
            // button1
            // 
            button1.BackColor = Color.Crimson;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.Honeydew;
            button1.Location = new Point(92, 161);
            button1.Name = "button1";
            button1.Size = new Size(128, 28);
            button1.TabIndex = 9;
            button1.Text = "Testar conexão";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnTestarFirebird_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.DarkViolet;
            btnSalvar.FlatAppearance.BorderColor = Color.DarkViolet;
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.Honeydew;
            btnSalvar.Location = new Point(213, 232);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(255, 46);
            btnSalvar.TabIndex = 10;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // FormConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(722, 295);
            Controls.Add(btnSalvar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormConfig";
            Text = "FormConfig";
            Load += FormConfig_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lbServidor;
        private Label lbBanco;
        private Label lbSenha;
        private Label lbUsuario;
        private TextBox txtServidor;
        private TextBox txtUsuario;
        private TextBox txtBanco;
        private TextBox txtSenha;
        private Button btnTestar;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button1;
        private Label lbUsuarioFirebird;
        private TextBox txtUsuarioFirebird;
        private Label lbSenhaFirebird;
        private TextBox txtSenhaFirebird;
        private Label lbServidorFirebird;
        private OpenFileDialog btnSelecionarBancoFirebird;
        private Button btnSalvar;
        private TextBox txtDSN;
        private Label lbDSN;
    }
}