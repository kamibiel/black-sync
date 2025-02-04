using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackSync.Forms
{
    public partial class FormPrincipal : Form
    {
        private static readonly string confiFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        
        public FormPrincipal()
        {
            InitializeComponent();
            VerificarConfiguracao();
            CarregarFormularios();
            this.ShowInTaskbar = true;
        }

        public TabControl TabControlPrincipal
        {
            get { return this.tabControlPrincipal; }
        }

        private void VerificarConfiguracao()
        {
            // Se o config.ini existir, perguntar ao usuário
            if (File.Exists(confiFilePath))
            {
                DialogResult resultado = MessageBox.Show("Deseja manter a configuração existente?", "Configuração Detectada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Garante que o formulário de configuração seja carregado antes da troca de aba
                FormConfig formConfig = new FormConfig(this);
                AdicionarFormularioAba(formConfig, tabConfig);

                if (resultado == DialogResult.Yes)
                {
                    // Carrega os dados da configuração antes de mudar para a aba de verificação
                    formConfig.CarregarConfiguracao();
                    tabControlPrincipal.SelectedTab = tabVerificacao;
                }
                else
                {
                    // Abre a aba "Configuração"
                    tabControlPrincipal.SelectedTab = tabConfig;
                }
            }
            else
            {
                // Se não existir config.ini, abre a aba "Configuração"
                MessageBox.Show("Nenhuma configuração encontrada. Por favor, configure o sistema.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControlPrincipal.SelectedTab = tabConfig;
            }
        }

        private void CarregarFormularios()
        {
            AdicionarFormularioAba(new FormConfig(this), tabConfig);
        }

        private void AdicionarFormularioAba(Form form, TabPage aba)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            aba.Controls.Add(form);
            form.Show();
        }

        private void tabConfig_Click(object sender, EventArgs e)
        {

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
