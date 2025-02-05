using BlackSync.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackSync.Forms
{
    public partial class FormEstrutura : Form
    {
        private readonly MySQLService _mySQLService;
        private readonly FirebirdService _firebirdService;
        
        public FormEstrutura(string mysqlServer, string mysqlDatabase, string mysqlUser, string mysqlPassword, string firebirdDSN)
        {
            InitializeComponent();
            _mySQLService = new MySQLService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword);
            _firebirdService = new FirebirdService(firebirdDSN);
            CarregarTabelas();
        }

        /// <summary>
        /// Carrega as tabelas do Firebird e MySQL no CheckedListBox
        /// </summary>

        private void CarregarTabelas()
        {
            try
            {
                // Carregar tabelas do Firebird
                var tabelasFirebird = _firebirdService.GetTabelasFirebird();
                clbTabelasFirebird.Items.Clear();
                foreach (var tabela in tabelasFirebird)
                {
                    clbTabelasFirebird.Items.Add(tabela, false);
                }

                // Carregar tabelas do MySQL
                var tabelasMySQL = _mySQLService.GetTabelasMySQL();
                clbTabelasMySQL.Items.Clear();
                foreach (var tabela in tabelasMySQL)
                {
                    clbTabelasMySQL.Items.Add(tabela, false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtém a lista de tabelas selecionadas no CheckedListBox do Firebird
        /// </summary>
        
        private List<string> ObterTabelasSelecionadas()
        {
            return clbTabelasFirebird.CheckedItems.Cast<string>().ToList();
        }

        private void btnVerificarEstrutura_Click(object sender, EventArgs e)
        {
            var tabelasSelecionadas = ObterTabelasSelecionadas();

            if (tabelasSelecionadas.Count == 0)
            {
                MessageBox.Show("Por favor, selecione ao menos uma tabela para verificar a estrutura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CompararEstruturaTabelas(tabelasSelecionadas);
        }

        /// <summary>
        /// Compara a estrutura das tabelas selecionadas no Firebird e MySQL.
        /// </summary>
        private void CompararEstruturaTabelas(List<string> tabelasSelecionadas)
        {
            gridEstrutura.Rows.Clear();

            // Verifica se as colunas já existem, caso contrário, adiciona
            if (gridEstrutura.Columns.Count == 0)
            {
                gridEstrutura.Columns.Add("Tabela", "Tabela");
                gridEstrutura.Columns.Add("Status", "Status");
            }

            foreach (var tabela in tabelasSelecionadas)
            {
                var estruturaFirebird = _firebirdService.ObterEstruturaTabela(tabela);
                var estruturaMySQL = _mySQLService.ObterEstruturaTabela(tabela.ToLower()); // MySQL usa nomes minúsculos

                if (!EstruturasSaoIguais(estruturaFirebird, estruturaMySQL))
                {
                    gridEstrutura.Rows.Add(tabela, "Divergente");
                }
            }

            if (gridEstrutura.Rows.Count == 0)
            {
                MessageBox.Show("Todas as tabelas selecionadas possuem a mesma estrutura no Firebird e MySQL.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Compara as estruturas de duas tabelas e retorna true se forem idênticas.
        /// </summary>
        private bool EstruturasSaoIguais(List<string> estruturaFirebird, List<string> estruturaMySQL)
        {
            // Normalizar as estruturas para evitar diferenças irrelevantes
            var normalizadaFirebird = estruturaFirebird
                .Select(coluna => NormalizarTipo(coluna))
                .ToList();

            var normalizadaMySQL = estruturaMySQL
                .Select(coluna => NormalizarTipo(coluna))
                .ToList();

            // Ordenar para evitar divergências causadas pela ordem das colunas
            normalizadaFirebird.Sort();
            normalizadaMySQL.Sort();

            // Verificar se são equivalentes
            bool saoIguais = normalizadaFirebird.SequenceEqual(normalizadaMySQL);

            // Log para depuração
            if (!saoIguais)
            {
                Console.WriteLine("\n--- DIFERENÇAS ENCONTRADAS ---");

                Console.WriteLine("\n🔥 Firebird:");
                foreach (var col in normalizadaFirebird) Console.WriteLine($" - {col}");

                Console.WriteLine("\n🐬 MySQL:");
                foreach (var col in normalizadaMySQL) Console.WriteLine($" - {col}");

                Console.WriteLine("\n🔍 Comparação Direta:");
                for (int i = 0; i < Math.Max(normalizadaFirebird.Count, normalizadaMySQL.Count); i++)
                {
                    string fb = i < normalizadaFirebird.Count ? normalizadaFirebird[i] : "N/A";
                    string my = i < normalizadaMySQL.Count ? normalizadaMySQL[i] : "N/A";
                    Console.WriteLine($"Firebird: {fb} | MySQL: {my}");
                }
            }

            return saoIguais;
        }

        private string NormalizarTipo(string tipo)
        {
            tipo = tipo.ToUpper().Trim();

            // Remover tamanhos e detalhes que não influenciam na comparação
            if (tipo.StartsWith("BIGINT")) return "BIGINT";
            if (tipo.StartsWith("INT")) return "INT";
            if (tipo.StartsWith("INTEGER")) return "INT";
            if (tipo.StartsWith("SMALLINT")) return "SMALLINT";
            if (tipo.StartsWith("DECIMAL") || tipo.StartsWith("NUMERIC")) return "DECIMAL(15,2)";
            if (tipo.StartsWith("FLOAT") || tipo.StartsWith("DOUBLE")) return "FLOAT";
            if (tipo.StartsWith("VARCHAR")) return "VARCHAR";
            if (tipo.StartsWith("CHAR")) return "CHAR";
            if (tipo.StartsWith("DATE")) return "DATE";
            if (tipo.StartsWith("DATETIME") || tipo.StartsWith("TIMESTAMP")) return "DATETIME";

            return tipo;
        }


    }
}
