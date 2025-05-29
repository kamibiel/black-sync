using BlackSync.Services;
using Org.BouncyCastle.Asn1.Cmp;
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
using System.Data.OleDb;
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

            //CarregarTabelas();
        }

        public MySQLService ObterMySQLService() => _mySQLService;
        public FirebirdService ObterFirebirdService() => _firebirdService;

        // Carrega as tabelas do banco de dados do Firebird e MySQL 

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
                pbGestao.Maximum = tabelasParaReabrir.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;
                btnAtualizarFilial.Enabled = false;
                btnExcluirDados.Enabled = false;
                btnFecharDados.Enabled = false;
                btnReabrirDados.Enabled = false;
                btnAlterarNumeracaoDocumentos.Enabled = false;
                // btnTruncate.Enabled = false;
                // btnLimpeza.Enabled = false;
                btnExportarBanco.Enabled = false;

                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a reabertura dos dados das tabelas: {tabelasParaReabrir} para o banco de dados: {bancoSelecionado}."
                    );

                    var resposta = MessageBox.Show(
                            $"🔍 Resumo da Operação{Environment.NewLine}" +
                            $"📅 Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}{Environment.NewLine}" +
                            $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                            $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"⚠️ Esta ação reabrirá os movimentos para o período selecionado.{Environment.NewLine}" +
                            $"❗ Deseja realmente continuar?",
                            "Confirmação de Reabertura",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        // Executar a atualização das tabelas de forma assíncrona
                        await Task.Run(() =>
                        {
                            foreach (string tabela in tabelasParaReabrir)
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
                    else
                    {
                        LogService.RegistrarLog(
                            "INFO",
                            $"⚠️ Operação cancelada: Reabertura do movimento não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                        );

                        MessageBox.Show(
                            $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                            $"As seguintes categorias não tiveram seus movimentos reabertos:{Environment.NewLine}" +
                            $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"✅ Nenhuma alteração foi feita.",
                            "Operação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a reabertura dos movimentos das tabelas: {tabelasParaReabrir} para o banco de dados: {bancoSelecionado}."
                    );

                    var resposta = MessageBox.Show(
                            $"🔍 Resumo da Operação{Environment.NewLine}" +
                            $"📅 Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}{Environment.NewLine}" +
                            $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                            $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"⚠️ Esta ação reabrirá os movimentos para o período selecionado.{Environment.NewLine}" +
                            $"❗ Deseja realmente continuar?",
                            "Confirmação de Reabertura",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        // Executar a atualização das tabelas de forma assíncrona
                        await Task.Run(() =>
                        {
                            foreach (string tabela in tabelasParaReabrir)
                            {
                                _mySQLService.ReabrirMovimentoMySQL(tabela, dataInicio, dataFim);

                                // Atualizar a barra de progresso na UI Thread
                                this.Invoke(new Action(() =>
                                {
                                    pbGestao.PerformStep();
                                }));
                            }
                        });

                        LogService.RegistrarLog(
                            "SUCCESS",
                            $"$🚀 Finalizado a rebertura dos movimentos das tabelas: {tabelasParaReabrir} com sucesso!"
                        );
                    }
                    else
                    {
                        LogService.RegistrarLog(
                            "INFO",
                            $"⚠️ Operação cancelada: Reabertura do movimento não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                        );

                        MessageBox.Show(
                            $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                            $"As seguintes categorias não tiveram seus movimentos reabertos:{Environment.NewLine}" +
                            $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"✅ Nenhuma alteração foi feita.",
                            "Operação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                btnAtualizarFilial.Enabled = true;
                btnExcluirDados.Enabled = true;
                btnFecharDados.Enabled = true;
                btnReabrirDados.Enabled = true;
                btnAlterarNumeracaoDocumentos.Enabled = true;
                // btnTruncate.Enabled = true;
                // btnLimpeza.Enabled = true;
                btnExportarBanco.Enabled = true;

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
                    $"❌ Erro ao reabrir movimento: {ex.Message}"
                );
                MessageBox.Show(
                    $"❌ Erro ao reabrir movimento: {ex.Message}",
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
                pbGestao.Maximum = tabelasParaFechar.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;
                btnAtualizarFilial.Enabled = false;
                btnExcluirDados.Enabled = false;
                btnFecharDados.Enabled = false;
                btnReabrirDados.Enabled = false;
                btnAlterarNumeracaoDocumentos.Enabled = false;
                // btnTruncate.Enabled = false;
                // btnLimpeza.Enabled = false;
                btnExportarBanco.Enabled = false;


                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando o fechamento dos movimentos das tabelas: {tabelasParaFechar} para o banco de dados: {bancoSelecionado}."
                    );

                    var resposta = MessageBox.Show(
                            $"🔍 Resumo da Operação{Environment.NewLine}" +
                            $"📅 Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}{Environment.NewLine}" +
                            $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                            $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"⚠️ Esta ação fechará os movimentos para o período selecionado.{Environment.NewLine}" +
                            $"❗ Deseja realmente continuar?",
                            "Confirmação de Fechamento",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        // Executar a atualização das tabelas de forma assíncrona
                        await Task.Run(() =>
                        {
                            foreach (string tabela in tabelasParaFechar)
                            {
                                _firebirdService.FecharMovimentoFirebird(tabela, dataInicio, dataFim);

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
                    else
                    {
                        LogService.RegistrarLog(
                            "INFO",
                            $"⚠️ Operação cancelada: Fechamento do movimento não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                        );

                        MessageBox.Show(
                            $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                            $"As seguintes categorias não tiveram seus movimentos fechados:{Environment.NewLine}" +
                            $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"✅ Nenhuma alteração foi feita.",
                            "Operação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando o fechamento dos movimentos das tabelas: {tabelasParaFechar} para o banco de dados: {bancoSelecionado}."
                    );

                    var resposta = MessageBox.Show(
                            $"🔍 Resumo da Operação{Environment.NewLine}" +
                            $"📅 Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}{Environment.NewLine}" +
                            $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                            $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"⚠️ Esta ação fechar os movimentos para o período selecionado.{Environment.NewLine}" +
                            $"❗ Deseja realmente continuar?",
                            "Confirmação de Fechamento",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        // Executar a atualização das tabelas de forma assíncrona
                        await Task.Run(() =>
                        {
                            foreach (string tabela in tabelasParaFechar)
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
                    else
                    {
                        LogService.RegistrarLog(
                            "INFO",
                            $"⚠️ Operação cancelada: Fechamento do movimento não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                        );

                        MessageBox.Show(
                            $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                            $"As seguintes categorias não tiveram seus movimentos fechados:{Environment.NewLine}" +
                            $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"✅ Nenhuma alteração foi feita.",
                            "Operação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }

                }

                btnAtualizarFilial.Enabled = true;
                btnExcluirDados.Enabled = true;
                btnFecharDados.Enabled = true;
                btnReabrirDados.Enabled = true;
                btnAlterarNumeracaoDocumentos.Enabled = true;
                // btnTruncate.Enabled = true;
                // btnLimpeza.Enabled = true;
                btnExportarBanco.Enabled = true;

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
                    $"❌ Erro ao fechar movimento: {ex.Message}"
                );
                MessageBox.Show(
                    $"❌ Erro ao fechar movimento: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnExcluirDados_Click(object sender, EventArgs e)
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
                pbGestao.Maximum = tabelasParaExcluir.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;
                btnAtualizarFilial.Enabled = false;
                btnExcluirDados.Enabled = false;
                btnFecharDados.Enabled = false;
                btnReabrirDados.Enabled = false;
                btnAlterarNumeracaoDocumentos.Enabled = false;
                // btnTruncate.Enabled = false;
                // btnLimpeza.Enabled = false;
                btnExportarBanco.Enabled = false;

                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a exclusão dos movimentos das tabelas: {tabelasParaExcluir} para o banco de dados: {bancoSelecionado}."
                    );

                    var resposta = MessageBox.Show(
                            $"🔍 Resumo da Operação{Environment.NewLine}" +
                            $"📅 Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}{Environment.NewLine}" +
                            $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                            $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"⚠️ Esta ação excluirá os movimentos para o período selecionado.{Environment.NewLine}" +
                            $"❗ Deseja realmente continuar?",
                            "Confirmação de Exclusão",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
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
                    else
                    {
                        LogService.RegistrarLog(
                           "INFO",
                           $"⚠️ Operação cancelada: Exclusão do movimento não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                        );

                        MessageBox.Show(
                            $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                            $"As seguintes categorias não tiveram seus movimentos excluídos:{Environment.NewLine}" +
                            $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"✅ Nenhuma alteração foi feita.",
                            "Operação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"🔄 Iniciando a exclusão dos movimentos das tabelas: {tabelasParaExcluir} para o banco de dados: {bancoSelecionado}."
                    );

                    var resposta = MessageBox.Show(
                            $"🔍 Resumo da Operação{Environment.NewLine}" +
                            $"📅 Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}{Environment.NewLine}" +
                            $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                            $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"⚠️ Esta ação excluirá os movimentos para o período selecionado.{Environment.NewLine}" +
                            $"❗ Deseja realmente continuar?",
                            "Confirmação de Exclusão",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        // Executar a atualização das tabelas de forma assíncrona
                        await Task.Run(() =>
                        {

                            // Executa a exclusão no MySQL
                            foreach (string tabela in tabelasParaExcluir)
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
                    else
                    {
                        LogService.RegistrarLog(
                           "INFO",
                           $"⚠️ Operação cancelada: Exclusão do movimento não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                        );

                        MessageBox.Show(
                            $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                            $"As seguintes categorias não tiveram seus movimentos excluídos:{Environment.NewLine}" +
                            $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                            $"✅ Nenhuma alteração foi feita.",
                            "Operação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                btnAtualizarFilial.Enabled = true;
                btnExcluirDados.Enabled = true;
                btnFecharDados.Enabled = true;
                btnReabrirDados.Enabled = true;
                btnAlterarNumeracaoDocumentos.Enabled = true;
                // btnTruncate.Enabled = true;
                // btnLimpeza.Enabled = true;
                btnExportarBanco.Enabled = true;

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
                    $"❌ Erro ao excluir os movimentos: {ex.Message}"
                );
                MessageBox.Show(
                    $"❌ Erro ao excluir os movimentos: {ex.Message}",
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
                List<string> tabelasLimpezaBanco = ObterTabelasPorCategoria(categoriasSelecionadas);

                if (tabelasLimpezaBanco.Count == 0)
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
                    $"🔄 Tabelas selecionadas para atualização da filial: {string.Join(", ", tabelasLimpezaBanco)}."
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
                pbGestao.Maximum = tabelasLimpezaBanco.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;
                btnAtualizarFilial.Enabled = false;
                btnExcluirDados.Enabled = false;
                btnFecharDados.Enabled = false;
                btnReabrirDados.Enabled = false;
                btnAlterarNumeracaoDocumentos.Enabled = false;
                // btnTruncate.Enabled = false;
                // btnLimpeza.Enabled = false;
                btnExportarBanco.Enabled = false;

                // Atualiza a filial nas tabelas do Firebird
                LogService.RegistrarLog(
                    "INFO",
                    $"🔄 Atualizando filial nas tabelas do Firebird."
                );

                var resposta = MessageBox.Show(
                        $"🔍 Resumo da Operação{Environment.NewLine}" +
                        $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                        $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                        $"⚠️ Esta ação atualizará a filial para a(s) categoria(s) selecionada(s).{Environment.NewLine}" +
                        $"❗ Deseja realmente continuar?",
                        "Confirmação a Atualização",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                );

                if (resposta == DialogResult.Yes)
                {
                    // Executar a atualização das tabelas de forma assíncrona
                    await Task.Run(() =>
                    {
                        foreach (string tabela in tabelasLimpezaBanco)
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
                        $"✅ Atualização da filial concluída para as tabelas: {string.Join(", ", tabelasLimpezaBanco)}."
                    );
                }
                else
                {
                    LogService.RegistrarLog(
                        "INFO",
                        $"⚠️ Operação cancelada: Atualização da filial não foi realizada para as categorias: {string.Join(", ", categoriasSelecionadas)}."
                    );

                    MessageBox.Show(
                        $"🔄 Ação Cancelada{Environment.NewLine}{Environment.NewLine}" +
                        $"As seguintes categorias não tiveram sua filial atualizada:{Environment.NewLine}" +
                        $"📌 {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                        $"✅ Nenhuma alteração foi feita.",
                        "Operação Cancelada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                btnAtualizarFilial.Enabled = true;
                btnExcluirDados.Enabled = true;
                btnFecharDados.Enabled = true;
                btnReabrirDados.Enabled = true;
                btnAlterarNumeracaoDocumentos.Enabled = true;
                // btnTruncate.Enabled = true;
                // btnLimpeza.Enabled = true;
                btnExportarBanco.Enabled = true;

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

        private async void btnAlterarNumeracaoDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                LogService.RegistrarLog("INFO", "🔄 Iniciando o processo de alterar a numeração dos documentos");

                // Pega os valores da empresa/documento
                int xEmpresa = (int)nEmpresa.Value;
                int yEmpresa = (int)nEmpresaN.Value; // Novo valor da empresa/documento

                if (xEmpresa <= 0 || yEmpresa <= 0)
                {
                    MessageBox.Show(
                        "Por favor, selecione números válidos para a numeração.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog("INFO", "⚠️ Numeração inválida informada.");
                    return;
                }

                LogService.RegistrarLog(
                    "INFO",
                    $"📌 Número da empresa selecionada: {xEmpresa}. Novo número: {yEmpresa}."
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

                    LogService.RegistrarLog("INFO", $"⚠️ Nenhuma categoria selecionada.");
                    return;
                }

                // Obtém as tabelas específicas para cada categoria
                List<string> tabelasParaAlterar = ObterTabelasPorCategoria(categoriasSelecionadas);

                if (tabelasParaAlterar.Count == 0)
                {
                    MessageBox.Show(
                        "Nenhuma tabela foi selecionada para alteração dos documentos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    LogService.RegistrarLog("INFO", "⚠️ Nenhuma tabela foi selecionada para a alteração dos documentos.");
                    return;
                }

                // Verifica qual banco foi selecionado
                string bancoSelecionado = cbBanco.SelectedItem.ToString();
                LogService.RegistrarLog("INFO", $"📤 Banco de dados selecionado: {bancoSelecionado}");

                // Configurar barra de progresso
                pbGestao.Minimum = 0;
                pbGestao.Maximum = tabelasParaAlterar.Count;
                pbGestao.Value = 0;
                pbGestao.Step = 1;

                // Desativar botões durante o processo
                btnAlterarNumeracaoDocumentos.Enabled = false;

                if (bancoSelecionado == "Firebird" || bancoSelecionado == "Ambos")
                {
                    var resposta = MessageBox.Show(
                        $"🔍 Resumo da Operação{Environment.NewLine}" +
                        $"🗄 Banco de Dados: {bancoSelecionado}{Environment.NewLine}" +
                        $"📌 Categorias Selecionadas: {string.Join(", ", categoriasSelecionadas)}{Environment.NewLine}{Environment.NewLine}" +
                        $"⚠️ Esta ação altera todos os documentos dos movimentos.{Environment.NewLine}" +
                        $"❗ Deseja realmente continuar?",
                        "Confirmação de Alteração",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        await Task.Run(() =>
                        {
                            foreach (string tabela in tabelasParaAlterar)
                            {
                                _firebirdService.AlterarDocumentoFirebird(tabela, xEmpresa, yEmpresa);
                                this.Invoke(new Action(() => pbGestao.PerformStep()));
                            }
                        });

                        LogService.RegistrarLog("SUCCESS", $"🚀 Alteração concluída para as tabelas: {tabelasParaAlterar}.");
                    }
                }

                if (bancoSelecionado == "MySQL" || bancoSelecionado == "Ambos")
                {
                    await Task.Run(() =>
                    {
                        foreach (string tabela in tabelasParaAlterar)
                        {
                            _mySQLService.AlterarDocumentoMySQL(tabela, xEmpresa, yEmpresa);
                            this.Invoke(new Action(() => pbGestao.PerformStep()));
                        }
                    });

                    LogService.RegistrarLog("SUCCESS", $"🚀 Alteração concluída para as tabelas: {tabelasParaAlterar}.");
                }

                // Reativar botões
                btnAlterarNumeracaoDocumentos.Enabled = true;

                MessageBox.Show("Alteração concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog("ERROR", $"❌ Erro ao alterar numeração dos documentos: {ex.Message}");
                MessageBox.Show($"Erro ao alterar numeração dos documentos:{Environment.NewLine}{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string caminhoArquivoAccess = null;

        // Ler as tabelas do Access
        private (DataTable dtEtiqueta, DataTable dtModelos) LerTabelasAccess(string caminhoArquivo)
        {
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={caminhoArquivo};";

            DataTable dtEtiqueta = new DataTable();
            DataTable dtModelos = new DataTable();

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string queryEtiqueta = "SELECT * FROM Etiqueta";
                using (var adapterEtiqueta = new OleDbDataAdapter(queryEtiqueta, connection))
                {
                    adapterEtiqueta.Fill(dtEtiqueta);
                }

                string queryModelos = "SELECT * FROM Modelos";
                using (var adapterModelos = new OleDbDataAdapter(queryModelos, connection))
                {
                    adapterModelos.Fill(dtModelos);
                }
            }

            return (dtEtiqueta, dtModelos);
        }

        private async void btnConverterZpl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(caminhoArquivoAccess))
            {
                MessageBox.Show("Selecione o arquivo Access primeiro.");
                return;
            }

            pbGestao.Value = 0;
            pbGestao.Visible = true;
            btnConverterZpl.Enabled = false;

            try
            {
                LogService.RegistrarLog("INFO", "🔄 Iniciando importação do arquivo Access.");

                var (dtEtiqueta, dtModelos) = await Task.Run(() => LerTabelasAccess(caminhoArquivoAccess));

                LogService.RegistrarLog("INFO", $"📊 Etiqueta: {dtEtiqueta.Rows.Count} registros carregados.");
                LogService.RegistrarLog("INFO", $"📊 Modelos: {dtModelos.Rows.Count} registros carregados.");

                // Pergunta para o usuário se quer truncar tabela etiqueta_zpl
                bool truncarEtiqueta = false;
                bool apenasNovosRegistros = false;

                if (_mySQLService.TabelaTemDados("etiqueta_zpl"))
                {
                    var resposta = MessageBox.Show(
                        "A tabela etiqueta_zpl já contém dados no MySQL.\n\n" +
                        "Escolha uma opção:\n\n" +
                        "SIM - Apaga todos os dados antes da inserção\n" +
                        "NÃO - Insere apenas novos registros\n" +
                        "CANCELAR - Cancela a importação",
                        "Dados existentes detectados!",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (resposta == DialogResult.Yes)
                    {
                        truncarEtiqueta = true;
                        _mySQLService.TruncateTabela("etiqueta_zpl");
                        LogService.RegistrarLog("SUCCESS", "🚀 Tabela etiqueta_zpl truncada!");
                    }
                    else if (resposta == DialogResult.No)
                    {
                        apenasNovosRegistros = true;
                    }
                    else
                    {
                        btnConverterZpl.Enabled = true;
                        pbGestao.Visible = false;
                        return;
                    }
                }

                DataTable dtEtiquetaParaInserir;

                if (truncarEtiqueta)
                {
                    dtEtiquetaParaInserir = dtEtiqueta;
                }
                else if (apenasNovosRegistros)
                {
                    var existentes = _mySQLService.ObterCodigosExistentes("etiqueta_zpl", "contador");
                    dtEtiquetaParaInserir = dtEtiqueta.Clone();

                    foreach (DataRow row in dtEtiqueta.Rows)
                    {
                        string contador = row["Contador"].ToString();
                        if (!existentes.Contains(contador))
                        {
                            dtEtiquetaParaInserir.ImportRow(row);
                        }
                    }
                }
                else
                {
                    dtEtiquetaParaInserir = dtEtiqueta;
                }

                // Barra de progresso: somando etiquetas + modelos
                int totalRegistros = dtEtiquetaParaInserir.Rows.Count + dtModelos.Rows.Count;
                pbGestao.Maximum = Math.Max(1, totalRegistros);
                pbGestao.Value = 0;

                // Inserir etiqueta em lote
                if (dtEtiquetaParaInserir.Rows.Count > 0)
                {
                    await Task.Run(() =>
                    {
                        _mySQLService.InserirDadosTabela("etiqueta_zpl", dtEtiquetaParaInserir);
                        Invoke(new Action(() => pbGestao.Value += dtEtiquetaParaInserir.Rows.Count));
                    });

                    LogService.RegistrarLog("SUCCESS", $"✅ {dtEtiquetaParaInserir.Rows.Count} registros inseridos em etiqueta_zpl.");
                }

                // Inserir modelos em lote (sem controle de duplicidade, se quiser pode implementar)
                if (dtModelos.Rows.Count > 0)
                {
                    await Task.Run(() =>
                    {
                        _mySQLService.InserirDadosTabela("modelos_zpl", dtModelos);
                        Invoke(new Action(() => pbGestao.Value += dtModelos.Rows.Count));
                    });

                    LogService.RegistrarLog("SUCCESS", $"✅ {dtModelos.Rows.Count} registros inseridos em modelos_zpl.");
                }

                MessageBox.Show("Importação concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogService.RegistrarLog("ERROR", $"❌ Erro na importação: {ex.Message}");
                MessageBox.Show($"Erro ao importar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConverterZpl.Enabled = true;
                pbGestao.Visible = false;
                pbGestao.Value = 0;
            }
        }

        private void btnSelecionarArquivoAccess_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivo Access (*.mdb)|*.mdb|Todos os arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione o arquivo zpl.mdb";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoArquivo = openFileDialog.FileName;
                    caminhoArquivoAccess = openFileDialog.FileName;
                    txtCaminhoArquivoAccess.Text = caminhoArquivoAccess;
                }
            }
        }



        private void btnExportarBanco_Click(object sender, EventArgs e)
        {
            MessageBox.Show("⚠️ Função ainda não disponível.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
