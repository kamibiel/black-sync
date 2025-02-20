using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace BlackSync.Services
{
    public class MySQLService
    {
        private string connectionString;
        private string _banco;

        public MySQLService(string servidor, string banco, string usuario, string senha)
        {
            _banco = banco;
            connectionString = $"Server={servidor};Database={banco};User={usuario};Password={senha};";
        }

        public bool TestarConexao()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Conexão com MySQL bem-sucedida!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool VerificarSeTabelaExiste(string tabela)
        {
            tabela = tabela.ToLower(); // 🔹 Converte para minúsculas para evitar erros

            string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{_banco}' AND LOWER(TABLE_NAME) = '{tabela}'";

            using (var connection = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, connection))
            {
                connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // Se houver retorno, a tabela existe
            }
        }

        public List<string> GetTabelasMySQL()
        {
            List<string> tabelas = new List<string>();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SHOW TABLES";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tabelas.Add(reader.GetString(0).Trim().ToLower());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter tabelas do MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabelas;
        }

        /// <summary>
        /// Obtém a estrutura da tabela no MySQL (nomes e tipos das colunas).
        /// </summary>
        public List<(string Nome, string Tipo)> ObterEstruturaTabela(string tabela)
        {
            List<(string Nome, string Tipo)> estrutura = new List<(string Nome, string Tipo)>();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔹 Força a verificação do banco de dados correto
                    string query = $@"
                SELECT COLUMN_NAME, DATA_TYPE 
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = '{_banco}' 
                AND TABLE_NAME COLLATE utf8_general_ci = '{tabela.ToLower()}'";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nome = reader["COLUMN_NAME"].ToString().Trim().ToLower();
                            string tipo = reader["DATA_TYPE"].ToString().Trim();

                            estrutura.Add((nome, tipo));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter estrutura da tabela {tabela} no MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return estrutura;
        }

        public void ExecutarScript(string script)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(script, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar script no MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool TabelaTemDados(string tabela)
        {
            bool temDados = false;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SELECT COUNT(*) FROM `{tabela.ToLower()}`";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        temDados = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar dados na tabela {tabela}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Se der erro, assume que a tabela está vazia para evitar problemas.
            }

            return temDados;
        }

        public void TruncateTabela(string tabela)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"TRUNCATE TABLE `{tabela.ToLower()}`";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao truncar a tabela {tabela}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable ObterDadosTabela(string tabela)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SELECT * FROM \"{tabela}\"";

                    Console.WriteLine($"🔍 Executando consulta: {query}"); // Log para depuração

                    using (var cmd = new OdbcCommand(query, conn))
                    using (var adapter = new OdbcDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter dados da tabela {tabela} no Firebird: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public List<string> GetColunasTabelaFirebird(string tabela)
        {
            List<string> colunas = new List<string>();

            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    string query = $@"
                SELECT RDB$FIELD_NAME FROM RDB$RELATION_FIELDS 
                WHERE RDB$RELATION_NAME = '{tabela.ToUpper()}'";

                    using (var cmd = new OdbcCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colunas.Add(reader.GetString(0).Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter colunas da tabela {tabela}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return colunas;
        }

        public HashSet<string> ObterCodigosExistentes(string tabela, string colunaPK)
        {
            HashSet<string> codigosExistentes = new HashSet<string>();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SELECT `{colunaPK}` FROM `{tabela.ToLower()}`";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            codigosExistentes.Add(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter códigos existentes da tabela {tabela}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return codigosExistentes;
        }

        public void InserirDadosTabela(string tabela, DataTable dados)
        {
            try
            {
                if (dados.Rows.Count == 0)
                {
                    LogService.RegistrarLog("INFO", $"⚠️ Nenhum dado para inserir na tabela {tabela}.");
                    return;
                }

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    LogService.RegistrarLog("INFO", $"✅ Iniciando inserção de {dados.Rows.Count} registros na tabela {tabela} no MySQL.");

                    string tabelaMySQL = tabela.ToLower();
                    var estruturaTabela = ObterEstruturaTabela(tabelaMySQL);

                    var colunasMySQL = estruturaTabela.Select(c => c.Nome.ToLower()).ToList();
                    var colunasFirebird = dados.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();

                    var colunasValidas = colunasFirebird.Intersect(colunasMySQL).ToList();
                    var colunasExtrasMySQL = colunasMySQL.Except(colunasFirebird).ToList();

                    if (colunasValidas.Count == 0)
                    {
                        LogService.RegistrarLog("ERROR", $"❌ Nenhuma coluna compatível entre Firebird e MySQL na tabela {tabela}.");
                        return;
                    }

                    List<string> batchInserts = new List<string>();
                    int batchSize = 1000;
                    int totalInseridos = 0;
                    int linhaAtual = 0;

                    foreach (DataRow row in dados.Rows)
                    {
                        List<string> values = new List<string>();
                        linhaAtual++;

                        foreach (var coluna in colunasValidas)
                        {
                            string valor = row[coluna]?.ToString().Trim() ?? "NULL";

                            // 🔹 Verifica se a coluna é DECIMAL(15,4) ou DECIMAL(15,2)

                            var colunaEncontrada = estruturaTabela.FirstOrDefault(c => c.Nome.ToLower() == coluna);
                            var tipoColuna = colunaEncontrada != default ? colunaEncontrada.Tipo : null;

                            bool isDecimal = tipoColuna != null && (tipoColuna.Contains("decimal") || tipoColuna.Contains("float") || tipoColuna.Contains("numeric"));

                            if (isDecimal)
                            {
                                if (string.IsNullOrEmpty(valor) || valor.ToLower() == "null")
                                {
                                    valor = "0";
                                }
                                else
                                {
                                    // 🔹 Substituir vírgulas por pontos para evitar erro de conversão
                                    valor = valor.Replace(",", ".");

                                    if (decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal numero))
                                    {
                                        // 🔹 Determina a precisão baseada no DECIMAL(15,4) ou DECIMAL(15,2)
                                        int casasDecimais = tipoColuna.Contains("15,4") ? 4 : 2;
                                        decimal maxValor = casasDecimais == 4 ? 999999999999.9999m : 999999999999.99m;

                                        if (numero > maxValor || numero < -maxValor)
                                        {
                                            LogService.RegistrarLog("ERROR", $"🔴 ERRO na linha {linhaAtual}: Valor '{numero}' excede o limite para a coluna '{coluna}'. Ajustado para {maxValor}.");
                                            numero = maxValor;
                                        }

                                        valor = Math.Round(numero, casasDecimais).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        LogService.RegistrarLog("ERROR", $"⚠️ ERRO na linha {linhaAtual}: Não foi possível converter '{valor}' na coluna '{coluna}'. Substituído por 0.");
                                        valor = "0";
                                    }
                                }
                            }
                            else if (tipoColuna != null && (tipoColuna.Contains("date") || tipoColuna.Contains("datetime")))
                            {
                                valor = DateTime.TryParse(valor, out DateTime dataConvertida) ? $"'{dataConvertida:yyyy-MM-dd HH:mm:ss}'" : "NULL";
                            }
                            else
                            {
                                valor = string.IsNullOrEmpty(valor) || valor.ToLower() == "null" ? "NULL" : $"'{valor.Replace("'", "''")}'";
                            }

                            values.Add(valor);
                        }

                        foreach (var colunaExtra in colunasExtrasMySQL)
                        {
                            values.Add("NULL");
                        }

                        batchInserts.Add($"({string.Join(", ", values)})");
                        totalInseridos++;

                        if (batchInserts.Count >= batchSize)
                        {
                            InserirLoteNoBanco(tabelaMySQL, colunasValidas, colunasExtrasMySQL, batchInserts, conn);
                            batchInserts.Clear();

                            // 🔹 Log após cada batch inserido
                            //LogService.RegistrarLog("SUCCESS", $"✅ {batchSize} registros inseridos na tabela {tabela}.");
                        }
                    }

                    if (batchInserts.Count > 0)
                    {
                        InserirLoteNoBanco(tabelaMySQL, colunasValidas, colunasExtrasMySQL, batchInserts, conn);
                        //LogService.RegistrarLog("SUCCESS", $"✅ Últimos {batchInserts.Count} registros inseridos na tabela {tabela}.");
                    }

                    LogService.RegistrarLog("SUCCESS", $"🎉 Total de {totalInseridos} registros inseridos na tabela {tabela}.");
                }
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog("ERROR", $"❌ Erro ao inserir dados na tabela {tabela}: {ex.Message}");
            }
        }

        /// <summary>
        /// Método para inserir um lote no banco de dados
        /// </summary>
        private void InserirLoteNoBanco(string tabela, List<string> colunasValidas, List<string> colunasExtrasMySQL, List<string> batchInserts, MySqlConnection conn)
        {
            try
            {
                string query = $"INSERT INTO `{tabela}` ({string.Join(", ", colunasValidas.Concat(colunasExtrasMySQL))}) VALUES {string.Join(", ", batchInserts)};";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog("ERROR", $"❌ Erro ao inserir lote na tabela {tabela}: {ex.Message}");
            }
        }

        public bool ColunaExiste(string tabela, string coluna)
        {
            bool existe = false;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $@"
                        SELECT COUNT(*) 
                        FROM INFORMATION_SCHEMA.COLUMNS 
                        WHERE TABLE_SCHEMA = '{_banco}' 
                        AND TABLE_NAME = '{tabela}' 
                        AND COLUMN_NAME = '{coluna}'";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar coluna '{coluna}' na tabela {tabela} no MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return existe;
        }

        public void AtualizarEnviado(string tabela)
        {
            try
            {
                if (!ColunaExiste(tabela, "Enviado"))
                {
                    Console.WriteLine($"⚠️ A coluna 'Enviado' não existe na tabela {tabela}. Nenhuma atualização foi feita.");
                    return;
                }

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"UPDATE `{tabela}` SET enviado = 1 WHERE enviado = 0";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        int registrosAtualizados = cmd.ExecuteNonQuery();
                        Console.WriteLine($"✅ {registrosAtualizados} registros foram marcados como 'Enviado' na tabela {tabela} do MySQL.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar coluna 'Enviado' na tabela {tabela} no MySQL: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
