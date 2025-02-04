using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BlackSync.Services
{
    public class MySQLService
    {
        private string connectionString;

        public MySQLService(string servidor, string banco, string usuario, string senha)
        {
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
                MessageBox.Show($"Erro ao conectar ao Firebird: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
