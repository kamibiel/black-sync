﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

namespace BlackSync.Services
{
    public static class ConfigService
    {
        private static readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

        public static bool ExisteConfiguracao()
        {
            return File.Exists(configFilePath);
        }

        public static void SalvarConfiguracaoMySQL(string servidor, string banco, string usuario, string senha)
        {
            string[] linhas =
            {
                "[MySQL]",
                $"Servidor={servidor}",
                $"Banco={banco}",
                $"Usuario={usuario}",
                $"Senha={senha}"
            };

            SalvarArquivo(linhas);
        }

        public static void SalvarConfiguracaoFirebird(string dsn)
        {
            string[] linhas =
            {
                "[Firebird]",
                $"DSN={dsn}",
            };

            SalvarArquivo(linhas);
        }

        public static void SalvarArquivo(string[] novasLinhas, bool exibirMensagem = false)
        {
            try
            {
                // Se o arquivo já existe, carregamos as linhas atuais para preservá-las
                List<string> linhas = File.Exists(configFilePath) ? File.ReadAllLines(configFilePath).ToList() : new List<string>();

                // Identifica a seção atual que está sendo salva (MySQL ou Firebird)
                string secaoAtual = novasLinhas[0]; // Exemplo: "[MySQL]" ou "[Firebird]"
                int indiceSecao = linhas.FindIndex(l => l == secaoAtual);

                if (indiceSecao != -1)
                {
                    // Encontramos a seção, então removemos todas as suas linhas até encontrar outra seção
                    int i = indiceSecao;
                    while (i < linhas.Count && (!linhas[i].StartsWith("[") || i == indiceSecao))
                    {
                        linhas.RemoveAt(i);
                    }
                }

                // Adiciona a nova configuração ao final do arquivo
                linhas.AddRange(novasLinhas);

                // Escreve todas as linhas atualizadas no arquivo
                File.WriteAllLines(configFilePath, linhas);

                // Exibir mensagem apenas se for solicitado
                if (exibirMensagem)
                {
                    MessageBox.Show($"Configuração salva em: {configFilePath}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar configuração: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string CarregarConfiguracaoFirebird()
        {
            if (!File.Exists(configFilePath))
                return "";

            string dsn = "";
            bool lendoFirebird = false;

            foreach (string linha in File.ReadAllLines(configFilePath))
            {
                if (linha.StartsWith("[Firebird]")) lendoFirebird = true;
                else if (linha.StartsWith("[")) lendoFirebird = false;

                if (!lendoFirebird || linha.StartsWith("[")) continue;

                var partes = linha.Split('=');
                if (partes.Length < 2) continue;

                if (partes[0] == "DSN") dsn = partes[1];
            }

            return dsn;
        }

        public static (string servidor, string banco, string usuario, string senha) CarregarConfiguracaoMySQL()
        {
            if (!File.Exists(configFilePath))
                return ("", "", "", "");

            string servidor = "", banco = "", usuario = "", senha = "";
            bool lendoMySQL = false;

            foreach (string linha in File.ReadAllLines(configFilePath))
            {
                if (linha.StartsWith("[MySQL]")) lendoMySQL = true;
                else if (linha.StartsWith("[")) lendoMySQL = false;

                if (!lendoMySQL || linha.StartsWith("[")) continue;

                var partes = linha.Split('=');
                if (partes.Length < 2) continue;

                switch (partes[0])
                {
                    case "Servidor": servidor = partes[1]; break;
                    case "Banco": banco = partes[1]; break;
                    case "Usuario": usuario = partes[1]; break;
                    case "Senha": senha = partes[1]; break;
                }
            }

            return (servidor, banco, usuario, senha);
        }
    }
}
