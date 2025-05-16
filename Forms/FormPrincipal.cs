using BlackSync.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackSync.Forms
{
    public partial class FormPrincipal : Form
    {
        private MySQLService _mySQLService;
        private FirebirdService _firebirdService;
        private FormConfig _formConfig;

        private static readonly string confiFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        
        public FormPrincipal()
        {
            InitializeComponent();

            if (VerificarConfiguracao())
            {
                AtualizarServicos();
                CarregarFormularios();
            }
          
            this.ShowInTaskbar = true;
        }

        private bool VerificarConfiguracao()
        {
            if (File.Exists(confiFilePath))
            {
                DialogResult resultado = MessageBox.Show(
                    "Deseja manter a configuração existente?",
                    "Configuração Detectada",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // 🔹 Garante que o FormConfig seja carregado corretamente
                _formConfig = new FormConfig(this);
                AdicionarFormularioAba(_formConfig, tabConfig);

                if (resultado == DialogResult.Yes)
                {
                    LogService.RegistrarLog("INFO", "🔄 Carregando configuração salva...");
                    _formConfig.CarregarConfiguracao();
                    tabControlPrincipal.SelectedTab = tabMigracao;
                    return true;
                }
                else
                {
                    tabControlPrincipal.SelectedTab = tabConfig;
                    return false;
                }
            }
            else
            {
                // Se não existir config.ini, abre a aba "Configuração"
                MessageBox.Show(
                    "Nenhuma configuração encontrada. Por favor, configure o sistema.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // 🔹 Agora, carrega o FormConfig para evitar que a tela fique vazia!
                _formConfig = new FormConfig(this);
                AdicionarFormularioAba(_formConfig, tabConfig);

                tabControlPrincipal.SelectedTab = tabConfig;
                return false;
            }
        }

        public MySQLService ObterMySQLService() => _mySQLService;
        public FirebirdService ObterFirebirdService() => _firebirdService;

        public TabControl TabControlPrincipal
        {
            get { return this.tabControlPrincipal; }
        }

        private void CarregarFormularios()
        {
            string mysqlServer = ConfigService.CarregarConfiguracaoMySQL().servidor;
            string mysqlDatabase = ConfigService.CarregarConfiguracaoMySQL().banco;
            string mysqlUser = ConfigService.CarregarConfiguracaoMySQL().usuario;
            string mysqlPassword = ConfigService.CarregarConfiguracaoMySQL().senha;
            string firebirdDSN = ConfigService.CarregarConfiguracaoFirebird();

            _mySQLService = new MySQLService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword);
            _firebirdService = new FirebirdService(firebirdDSN);

            if (_formConfig == null)
                _formConfig = new FormConfig(this);
            AdicionarFormularioAba(_formConfig, tabConfig);
            //AdicionarFormularioAba(new FormConfig(this), tabConfig);
            // AdicionarFormularioAba(new FormVerificacao(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabVerificacao);
            // AdicionarFormularioAba(new FormEstrutura(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabEstrutura);
            AdicionarFormularioAba(new FormMigracao(this, mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabMigracao);
            AdicionarFormularioAba(new FormGestaoBancoDados(this, mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabManutencao);
            AdicionarFormularioAba(new FormLogs(), tabLog);
        }

        public void AtualizarServicos()
        {
            (string mysqlServer, string mysqlDatabase, string mysqlUser, string mysqlPassword) = ConfigService.CarregarConfiguracaoMySQL();
            string firebirdDSN = ConfigService.CarregarConfiguracaoFirebird();

            _mySQLService = new MySQLService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword);
            _firebirdService = new FirebirdService(firebirdDSN);

            LogService.RegistrarLog("SUCCESS", $"🔄 Serviços de banco de dados foram atualizados com novas configurações.");

            AtualizarFormularios();
        }

        public void AtualizarFormularios()
        {
            //tabVerificacao.Controls.Clear();
            //tabEstrutura.Controls.Clear();
            tabMigracao.Controls.Clear();
            tabManutencao.Controls.Clear();
            tabLog.Controls.Clear();

            string mysqlServer = ConfigService.CarregarConfiguracaoMySQL().servidor;
            string mysqlDatabase = ConfigService.CarregarConfiguracaoMySQL().banco;
            string mysqlUser = ConfigService.CarregarConfiguracaoMySQL().usuario;
            string mysqlPassword = ConfigService.CarregarConfiguracaoMySQL().senha;
            string firebirdDSN = ConfigService.CarregarConfiguracaoFirebird();

            // Recria os formulários com as novas configurações
            //AdicionarFormularioAba(new FormVerificacao(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabVerificacao);
            //AdicionarFormularioAba(new FormEstrutura(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabEstrutura);
            AdicionarFormularioAba(new FormMigracao(this, mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabMigracao);
            AdicionarFormularioAba(new FormGestaoBancoDados(this, mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN), tabManutencao);
            AdicionarFormularioAba(new FormLogs(), tabLog);
        }


        private void AdicionarFormularioAba(Form form, TabPage aba)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            aba.Controls.Clear();
            aba.Controls.Add(form);

            form.Show();
            form.BringToFront();
        }

        private void tabConfig_Click(object sender, EventArgs e)
        {

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.Font = new Font("Arial", 10);
        }
    }
}
