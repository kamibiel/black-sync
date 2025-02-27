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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            gridFirebirdTables = new DataGridView();
            gridMySQLTables = new DataGridView();
            btnVerificarTabelas = new Button();
            gridDiferencas = new DataGridView();
            btnGerarScripts = new Button();
            progressBarScripts = new ProgressBar();
            gbFirebird = new GroupBox();
            gbMySQL = new GroupBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gridFirebirdTables).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridMySQLTables).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridDiferencas).BeginInit();
            gbFirebird.SuspendLayout();
            gbMySQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // gridFirebirdTables
            // 
            gridFirebirdTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridFirebirdTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridFirebirdTables.Location = new Point(6, 18);
            gridFirebirdTables.Name = "gridFirebirdTables";
            gridFirebirdTables.Size = new Size(172, 199);
            gridFirebirdTables.TabIndex = 1;
            // 
            // gridMySQLTables
            // 
            gridMySQLTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridMySQLTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridMySQLTables.Location = new Point(6, 18);
            gridMySQLTables.Name = "gridMySQLTables";
            gridMySQLTables.Size = new Size(172, 199);
            gridMySQLTables.TabIndex = 2;
            // 
            // btnVerificarTabelas
            // 
            btnVerificarTabelas.BackColor = Color.DimGray;
            btnVerificarTabelas.FlatStyle = FlatStyle.Flat;
            btnVerificarTabelas.Font = new Font("Arial", 9.75F);
            btnVerificarTabelas.Location = new Point(12, 391);
            btnVerificarTabelas.Name = "btnVerificarTabelas";
            btnVerificarTabelas.Size = new Size(182, 41);
            btnVerificarTabelas.TabIndex = 3;
            btnVerificarTabelas.Text = "Verificar tabelas";
            btnVerificarTabelas.UseVisualStyleBackColor = false;
            btnVerificarTabelas.Click += btnVerificarTabelas_Click;
            // 
            // gridDiferencas
            // 
            gridDiferencas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            gridDiferencas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            gridDiferencas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            gridDiferencas.DefaultCellStyle = dataGridViewCellStyle6;
            gridDiferencas.Location = new Point(12, 259);
            gridDiferencas.Name = "gridDiferencas";
            gridDiferencas.Size = new Size(376, 126);
            gridDiferencas.TabIndex = 4;
            // 
            // btnGerarScripts
            // 
            btnGerarScripts.FlatAppearance.BorderColor = Color.Red;
            btnGerarScripts.FlatStyle = FlatStyle.Flat;
            btnGerarScripts.Font = new Font("Arial", 9.75F);
            btnGerarScripts.Location = new Point(206, 391);
            btnGerarScripts.Name = "btnGerarScripts";
            btnGerarScripts.Size = new Size(182, 41);
            btnGerarScripts.TabIndex = 4;
            btnGerarScripts.Text = "Gerar Scripts";
            btnGerarScripts.UseVisualStyleBackColor = true;
            btnGerarScripts.Click += btnGerarScripts_Click;
            // 
            // progressBarScripts
            // 
            progressBarScripts.Location = new Point(12, 241);
            progressBarScripts.Name = "progressBarScripts";
            progressBarScripts.Size = new Size(374, 12);
            progressBarScripts.Style = ProgressBarStyle.Marquee;
            progressBarScripts.TabIndex = 5;
            progressBarScripts.Visible = false;
            // 
            // gbFirebird
            // 
            gbFirebird.Controls.Add(gridFirebirdTables);
            gbFirebird.Location = new Point(12, 12);
            gbFirebird.Name = "gbFirebird";
            gbFirebird.Size = new Size(184, 223);
            gbFirebird.TabIndex = 6;
            gbFirebird.TabStop = false;
            gbFirebird.Text = "Tabelas Firebird";
            // 
            // gbMySQL
            // 
            gbMySQL.Controls.Add(dataGridView1);
            gbMySQL.Controls.Add(gridMySQLTables);
            gbMySQL.Location = new Point(202, 12);
            gbMySQL.Name = "gbMySQL";
            gbMySQL.Size = new Size(184, 223);
            gbMySQL.TabIndex = 7;
            gbMySQL.TabStop = false;
            gbMySQL.Text = "Tabelas MySQL";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 18);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(172, 199);
            dataGridView1.TabIndex = 1;
            // 
            // FormVerificacao
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(405, 450);
            Controls.Add(gbMySQL);
            Controls.Add(gbFirebird);
            Controls.Add(progressBarScripts);
            Controls.Add(btnGerarScripts);
            Controls.Add(gridDiferencas);
            Controls.Add(btnVerificarTabelas);
            Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "FormVerificacao";
            Text = "FormVerificacao";
            ((System.ComponentModel.ISupportInitialize)gridFirebirdTables).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridMySQLTables).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridDiferencas).EndInit();
            gbFirebird.ResumeLayout(false);
            gbMySQL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridFirebirdTables;
        private DataGridView gridMySQLTables;
        private Button btnVerificarTabelas;
        private DataGridView gridDiferencas;
        private Button btnGerarScripts;
        private ProgressBar progressBarScripts;
        private GroupBox gbFirebird;
        private GroupBox gbMySQL;
        private DataGridView dataGridView1;
    }
}