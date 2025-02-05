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
                    string query = "SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE RDB$VIEW_SOURCE IS NULL AND RDB$SYSTEM_FLAG = 0;";

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
        /// Obtém a estrutura da tabela no Firebird (nomes e tipos das colunas).
        /// </summary>
        public List<string> ObterEstruturaTabela(string tabela)
        {
            List<string> estrutura = new List<string>();

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
                            string tipo = reader["TIPO"].ToString();
                            string tamanho = reader["TAMANHO"].ToString();
                            estrutura.Add($"{coluna} {tipo}({tamanho})");
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


    }
}
