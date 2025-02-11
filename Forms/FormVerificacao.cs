using BlackSync.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSync.Services;

namespace BlackSync.Forms
{
    public partial class FormVerificacao : Form
    {
        private TableComparisonService tableComparisonService;

        public FormVerificacao(string mysqlServer, string mysqlDatabase, string mysqlUser, string mysqlPassword, string firebirdDSN)
        {
            InitializeComponent();
            tableComparisonService = new TableComparisonService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword, firebirdDSN);
        }

        private void btnVerificarTabelas_Click(object sender, EventArgs e)
        {
            CarregarTabelasMySQL();
            CarregarTabelasFirebird();
            CompararTabelas();
        }

        private void CarregarTabelasMySQL()
        {
            try
            {
                // Obter a conexão do MySQL a partir do ConfigService
                (string servidor, string banco, string usuario, string senha) = ConfigService.CarregarConfiguracaoMySQL();

                string connectionString = $"Server={servidor};Database={banco};User Id={usuario};Password={senha};";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SHOW TABLES";

                    using (var cmd = new MySqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gridMySQLTables.DataSource = dt;
                    }
                }

                // **Ajustar cabeçalho do MySQL com o nome do banco**
                (string _, string nomeBancoMySQL, _, _) = ConfigService.CarregarConfiguracaoMySQL();
                if (gridMySQLTables.Columns.Count > 0)
                {
                    gridMySQLTables.Columns[0].HeaderText = $"Tabelas - {nomeBancoMySQL}";
                }

                // **Ajustar aparência**
                AjustarGridView(gridMySQLTables);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas do MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarTabelasFirebird()
        {
            try
            {
                string dsnFirebird = ConfigService.CarregarConfiguracaoFirebird();

                string connectionString = $"DSN={dsnFirebird};";
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE RDB$VIEW_BLR IS NULL AND RDB$SYSTEM_FLAG = 0";

                    using (var cmd = new OdbcCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gridFirebirdTables.DataSource = dt;
                    }
                }

                // **Ajustar cabeçalho do Firebird com o nome do DSN**
                if (gridFirebirdTables.Columns.Count > 0)
                {
                    gridFirebirdTables.Columns[0].HeaderText = $"Tabelas - {dsnFirebird}";
                }

                // **Ajustar aparência**
                AjustarGridView(gridFirebirdTables);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas do Firebird: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompararTabelas()
        {
            // Obter listas de tabelas, ignorando linhas vazias
            List<string> tabelasMySQL = gridMySQLTables.Rows.Cast<DataGridViewRow>()
                .Where(row => row.Cells[0].Value != null) // Verifica se o valor não é nulo
                .Select(row => row.Cells[0].Value.ToString().Trim().ToUpper())
                .ToList();

            List<string> tabelasFirebird = gridFirebirdTables.Rows.Cast<DataGridViewRow>()
                .Where(row => row.Cells[0].Value != null) // Verifica se o valor não é nulo
                .Select(row => row.Cells[0].Value.ToString().Trim().ToUpper())
                .ToList();

            // Filtrar apenas tabelas que estão no Firebird e não no MySQL
            List<string> apenasNoFirebird = tabelasFirebird.Except(tabelasMySQL).ToList();

            // Criar DataTable para exibir as tabelas do Firebird que faltam no MySQL
            DataTable dt = new DataTable();
            dt.Columns.Add("Tabela"); // Agora só tem essa coluna!

            foreach (var tabela in apenasNoFirebird)
                dt.Rows.Add(tabela);

            // Atualizar grid de diferenças
            gridDiferencas.DataSource = dt;

            // Ajustar aparência da grid
            AjustarGridView(gridDiferencas);
        }

        private void AjustarGridView(DataGridView grid)
        {
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9); // Reduzir a fonte
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Cabeçalho maior e em negrito
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Ajusta colunas automaticamente
            grid.AutoResizeColumns(); // Redimensiona para caber na grid
        }

        private async void btnGerarScripts_Click(object sender, EventArgs e)
        {
            if (gridDiferencas.Rows.Count == 0)
            {
                MessageBox.Show("Nenhuma tabela encontrada para gerar script.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Abrir caixa de diálogo para escolher onde salvar o script
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Salvar Script MySQL",
                Filter = "Arquivo SQL (*.sql)|*.sql",
                DefaultExt = "sql",
                FileName = "script-mysql-verificao.sql"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return; // Se o usuário cancelar, não faz nada
            }

            string caminhoMySQL = saveFileDialog.FileName; // Caminho escolhido pelo usuário

            // Mostrar a barra de progresso antes de iniciar o processo
            progressBarScripts.Visible = true;
            progressBarScripts.Value = 0; // Resetar o progresso
            progressBarScripts.Maximum = gridDiferencas.Rows.Count; // Definir o total de tabelas
            btnGerarScripts.Enabled = false;

            // Recuperar configurações do Firebird
            string firebirdDSN = ConfigService.CarregarConfiguracaoFirebird();
            StringBuilder scriptMySQL = new StringBuilder();

            await Task.Run(() =>
            {
                int progresso = 0;

                foreach (DataGridViewRow row in gridDiferencas.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string tabela = row.Cells[0].Value.ToString();

                    // Gera script apenas para tabelas que estão no Firebird e faltam no MySQL
                    scriptMySQL.AppendLine($"-- Criar tabela {tabela} no MySQL");
                    scriptMySQL.AppendLine(ScriptGeneratorService.GerarScriptFirebirdParaMySQL(tabela, firebirdDSN));
                    scriptMySQL.AppendLine();

                    // Atualizar progresso na UI
                    progresso++;
                    Invoke(new Action(() => progressBarScripts.Value = progresso));
                }
            });

            try
            {
                File.WriteAllText(caminhoMySQL, scriptMySQL.ToString(), Encoding.UTF8);
                MessageBox.Show($"Scripts gerados com sucesso!\n\n📄 {caminhoMySQL}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os arquivos de script: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ocultar barra de progresso e reativar botão após a conclusão
                progressBarScripts.Visible = false;
                btnGerarScripts.Enabled = true;
            }
        }
    }
}
