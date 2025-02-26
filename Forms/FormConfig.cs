using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSync.Services;
using FirebirdSql.Data.FirebirdClient;

namespace BlackSync.Forms
{
    public partial class FormConfig : Form
    {
        private FormPrincipal _formPrincipal;

        public FormConfig(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            _formPrincipal = formPrincipal;
        }


        private void tabControlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_formPrincipal.TabControlPrincipal.SelectedTab.Text == "Configuração")
            {
                CarregarConfiguracao();
            }
        }

        public void CarregarConfiguracao()
        {
            if (ConfigService.ExisteConfiguracao())
            {
                // Carregar configuração do MySQL
                (string servidorMySQL, string bancoMySQL, string usuarioMySQL, string senhaMySQL) = ConfigService.CarregarConfiguracaoMySQL();
                txtServidor.Text = servidorMySQL;
                txtBanco.Text = bancoMySQL;
                txtUsuario.Text = usuarioMySQL;
                txtSenha.Text = senhaMySQL;

                // Carregar configuração do Firebird
                string dsnFirebird = ConfigService.CarregarConfiguracaoFirebird();
                txtDSN.Text = dsnFirebird;
            }
        }

        // Testar conexão com MySQL
        private void btnTestar_Click(object sender, EventArgs e)
        {
            string servidor = txtServidor.Text;
            string banco = txtBanco.Text;
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            var mysqlService = new MySQLService(servidor, banco, usuario, senha);
            mysqlService.TestarConexao();
        }

        // Testar Conexão com o Firebird
        private void btnTestarFirebird_Click(object sender, EventArgs e)
        {
            string dsnFirebird = txtDSN.Text;

            var firebirdService = new FirebirdService(dsnFirebird);
            firebirdService.TestarConexao();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Salvar MySQL
            string servidorMySQL = txtServidor.Text;
            string bancoMySQL = txtBanco.Text;
            string usuarioMySQL = txtUsuario.Text;
            string senhaMySQL = txtSenha.Text;
            ConfigService.SalvarConfiguracaoMySQL(servidorMySQL, bancoMySQL, usuarioMySQL, senhaMySQL);

            // Salvar Firebird
            string dsnFirebird = txtDSN.Text;
            ConfigService.SalvarConfiguracaoFirebird(dsnFirebird);

            // 🔹 Atualiza imediatamente os serviços antes de exibir a mensagem
            LogService.RegistrarLog("INFO", $"🔄 Atualizando os serviços.");
            _formPrincipal.AtualizarServicos();

            // Log e mensagem ao usuário
            LogService.RegistrarLog("SUCCESS", $"✅ Configurações de MySQL e Firebird salvas com sucesso!");
            MessageBox.Show(
                "Configurações de MySQL e Firebird salvas com sucesso!", 
                "Sucesso", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );
        }

        private void txtServidorFirebird_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos Firebird (*.fdb)|*.fdb"; // Filtro para arquivos Firebird
            openFileDialog.Title = "Selecione o banco de dados Firebird";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtDSN.Text = openFileDialog.FileName; // Exibe o caminho do banco no TextBox
            }
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            // Código para inicialização do form, se necessário
        }
    }
}
