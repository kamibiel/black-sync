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
            bool marcarTodas = cbSelecionarTabelas.Checked;

            cbSelecionarTabelas.Text = marcarTodas ? "Desmarcar todas as tabelas" : "Selecionar todas as tabelas";

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

            pbCarregamentoScripts.Visible = true;
            pbCarregamentoScripts.Value = 0;
            pbCarregamentoScripts.Maximum = tabelasSelecionadas.Count;
            btnVerificarEstrutura.Enabled = false;

            tabelasComErro.Clear();

            await Task.Run(() =>
            {
                for (int i = 0; i < tabelasSelecionadas.Count; i++)
                {
                    var tabelaAtual = tabelasSelecionadas[i];

                    if (CompararEstruturaTabelas(new List<string> { tabelaAtual }))
                    {
                        tabelasComErro.Add(tabelaAtual);
                    }

                    Invoke(new Action(() =>
                    {
                        pbCarregamentoScripts.Value = i + 1;
                    }));
                }
            });

            // Atualizar a lista de tabelas no clbTabelasFirebird sem limpar completamente
            Invoke(new Action(() =>
            {
                for (int i = 0; i < clbTabelasFirebird.Items.Count; i++)
                {
                    string tabela = clbTabelasFirebird.Items[i].ToString();
                    clbTabelasFirebird.SetItemChecked(i, tabelasComErro.Contains(tabela));
                }
            }));

            pbCarregamentoScripts.Visible = false;
            btnVerificarEstrutura.Enabled = true;
        }

        private bool CompararEstruturaTabelas(List<string> tabelasSelecionadas)
        {
            bool encontrouErro = false;
            StringBuilder resultado = new StringBuilder();

            foreach (var tabela in tabelasSelecionadas)
            {
                var estruturaMySQL = _mySQLService.ObterEstruturaTabela(tabela);

                if (estruturaMySQL == null || estruturaMySQL.Count == 0) // 🔹 Verifica se a tabela existe no MySQL
                {
                    encontrouErro = true;
                    resultado.AppendLine($"🚨 Tabela {tabela} **NÃO EXISTE** no MySQL e precisa ser criada.");
                }
                else
                {
                    var colunasFaltantes = _firebirdService.CompararEstrutura(tabela, _mySQLService);

                    if (colunasFaltantes.Any())
                    {
                        encontrouErro = true;
                        resultado.AppendLine($"❌ Tabela {tabela} precisa de ajustes! {colunasFaltantes.Count} colunas faltando.");
                    }
                }
            }

            if (encontrouErro)
            {
                Invoke(new Action(() =>
                {
                    txtSaida.Text += resultado.ToString();
                }));
            }

            return encontrouErro;
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
                var estruturaMySQL = _mySQLService.ObterEstruturaTabela(tabela);

                // 🛑 Debug para verificar a existência da tabela
                Console.WriteLine($"🔍 Verificando tabela: {tabela}");
                Console.WriteLine($"🔹 Estrutura retornada: {estruturaMySQL?.Count} colunas");

                // 🔹 Se a estrutura for vazia, usamos o método `VerificarSeTabelaExiste`
                if (estruturaMySQL == null || estruturaMySQL.Count == 0)
                {
                    if (!_mySQLService.VerificarSeTabelaExiste(tabela))
                    {
                        scriptFinal.AppendLine($"-- ❌ A tabela {tabela} não existe no MySQL. Precisa ser criada manualmente.");
                        continue;
                    }
                }

                // 🔹 Se a tabela existe, verificamos colunas faltantes e geramos ALTER TABLE
                var colunasFaltantes = _firebirdService.CompararEstrutura(tabela, _mySQLService);
                if (colunasFaltantes.Any())
                {
                    string alterScript = ScriptGeneratorService.GerarScriptAlteracao(tabela, colunasFaltantes);
                    if (!string.IsNullOrEmpty(alterScript))
                        scriptFinal.AppendLine(alterScript);
                }
            }

            if (scriptFinal.Length == 0)
            {
                MessageBox.Show("Nenhum script necessário!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
