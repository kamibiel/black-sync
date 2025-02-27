using BlackSync.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlackSync.Forms
{
    public partial class FormGestaoBancoDados : Form
    {
        private readonly MySQLService _mySQLService;
        private readonly FirebirdService _firebirdService;
        private readonly FormPrincipal _formPrincipal;

        public FormGestaoBancoDados(
            FormPrincipal formprincipal,
            string mysqlServer,
            string mysqlDatabase,
            string mysqlUser,
            string mysqlPassword,
            string firebirdDSN)
        {
            InitializeComponent();
            _formPrincipal = formprincipal ?? throw new ArgumentNullException(nameof(formprincipal));

            _mySQLService = new MySQLService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword);
            _firebirdService = new FirebirdService(firebirdDSN);

            CarregarTabelas();
        }

        public MySQLService ObterMySQLService() => _mySQLService;
        public FirebirdService ObterFirebirdService() => _firebirdService;

        // Carrega as tabelas do banco de dados do Firebird e MySQL        
        private void CarregarTabelas()
        {
            LogService.RegistrarLog(
                "INFO",
                $"🔄 Carregando as tabelas dos bancos de dados."
            );

            try
            {
                var tabelasFirebird = _firebirdService.GetTabelasFirebird();
                clbTabelasFirebird.Items.Clear();

                foreach (var tabela in tabelasFirebird)
                    clbTabelasFirebird.Items.Add(tabela, false);

                LogService.RegistrarLog(
                    "SUCCESS",
                    $"✅ Tabelas do banco Firebird carregadas com sucesso."
                );

                var tabelasMySQL = _mySQLService.GetTabelasMySQL();
                clbTabelasMySQL.Items.Clear();

                foreach (var tabela in tabelasMySQL)
                    clbTabelasMySQL.Items.Add(tabela, false);

                LogService.RegistrarLog(
                    "SUCCESS",
                    $"✅ Tabelas do banco MySQL carregadas com sucesso."
                );
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog(
                    "ERROR",
                    $"Erro ao carregar tabelas: {ex.Message}"
                );

                MessageBox.Show(
                    $"Erro ao carregar tabelas: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private List<string> ObterTabelasPorCategoria(List<string> categorias)
        {
            Dictionary<string, List<string>> tabelasPorCategoria = new Dictionary<string, List<string>>()
            {
                { "Estoque", new List<string> {"movestoque", "nfentrada", "itemnfentrada" } },
                { "Financeiro", new List<string> {"baixapagar", "baixareceber", "contacartao", "pagar", "receber"} },
                { "Vendas", new List<string> {"abrecaixa", "caixa", "itensnf", "itenspedidovenda", "notafiscal", "pedidosvenda" } }
            };

            List<string> tabelasSelecionadas = new List<string>();

            foreach (var categoria in categorias)
            {
                if (tabelasPorCategoria.ContainsKey(categoria))
                {
                    tabelasSelecionadas.AddRange(tabelasPorCategoria[categoria]);
                }
            }

            return tabelasSelecionadas.Distinct().ToList();
        }

        private async void btnReabrirDados_Click(object sender, EventArgs e)
        {
            try
            {
                LogService.RegistrarLog(
                    "INFO",
                    "🔄 Iniciando o processo de reabertura"
                );

                // Pega o período selecionado
                DateTime dataInicio = dtDe.Value;
                DateTime dataFim = dtAte.Value;

                LogService.RegistrarLog(
                    "INFO",
                    $"📤 Foi selecionado o perído De: {dataInicio} até: {dataFim}."
                );

                // Verifica quais tipos de dados foram marcados
                List<string> categoriasSelecionadas = new List<string>();
                if (cbEstoque.Checked) categoriasSelecionadas.Add("Estoque");
                if (cbFinanceiro.Checked) categoriasSelecionadas.Add("Financeiro");
                if (cbVendas.Checked) categoriasSelecionadas.Add("Vendas");

                if (categoriasSelecionadas.Count == 0)
                {
                    MessageBox.Show(
                        "Selecione ao menos um tipo de dado (Estoque, Financeiro, Vendas).",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        $"⚠️ Nenhuma categoria selecionada."
                    );

                    return;
                }

                // Obtém as tabelas específicas para cada categoria
                List<string> tabelasParaReabrir = ObterTabelasPorCategoria(categoriasSelecionadas);

                LogService.RegistrarLog(
                    "INFO",
                    $"🔄 Iniciando a reabertura dos dados das tabelas: {tabelasParaReabrir}."
                );

                if (tabelasParaReabrir.Count == 0)
                {
                    MessageBox.Show(
                        "Nenhuma tabela foi selecionada para reabertura de movimento.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        "⚠️ Nenhuma tabela foi selecionada para reabertura de movimento."
                    );

                    return;
                }

                // Verifica qual banco foi selecionado
                string bancoSelecionado = cbBanco.SelectedItem.ToString();
                LogService.RegistrarLog(
                    "INFO",
                    $"📤 Foi selecionado o banco de dados: {bancoSelecionado}"
                );

                // Iniciar barra de progresso
                pbGestao.Minimum = 0;
                pbGestao.Maximum = tabelasParaAtualizarFilial.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;

                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a reabertura dos dados das tabelas: {tabelasParaReabrir} para o banco de dados: {bancoSelecionado}."
                    );

                    // Executar a atualização das tabelas de forma assíncrona
                    await Task.Run(() => {
                        foreach(string tabela in tabelasParaReabrir)
                        {
                            _firebirdService.ReabrirMovimentoFirebird(tabela, dataInicio, dataFim);

                            // Atualizar a barra de progresso na UI Thread
                            this.Invoke(new Action(() =>
                            {
                                pbGestao.PerformStep();
                            }));
                        }
                    });

                    LogService.RegistrarLog(
                        "SUCCESS",
                        $"$🚀 Finalizado a rebertura dos dados das tabelas: {tabelasParaReabrir} com sucesso!"
                    );
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a reabertura dos movimentos das tabelas: {tabelasParaReabrir} para o banco de dados: {bancoSelecionado}."
                    );

                    // Executar a atualização das tabelas de forma assíncrona
                    await Task.Run (() => {
                        foreach(string tabela in tabelasParaReabrir)
                        {
                            _mySQLService.ReabrirMovimentoMySQL(tabela, dataInicio, dataFim);

                            // Atualizar a barra de progresso na UI Thread
                            this.Invoke(new Action (() => {
                                pbGestao.PerformStep();
                            }));
                        }
                    });

                    LogService.RegistrarLog(
                        "SUCCESS",
                        $"$🚀 Finalizado a rebertura dos movimentos das tabelas: {tabelasParaReabrir} com sucesso!"
                    );
                }

                MessageBox.Show(
                    "Movimento reaberto com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog(
                    "ERROR",
                    $"Erro ao reabrir movimento: {ex.Message}"
                );
                MessageBox.Show(
                    $"Erro ao reabrir movimento: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnFecharDados_Click(object sender, EventArgs e)
        {
            try
            {
                LogService.RegistrarLog(
                    "INFO",
                    "🔄 Iniciando o processo de fechamento dos movimentos"
                );

                // Pega o período selecionado
                DateTime dataInicio = dtDe.Value;
                DateTime dataFim = dtAte.Value;

                LogService.RegistrarLog(
                    "INFO",
                    $"📤 Foi selecionado o período De: {dataInicio} até: {dataFim}."
                );

                // Verifica quais tipos de dados foram marcados
                List<string> categoriasSelecionadas = new List<string>();
                if (cbEstoque.Checked) categoriasSelecionadas.Add("Estoque");
                if (cbFinanceiro.Checked) categoriasSelecionadas.Add("Financeiro");
                if (cbVendas.Checked) categoriasSelecionadas.Add("Vendas");

                if (categoriasSelecionadas.Count == 0)
                {
                    MessageBox.Show(
                        "Selecione ao menos um tipo de dado (Estoque, Financeiro, Vendas).",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        $"⚠️ Nenhuma categoria selecionada."
                    );

                    return;
                }

                // Obtém as tabelas específicas para cada categoria
                List<string> tabelasParaFechar = ObterTabelasPorCategoria(categoriasSelecionadas);

                LogService.RegistrarLog(
                    "INFO",
                    $"🔄 Iniciando o fechamento dos movimentos das tabelas: {tabelasParaFechar}."
                );

                if (tabelasParaFechar.Count == 0)
                {
                    MessageBox.Show(
                        "Nenhuma tabela foi selecionada para reabertura de movimento.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        "⚠️ Nenhuma tabela foi selecionada para o fechamento dos movimentos."
                    );

                    return;
                }

                // Verifica qual banco foi selecionado
                string bancoSelecionado = cbBanco.SelectedItem.ToString();

                LogService.RegistrarLog(
                    "INFO",
                    $"📤 Foi selecionado o banco de dados: {bancoSelecionado}"
                );

                // Iniciar barra de progresso
                pbGestao.Minimum = 0;
                pbGestao.Maximum = tabelasParaAtualizarFilial.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;                

                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando o fechamento dos movimentos das tabelas: {tabelasParaFechar} para o banco de dados: {bancoSelecionado}."
                    );

                    // Executar a atualização das tabelas de forma assíncrona
                    await Task.Run(() => {
                        foreach(string tabela in tabelasParaFechar)
                        {
                            _firebirdService.FecharMovimentoFirebird(tabela, dataInicio, dataFim);

                            // Atualizar a barra de progresso na UI Thread
                            this.Invoke(new Action(() => {
                                pbGestao.PerformStep();
                            }));
                        }
                    });

                    LogService.RegistrarLog(
                        "SUCCESS",
                        $"$🚀 Finalizado o fechamento dos movimentos das tabelas: {tabelasParaFechar} com sucesso!"
                    );
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando o fechamento dos movimentos das tabelas: {tabelasParaFechar} para o banco de dados: {bancoSelecionado}."
                    );

                    // Executar a atualização das tabelas de forma assíncrona
                    await Task.Run(() => {
                        foreach(string tabela in tabelasParaFechar)
                        {
                            _mySQLService.FecharMovimentoMySQL(tabela, dataInicio, dataFim);

                            // Atualizar a barra de progresso na UI Thread
                            this.Invoke(new Action(() =>
                            {
                                pbGestao.PerformStep();
                            }));
                        }
                    });

                    LogService.RegistrarLog(
                        "SUCCESS",
                        $"$🚀 Finalizado o fechamento dos movimentos das tabelas: {tabelasParaFechar} com sucesso!"
                    );
                }

                MessageBox.Show(
                    "Movimento fechado com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog(
                    "ERROR",
                    $"Erro ao fechar movimento: {ex.Message}"
                );
                MessageBox.Show(
                    $"Erro ao fechar movimento: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async Task btnExcluirDados_Click(object sender, EventArgs e)
        {
            try
            {
                LogService.RegistrarLog(
                    "INFO",
                    "🔄 Iniciando o processo de exclusão dos movimentos"
                );

                // Pega o período selecionado
                DateTime dataInicio = dtDe.Value;
                DateTime dataFim = dtAte.Value;

                LogService.RegistrarLog(
                    "INFO",
                    $"📤 Foi selecionado o perído De: {dataInicio} até {dataFim}."
                );

                // Verifique quais tipos de dados foram marcados
                List<string> categoriasSelecionadas = new List<string>();
                if (cbEstoque.Checked) categoriasSelecionadas.Add("Estoque");
                if (cbFinanceiro.Checked) categoriasSelecionadas.Add("Financeiro");
                if (cbVendas.Checked) categoriasSelecionadas.Add("Vendas");

                if (categoriasSelecionadas.Count == 0)
                {
                    MessageBox.Show(
                        "Selecione ao menos um tipo de dado (Estoque, Financieiro, Vendas).",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        $"⚠️ Nenhuma categoria selecionada."
                    );

                    return;
                }

                // Obtém as tabelas específicas para cada categoria
                List<string> tabelasParaExcluir = ObterTabelasPorCategoria(categoriasSelecionadas);

                LogService.RegistrarLog(
                    "INFO",
                    $"🔄 Iniciando a exclusão dos movimentos na tabelas: {tabelasParaExcluir}"
                );

                if (tabelasParaExcluir.Count == 0)
                {
                    MessageBox.Show(
                        "Nenhuma tabela foi selecionada para exclusão dos movimentos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        "⚠️ Nenhuma tabela foi selecionada para a exclusão dos movimentos."
                    );

                    return;
                }

                // Verifica qual banco foi selecionado
                string bancoSelecionado = cbBanco.SelectedItem.ToString();

                LogService.RegistrarLog(
                    "INFO",
                    "📤 Foi selecionado o banco de dados: {bancoSelecionado}"
                );

                // Iniciar barra de progresso
                pbGestao.Minimum = 0;
                pbGestao.Maximum = tabelasParaAtualizarFilial.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;                

                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a exclusão dos movimentos das tabelas: {tabelasParaExcluir} para o banco de dados: {bancoSelecionado}."
                    );

                    // Executar a atualização das tabelas de forma assíncrona
                    await Task.Run(() =>
                    {
                        // Executa a exclusão no Firebird
                        foreach (string tabela in tabelasParaExcluir)
                        {
                            _firebirdService.ExcluirMovimentoFirebird(tabela, dataInicio, dataFim);

                            // Atualizar a barra de progresso na UI Thread
                            this.Invoke(new Action(() =>
                            {
                                pbGestao.PerformStep();
                            }));
                        }
                    });    

                    LogService.RegistrarLog(
                        "SUCCESS",
                        $"🚀 Finalizado a exclusão dos movimentos das tabelas: {tabelasParaExcluir} com sucesso!"
                    );
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a exclusão dos movimentos das tabelas: {tabelasParaExcluir} para o banco de dados: {bancoSelecionado}."
                    );

                    await Task.Run(()=>{
                        
                        // Executa a exclusão no MySQL
                        foreach(string tabela in tabelasParaExcluir)
                        {
                            _mySQLService.ExcluirMovimentoMySQL(tabela, dataInicio, dataFim);

                            // Atualizar a barra de progresso na UI Thread
                            this.Invoke(new Action(() =>
                            {
                                pbGestao.PerformStep();
                            }));
                        }
                    });

                    LogService.RegistrarLog(
                        "SUCCESS",
                        $"🚀 Finalizado a exclusão dos movimentos das tabelas: {tabelasParaExcluir} com sucesso!"
                    );
                }

                MessageBox.Show(
                    "Movimento excluído com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

            }
            catch (Exception ex)
            {
                LogService.RegistrarLog(
                    "ERRO",
                    $"Erro ao excluir os movimentos: {ex.Message}"
                );
                MessageBox.Show(
                    $"Erro ao excluir os movimentos: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnAtualizarFilial_Click(object sender, EventArgs e)
        {
            try
            {
                LogService.RegistrarLog(
                    "INFO",
                    "🔄 Iniciando o processo de atualização da filial."
                );

                // Pega a numeração da filial
                int xFilial = (int)nFilial.Value;

                if (xFilial <= 0)
                {
                    MessageBox.Show(
                        "Por favor, selecione um número de filial válido maior que zero.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog("INFO", "⚠️ Nenhuma filial válida foi informada.");
                    return;
                }

                LogService.RegistrarLog(
                    "INFO",
                    $"📌 Número da filial selecionada: {xFilial}."
                );

                // Verifique quais tipos de dados foram marcados
                List<string> categoriasSelecionadas = new List<string>();
                if (cbEstoque.Checked) categoriasSelecionadas.Add("Estoque");
                if (cbFinanceiro.Checked) categoriasSelecionadas.Add("Financeiro");
                if (cbVendas.Checked) categoriasSelecionadas.Add("Vendas");

                if (categoriasSelecionadas.Count == 0)
                {
                    MessageBox.Show(
                        "Selecione ao menos um tipo de dados (Estoque, Financeiro, Vendas).",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        $"⚠️ Nenhuma categoria selecionada."
                    );

                    return;
                }

                // Obtém as tabelas especificas para cada categoria
                List<string> tabelasParaAtualizarFilial = ObterTabelasPorCategoria(categoriasSelecionadas);

                if (tabelasParaAtualizarFilial.Count == 0)
                {
                    MessageBox.Show(
                        "Nenhuma tabela foi selecionada para atualização da filial.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        "⚠️ Nenhuma tabela foi selecionada para atualização da filial."
                    );

                    return;
                }

                LogService.RegistrarLog(
                    "INFO",
                    $"🔄 Tabelas selecionadas para atualização da filial: {string.Join(", ", tabelasParaAtualizarFilial)}."
                );

                // Verifica qual banco foi selecionado
                string bancoSelecionado = cbBanco.SelectedItem?.ToString() ?? "Nenhum";

                if (bancoSelecionado != "Firebird")
                {
                    MessageBox.Show(
                        "O banco de dados selecionado não é válido para esta ação. Escolha o Firebird.",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    LogService.RegistrarLog(
                        "INFO",
                        $"⚠️ Banco selecionado inválido: {bancoSelecionado}."
                    );

                    return;
                }

                // Iniciar barra de progresso
                pbGestao.Minimum = 0;
                pbGestao.Maximum = tabelasParaAtualizarFilial.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;

                // Atualiza a filial nas tabelas do Firebird
                LogService.RegistrarLog(
                    "INFO",
                    $"🔄 Atualizando filial nas tabelas do Firebird."
                );

                // Executar a atualização das tabelas de forma assíncrona
                await Task.Run(() =>
                {
                    foreach (string tabela in tabelasParaAtualizarFilial)
                    {
                        _firebirdService.AtualizarFilialFirebird(tabela, xFilial);

                        // Atualizar a barra de progresso na UI Thread
                        this.Invoke(new Action(() =>
                        {
                            pbGestao.PerformStep();
                        }));
                    }
                });

                LogService.RegistrarLog(
                    "SUCCESS",
                    $"✅ Atualização da filial concluída para as tabelas: {string.Join(", ", tabelasParaAtualizarFilial)}."
                );

                MessageBox.Show(
                    "Atualização da filial concluída com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog(
                    "ERRO",
                    $"❌ Erro ao atualizar a filial: {ex.Message}"
                );

                MessageBox.Show(
                    $"Ocorreu um erro ao atualizar a filial: {Environment.NewLine}{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnExportarBanco_Click(object sender, EventArgs e)
        {

        }
    }
}
