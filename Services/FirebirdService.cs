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
    }
}
