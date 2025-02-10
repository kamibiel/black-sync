using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackSync.Services
{
    internal class FirebirdService
    {
        private string connectionString;

        public FirebirdService(string dsn)
        {
            this.connectionString = $"DSN={dsn};";
        }

        public bool TestarConexao()
        {
            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();

                    // Executa uma consulta simples para validar a conexão
                    using (var cmd = new OdbcCommand("SELECT 1 FROM RDB$DATABASE", conn))
                    {
                        cmd.ExecuteScalar();
                    }

                    MessageBox.Show("Conexão com Firebird via ODBC bem-sucedida!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (OdbcException ex)
            {
                MessageBox.Show($"Erro ao conectar ao Firebird via ODBC: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<string> GetTabelasFirebird()
        {
            List<string> tabelas = new List<string>();

            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE RDB$VIEW_BLR IS NULL ORDER BY RDB$RELATION_NAME";

                    using (var cmd = new OdbcCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tabelas.Add(reader.GetString(0).Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter tabelas do Firebird: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabelas;
        }

        /// <summary>
        /// Converte os códigos dos tipos do Firebird para nomes reais
        /// </summary>
        private string ConverterTipoFirebird(int tipo, int tamanho)
        {
            Dictionary<int, string> tiposFirebird = new Dictionary<int, string>
            {
                { 7, "SMALLINT" },
                { 8, "INT" },
                { 16, "BIGINT" }, // 🔹 Firebird usa 16 para BIGINT, mas MySQL pode tratar como DECIMAL
                { 10, "FLOAT" },
                { 27, "DOUBLE" },
                { 37, $"VARCHAR({tamanho})" },
                { 14, $"CHAR({tamanho})" },
                { 12, "DATE" },
                { 13, "TIME" },
                { 35, "DATETIME" },
                { 261, "LONGTEXT" }
            };

            // 🔹 Se for um tipo desconhecido, assume DECIMAL(15,2) por segurança
            return tiposFirebird.ContainsKey(tipo) ? tiposFirebird[tipo] : "DECIMAL(15,2)";
        }

        /// <summary>
        /// Obtém a estrutura da tabela no Firebird (nomes e tipos das colunas).
        /// Agora retorna uma lista de objetos (Nome, Tipo) para ser compatível com CompararEstrutura().
        /// </summary>
        public List<(string Nome, string Tipo)> ObterEstruturaTabela(string tabela)
        {
            List<(string Nome, string Tipo)> estrutura = new List<(string Nome, string Tipo)>();

            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    string query = $@"
                SELECT 
                    rf.RDB$FIELD_NAME AS COLUNA,
                    f.RDB$FIELD_TYPE AS TIPO,
                    COALESCE(f.RDB$FIELD_LENGTH, 0) AS TAMANHO
                FROM RDB$RELATION_FIELDS rf
                JOIN RDB$FIELDS f ON rf.RDB$FIELD_SOURCE = f.RDB$FIELD_NAME
                WHERE rf.RDB$RELATION_NAME = '{tabela}'
                ORDER BY rf.RDB$FIELD_POSITION";

                    using (var cmd = new OdbcCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string coluna = reader["COLUNA"].ToString().Trim();
                            int tipo = Convert.ToInt32(reader["TIPO"]);
                            int tamanho = Convert.ToInt32(reader["TAMANHO"]);

                            string tipoConvertido = ConverterTipoFirebird(tipo, tamanho);

                            estrutura.Add((coluna, tipoConvertido));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter estrutura da tabela {tabela} no Firebird: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return estrutura;
        }

        /// <summary>
        /// Método que compara a estrutura das tabelas do Firebird e MySQL
        /// </summary>
        public List<(string Nome, string Tipo)> CompararEstrutura(string tabela, MySQLService mySQLService)
        {
            var estruturaFirebird = ObterEstruturaTabela(tabela);
            var estruturaMySQL = mySQLService.ObterEstruturaTabela(tabela.ToLower());

            var colunasMySQL = estruturaMySQL.Select(e => e.Nome.ToUpper()).ToList();

            // Filtrar colunas que existem no Firebird, mas não no MySQL
            var colunasFaltantes = estruturaFirebird.Where(e => !colunasMySQL.Contains(e.Nome.ToUpper())).ToList();

            return colunasFaltantes;
        }
    }
}
