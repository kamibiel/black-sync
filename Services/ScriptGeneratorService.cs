using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSync.Services
{
    public static class ScriptGeneratorService
    {
        // Método para gerar script de criação de tabela no MySQL
        public static string GerarScriptMySQL(string tabela, string servidor, string banco, string usuario, string senha, string firebirdDSN)
        {
            // Gerar o script baseado na estrutura do Firebird
            string scriptFirebird = GerarScriptFirebird(tabela, firebirdDSN);

            if (string.IsNullOrWhiteSpace(scriptFirebird))
                throw new Exception($"Não foi possível obter a estrutura da tabela {tabela} no Firebird.");

            // Converter os tipos do Firebird para MySQL
            string scriptMySQL = ConverterScriptFirebirdParaMySQL(scriptFirebird);

            return scriptMySQL;
        }

        private static string ConverterScriptFirebirdParaMySQL(string scriptFirebird)
        {
            Dictionary<string, string> conversaoTipos = new Dictionary<string, string>
            {
                { "VARCHAR", "VARCHAR" },
                { "INTEGER", "INT" },
                { "SMALLINT", "SMALLINT" },
                { "BIGINT", "BIGINT" },
                { "DECIMAL", "DECIMAL" },
                { "NUMERIC", "DECIMAL" },
                { "FLOAT", "FLOAT" },
                { "DOUBLE PRECISION", "DOUBLE" },
                { "DATE", "DATE" },
                { "TIME", "TIME" },
                { "TIMESTAMP", "DATETIME" },
                { "BLOB SUB_TYPE 1", "TEXT" }, // Texto grande no Firebird vira TEXT no MySQL
                { "BLOB SUB_TYPE 0", "BLOB" }  // Dados binários
            };

            foreach (var par in conversaoTipos)
                    {
                        scriptFirebird = scriptFirebird.Replace(par.Key, par.Value);
                    }

                    return scriptFirebird;
        }

        public static string GerarScriptFirebird(string tabela, string dsn)
        {
            return $"-- Script de criação para {tabela} no Firebird não implementado.";
        }

        public static string GerarScriptFirebirdParaMySQL(string tabela, string dsn)
        {
            string connectionString = $"DSN={dsn};";
            StringBuilder script = new StringBuilder();

            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                // Consulta para obter as colunas do Firebird
                string query = $@"
            SELECT 
                rf.RDB$FIELD_NAME AS COLUNA,
                f.RDB$FIELD_TYPE AS TIPO,
                COALESCE(f.RDB$FIELD_LENGTH, 0) AS TAMANHO,
                rf.RDB$NULL_FLAG AS NOT_NULL
            FROM RDB$RELATION_FIELDS rf
            JOIN RDB$FIELDS f ON rf.RDB$FIELD_SOURCE = f.RDB$FIELD_NAME
            WHERE rf.RDB$RELATION_NAME = '{tabela}'
            ORDER BY rf.RDB$FIELD_POSITION";

                using (var cmd = new OdbcCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    script.AppendLine($"CREATE TABLE `{tabela}` (");

                    List<string> colunas = new List<string>();

                    while (reader.Read())
                    {
                        string coluna = reader["COLUNA"].ToString().Trim();
                        int tipo = Convert.ToInt32(reader["TIPO"]);
                        int tamanho = Convert.ToInt32(reader["TAMANHO"]);
                        bool notNull = reader["NOT_NULL"] != DBNull.Value;

                        // Converter o tipo Firebird para MySQL
                        string tipoMySQL = ConverterTipoFirebirdParaMySQL(tipo, tamanho);

                        string colunaSQL = $"`{coluna}` {tipoMySQL}";
                        if (notNull)
                            colunaSQL += " NOT NULL";

                        colunas.Add(colunaSQL);
                    }

                    script.AppendLine(string.Join(",\n", colunas));
                    script.AppendLine(");");
                }
            }

            return script.ToString();
        }

        private static string ConverterTipoFirebirdParaMySQL(int tipo, int tamanho)
        {
            Dictionary<int, string> tiposFirebird = new Dictionary<int, string>
            {
                { 7, "SMALLINT" },
                { 8, "INT" },
                { 16, "BIGINT" },
                { 10, "FLOAT" },
                { 27, "DOUBLE" },
                { 37, $"VARCHAR({tamanho})" },
                { 14, $"CHAR({tamanho})" },
                { 12, "DATE" },
                { 13, "TIME" },
                { 35, "DATETIME" },
                { 261, "BLOB" }
            };

            return tiposFirebird.ContainsKey(tipo) ? tiposFirebird[tipo] : "TEXT";
        }
    }
}
