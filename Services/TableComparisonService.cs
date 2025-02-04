using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSync.Services
{
    public class TableComparisonService
    {
        private string connectionStringMySQL;
        private string connectionStringFirebird;

        public TableComparisonService(string mysqlServer, string mysqlDatabase, string mysqlUser, string mysqlPassword, string firebirdDSN)
        {
            connectionStringMySQL = $"Server={mysqlServer};Database={mysqlDatabase};User={mysqlUser};Password={mysqlPassword};";
            connectionStringFirebird = $"DSN={firebirdDSN};";
        }

        // Método para obter tabelas do MySQL
        public List<string> GetTablesMySQL()
        {
            List<string> tables = new List<string>();

            try
            {
                using (var conn = new MySqlConnection(connectionStringMySQL))
                {
                    conn.Open();
                    string query = "SHOW TABLES;";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter tabelas do MySQL: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tables;
        }

        // Método para obter tabelas do Firebird via ODBC
        public List<string> GetTablesFirebird()
        {
            List<string> tables = new List<string>();

            try
            {
                using (var conn = new OdbcConnection(connectionStringFirebird))
                {
                    conn.Open();
                    string query = "SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE RDB$VIEW_SOURCE IS NULL AND RDB$SYSTEM_FLAG = 0;";

                    using (var cmd = new OdbcCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0).Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter tabelas do Firebird: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tables;
        }

        // Método para comparar tabelas
        public Dictionary<string, string> CompareTables()
        {
            var mysqlTables = GetTablesMySQL();
            var firebirdTables = GetTablesFirebird();
            var differences = new Dictionary<string, string>();

            foreach (var table in mysqlTables)
            {
                if (!firebirdTables.Contains(table))
                {
                    differences.Add(table, "Falta no Firebird");
                }
            }

            foreach (var table in firebirdTables)
            {
                if (!mysqlTables.Contains(table))
                {
                    differences.Add(table, "Falta no MySQL");
                }
            }

            return differences;
        }
    }
}
