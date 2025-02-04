namespace BlackSync.Forms
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            tabControlPrincipal = new TabControl();
            tabConfig = new TabPage();
            tabVerificacao = new TabPage();
            tabMigracao = new TabPage();
            tabLog = new TabPage();
            tabEstrutura = new TabPage();
            tabControlPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            resources.ApplyResources(tabControlPrincipal, "tabControlPrincipal");
            tabControlPrincipal.Controls.Add(tabConfig);
            tabControlPrincipal.Controls.Add(tabVerificacao);
            tabControlPrincipal.Controls.Add(tabEstrutura);
            tabControlPrincipal.Controls.Add(tabMigracao);
            tabControlPrincipal.Controls.Add(tabLog);
            tabControlPrincipal.Multiline = true;
            tabControlPrincipal.Name = "tabControlPrincipal";
            tabControlPrincipal.SelectedIndex = 0;
            tabControlPrincipal.SizeMode = TabSizeMode.Fixed;
            tabControlPrincipal.TabStop = false;
            // 
            // tabConfig
            // 
            resources.ApplyResources(tabConfig, "tabConfig");
            tabConfig.BackColor = Color.DimGray;
            tabConfig.ForeColor = SystemColors.ControlText;
            tabConfig.Name = "tabConfig";
            tabConfig.Click += tabConfig_Click;
            // 
            // tabVerificacao
            // 
            resources.ApplyResources(tabVerificacao, "tabVerificacao");
            tabVerificacao.BackColor = Color.DimGray;
            tabVerificacao.Name = "tabVerificacao";
            // 
            // tabMigracao
            // 
            resources.ApplyResources(tabMigracao, "tabMigracao");
            tabMigracao.BackColor = Color.DimGray;
            tabMigracao.Name = "tabMigracao";
            // 
            // tabLog
            // 
            resources.ApplyResources(tabLog, "tabLog");
            tabLog.BackColor = Color.DimGray;
            tabLog.Name = "tabLog";
            // 
            // tabEstrutura
            // 
            resources.ApplyResources(tabEstrutura, "tabEstrutura");
            tabEstrutura.Name = "tabEstrutura";
            tabEstrutura.UseVisualStyleBackColor = true;
            // 
            // FormPrincipal
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            Controls.Add(tabControlPrincipal);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormPrincipal";
            ShowInTaskbar = false;
            Load += FormPrincipal_Load;
            tabControlPrincipal.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlPrincipal;
        private TabPage tabConfig;
        private TabPage tabMigracao;
        private TabPage tabLog;
        private TabPage tabVerificacao;
        private TabPage tabEstrutura;
    }
}