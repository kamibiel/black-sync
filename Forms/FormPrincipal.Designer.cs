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
            tabMigracao = new TabPage();
            tabManutencao = new TabPage();
            tabLog = new TabPage();
            tabControlPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            resources.ApplyResources(tabControlPrincipal, "tabControlPrincipal");
            tabControlPrincipal.Controls.Add(tabConfig);
            tabControlPrincipal.Controls.Add(tabMigracao);
            tabControlPrincipal.Controls.Add(tabManutencao);
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
            // tabMigracao
            // 
            resources.ApplyResources(tabMigracao, "tabMigracao");
            tabMigracao.BackColor = Color.DimGray;
            tabMigracao.Name = "tabMigracao";
            // 
            // tabManutencao
            // 
            resources.ApplyResources(tabManutencao, "tabManutencao");
            tabManutencao.BackColor = Color.DimGray;
            tabManutencao.Name = "tabManutencao";
            // 
            // tabLog
            // 
            resources.ApplyResources(tabLog, "tabLog");
            tabLog.BackColor = Color.DimGray;
            tabLog.Name = "tabLog";
            // 
            // FormPrincipal
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            Controls.Add(tabControlPrincipal);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            IsMdiContainer = true;
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
        private TabPage tabManutencao;
    }
}