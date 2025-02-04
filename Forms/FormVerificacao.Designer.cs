namespace BlackSync.Forms
{
    partial class FormVerificacao
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            gridFirebirdTables = new DataGridView();
            lbFirebird = new Label();
            lbMySQL = new Label();
            gridMySQLTables = new DataGridView();
            btnVerificarTabelas = new Button();
            gridDiferencas = new DataGridView();
            btnGerarScripts = new Button();
            progressBarScripts = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)gridFirebirdTables).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridMySQLTables).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridDiferencas).BeginInit();
            SuspendLayout();
            // 
            // gridFirebirdTables
            // 
            gridFirebirdTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridFirebirdTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridFirebirdTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridFirebirdTables.Location = new Point(37, 52);
            gridFirebirdTables.Name = "gridFirebirdTables";
            gridFirebirdTables.Size = new Size(267, 262);
            gridFirebirdTables.TabIndex = 1;
            // 
            // lbFirebird
            // 
            lbFirebird.AutoSize = true;
            lbFirebird.Location = new Point(132, 21);
            lbFirebird.Name = "lbFirebird";
            lbFirebird.Size = new Size(68, 19);
            lbFirebird.TabIndex = 1;
            lbFirebird.Text = "Firebird";
            // 
            // lbMySQL
            // 
            lbMySQL.AutoSize = true;
            lbMySQL.Location = new Point(426, 21);
            lbMySQL.Name = "lbMySQL";
            lbMySQL.Size = new Size(64, 19);
            lbMySQL.TabIndex = 3;
            lbMySQL.Text = "MySQL";
            // 
            // gridMySQLTables
            // 
            gridMySQLTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            gridMySQLTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            gridMySQLTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridMySQLTables.Location = new Point(331, 52);
            gridMySQLTables.Name = "gridMySQLTables";
            gridMySQLTables.Size = new Size(267, 262);
            gridMySQLTables.TabIndex = 2;
            // 
            // btnVerificarTabelas
            // 
            btnVerificarTabelas.BackColor = Color.DarkViolet;
            btnVerificarTabelas.FlatAppearance.BorderSize = 0;
            btnVerificarTabelas.FlatStyle = FlatStyle.Flat;
            btnVerificarTabelas.Location = new Point(226, 320);
            btnVerificarTabelas.Name = "btnVerificarTabelas";
            btnVerificarTabelas.Size = new Size(185, 41);
            btnVerificarTabelas.TabIndex = 3;
            btnVerificarTabelas.Text = "Verificar tabelas";
            btnVerificarTabelas.UseVisualStyleBackColor = false;
            btnVerificarTabelas.Click += btnVerificarTabelas_Click;
            // 
            // gridDiferencas
            // 
            gridDiferencas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            gridDiferencas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            gridDiferencas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            gridDiferencas.DefaultCellStyle = dataGridViewCellStyle4;
            gridDiferencas.Location = new Point(36, 367);
            gridDiferencas.Name = "gridDiferencas";
            gridDiferencas.Size = new Size(300, 126);
            gridDiferencas.TabIndex = 4;
            // 
            // btnGerarScripts
            // 
            btnGerarScripts.FlatAppearance.BorderColor = Color.Red;
            btnGerarScripts.FlatAppearance.BorderSize = 2;
            btnGerarScripts.FlatStyle = FlatStyle.Flat;
            btnGerarScripts.Location = new Point(357, 402);
            btnGerarScripts.Name = "btnGerarScripts";
            btnGerarScripts.Size = new Size(133, 51);
            btnGerarScripts.TabIndex = 4;
            btnGerarScripts.Text = "Gerar Scripts";
            btnGerarScripts.UseVisualStyleBackColor = true;
            btnGerarScripts.Click += btnGerarScripts_Click;
            // 
            // progressBarScripts
            // 
            progressBarScripts.Location = new Point(37, 497);
            progressBarScripts.Name = "progressBarScripts";
            progressBarScripts.Size = new Size(561, 12);
            progressBarScripts.Style = ProgressBarStyle.Marquee;
            progressBarScripts.TabIndex = 5;
            progressBarScripts.Visible = false;
            // 
            // FormVerificacao
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(630, 519);
            Controls.Add(progressBarScripts);
            Controls.Add(btnGerarScripts);
            Controls.Add(gridDiferencas);
            Controls.Add(btnVerificarTabelas);
            Controls.Add(lbMySQL);
            Controls.Add(gridMySQLTables);
            Controls.Add(lbFirebird);
            Controls.Add(gridFirebirdTables);
            Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "FormVerificacao";
            Text = "FormVerificacao";
            ((System.ComponentModel.ISupportInitialize)gridFirebirdTables).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridMySQLTables).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridDiferencas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridFirebirdTables;
        private Label lbFirebird;
        private Label lbMySQL;
        private DataGridView gridMySQLTables;
        private Button btnVerificarTabelas;
        private DataGridView gridDiferencas;
        private Button btnGerarScripts;
        private ProgressBar progressBarScripts;
    }
}