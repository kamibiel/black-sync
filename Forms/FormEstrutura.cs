using BlackSync.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackSync.Forms
{
    public partial class FormEstrutura : Form
    {
        private readonly MySQLService _mySQLService;
        private readonly FirebirdService _firebirdService;
        private List<string> tabelasComErro = new List<string>();

        public FormEstrutura(string mysqlServer, string mysqlDatabase, string mysqlUser, string mysqlPassword, string firebirdDSN)
        {
            InitializeComponent();
            _mySQLService = new MySQLService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword);
            _firebirdService = new FirebirdService(firebirdDSN);
            CarregarTabelas();
        }

        private void CarregarTabelas()
        {
            try
            {
                var tabelasFirebird = _firebirdService.GetTabelasFirebird();
                clbTabelasFirebird.Items.Clear();
                foreach (var tabela in tabelasFirebird)
                    clbTabelasFirebird.Items.Add(tabela, false);

                var tabelasMySQL = _mySQLService.GetTabelasMySQL();
                clbTabelasMySQL.Items.Clear();
                foreach (var tabela in tabelasMySQL)
                    clbTabelasMySQL.Items.Add(tabela, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSelecionarTabelas_CheckedChanged(object sender, EventArgs e)
        {
            // Verifica o estado do checkbox
            bool marcarTodas = cbSelecionarTabelas.Checked;

            // Atualiza o texto do checkbox dinamicamente
            cbSelecionarTabelas.Text = marcarTodas ? "Desmarcar todas as tabelas" : "Selecionar todas as tabelas";

            // Pecorre os itens do CheckedListBox e marca/desmarca todos
            for (int i = 0; i < clbTabelasFirebird.Items.Count; i++)
            {
                clbTabelasFirebird.SetItemChecked(i, marcarTodas);
            }
        }

        /// <summary>
        /// Obtém a lista de tabelas selecionadas no CheckedListBox do Firebird.
        /// </summary>
        private List<string> ObterTabelasSelecionadas()
        {
            return clbTabelasFirebird.CheckedItems.Cast<string>().ToList();
        }

        private async void btnVerificarEstrutura_Click(object sender, EventArgs e)
        {
            var tabelasSelecionadas = ObterTabelasSelecionadas();

            if (tabelasSelecionadas.Count == 0)
            {
                MessageBox.Show("Por favor, selecione ao menos uma tabela para verificar a estrutura.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar a barra de progresso antes de iniciar o processo
            pbCarregamentoScripts.Visible = true;
            pbCarregamentoScripts.Value = 0; // Resetar o progresso
            pbCarregamentoScripts.Maximum = tabelasSelecionadas.Count;
            btnVerificarEstrutura.Enabled = false;


            await Task.Run(() =>
            {
                CompararEstruturaTabelas(tabelasSelecionadas);

                // Atualizar a barra de progresso na UI Thread
                Invoke(new Action(() =>
                {
                    pbCarregamentoScripts.Value = pbCarregamentoScripts.Maximum;
                }));
            });

            // Reativar botão e esconder barra de progresso após finalização
            pbCarregamentoScripts.Visible = false;
            btnVerificarEstrutura.Enabled = true;
        }

        private void CompararEstruturaTabelas(List<string> tabelasSelecionadas)
        {
            Invoke(new Action(() =>
            {
                txtSaida.Clear();
                tabelasComErro.Clear();
            }));

            StringBuilder resultado = new StringBuilder();

            foreach (var tabela in tabelasSelecionadas)
            {
                var colunasFaltantes = _firebirdService.CompararEstrutura(tabela, _mySQLService);

                if (colunasFaltantes.Any())
                {
                    tabelasComErro.Add(tabela);
                    resultado.AppendLine($"❌ Tabela {tabela} precisa de ajustes! {colunasFaltantes.Count} colunas faltando.");
                }
            }

            // Atualiza a UI após processar todas as tabelas
            Invoke(new Action(() =>
            {
                txtSaida.Text = resultado.ToString();
            }));
        }

        private void btnGerarScript_Click(object sender, EventArgs e)
        {
            if (tabelasComErro.Count == 0)
            {
                MessageBox.Show("Nenhuma tabela precisa ser corrigida!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StringBuilder scriptFinal = new StringBuilder();

            foreach (var tabela in tabelasComErro)
            {
                var colunasFaltantes = _firebirdService.CompararEstrutura(tabela, _mySQLService);
                string script = ScriptGeneratorService.GerarScriptAlteracao(tabela, colunasFaltantes);

                if (!string.IsNullOrEmpty(script))
                    scriptFinal.AppendLine(script);
            }

            SalvarScript(scriptFinal);
        }

        private void SalvarScript(StringBuilder script)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Salvar Script MySQL",
                Filter = "Arquivo SQL (*.sql)|*.sql",
                DefaultExt = "sql",
                FileName = "script-mysql-estrutura.sql"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllText(saveFileDialog.FileName, script.ToString(), Encoding.UTF8);
            MessageBox.Show($"Script salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormEstrutura_Load(object sender, EventArgs e)
        {

        }
    }
}
