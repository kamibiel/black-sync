﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                            tabelas.Add(reader.GetString(0));
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
        public List<string> ObterEstruturaTabela(string tabela)
        {
            List<string> estrutura = new List<string>();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $@"
                SELECT COLUMN_NAME, DATA_TYPE, COALESCE(CHARACTER_MAXIMUM_LENGTH, 0) AS TAMANHO
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = '{_banco}' AND TABLE_NAME = '{tabela}'";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string coluna = reader["COLUMN_NAME"].ToString();
                            string tipo = reader["DATA_TYPE"].ToString();
                            string tamanho = reader["TAMANHO"].ToString();
                            estrutura.Add($"{coluna} {tipo}({tamanho})");
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

    }
}
