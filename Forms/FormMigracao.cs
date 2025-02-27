using BlackSync.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackSync.Forms
{
    public partial class FormMigracao : Form
    {
        private readonly MySQLService _mySQLService;
        private readonly FirebirdService _firebirdService;
        private readonly FormPrincipal _formPrincipal;

        public FormMigracao(FormPrincipal formprincipal, string mysqlServer, string mysqlDatabase, string mysqlUser, string mysqlPassword, string firebirdDSN)
        {
            InitializeComponent();
            _formPrincipal = formprincipal ?? throw new ArgumentNullException(nameof(formprincipal));

            //_mySQLService = new MySQLService(mysqlServer, mysqlDatabase, mysqlUser, mysqlPassword);
            //_firebirdService = new FirebirdService(firebirdDSN);

            _mySQLService = _formPrincipal.ObterMySQLService();
            _firebirdService = _formPrincipal.ObterFirebirdService();

            CarregarTabelas();

            // 🔹 Conecta o evento para atualizar automaticamente o checkbox
            clbTabelasFirebird.ItemCheck += clbTabelasFirebird_ItemCheck;
        }

        public MySQLService ObterMySQLService() => _mySQLService;
        public FirebirdService ObterFirebirdService() => _firebirdService;

        private void CarregarTabelas()
        {
            try
            {
                var tabelasFirebird = _firebirdService.GetTabelasFirebird();
                clbTabelasFirebird.Items.Clear();

                foreach (var tabela in tabelasFirebird)
                    clbTabelasFirebird.Items.Add(tabela, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void cbMarcarTodas_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodas = cbMarcarTodas.Checked;

            // Temporariamente desativa o evento para evitar loop infinito
            clbTabelasFirebird.ItemCheck -= clbTabelasFirebird_ItemCheck;

            for (int i = 0; i < clbTabelasFirebird.Items.Count; i++)
            {
                clbTabelasFirebird.SetItemChecked(i, marcarTodas);
            }

            // Atualiza o texto do checkbox
            cbMarcarTodas.Text = marcarTodas ? "Desmarcar todas as tabelas" : "Selecionar todas as tabelas";

            // Reativa o evento
            clbTabelasFirebird.ItemCheck += clbTabelasFirebird_ItemCheck;
        }

        private void clbTabelasFirebird_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                int totalItens = clbTabelasFirebird.Items.Count;
                int itensSelecionados = 0;

                // Conta os itens já selecionados e adiciona a mudança atual do evento
                for (int i = 0; i < totalItens; i++)
                {
                    if (clbTabelasFirebird.GetItemCheckState(i) == CheckState.Checked || (i == e.Index && e.NewValue == CheckState.Checked))
                    {
                        itensSelecionados++;
                    }
                }

                // Temporariamente desativa o evento para evitar loops
                cbMarcarTodas.CheckedChanged -= cbMarcarTodas_CheckedChanged;

                // Atualiza o estado do checkbox principal
                if (itensSelecionados == totalItens)
                {
                    cbMarcarTodas.Checked = true;
                    cbMarcarTodas.Text = "Desmarcar todas as tabelas";
                }
                else if (itensSelecionados == 0)
                {
                    cbMarcarTodas.Checked = false;
                    cbMarcarTodas.Text = "Selecionar todas as tabelas";
                }
                else
                {
                    cbMarcarTodas.Checked = false;
                    cbMarcarTodas.CheckState = CheckState.Indeterminate; // Estado intermediário
                    cbMarcarTodas.Text = "Desmarcar todas as tabelas";
                }

                // Reativa o evento do checkbox
                cbMarcarTodas.CheckedChanged += cbMarcarTodas_CheckedChanged;
            });
        }

        private List<string> ObterTabelasSelecionadas()
        {
            return clbTabelasFirebird.CheckedItems.Cast<string>().ToList();
        }

        private async void btnMigrar_Click(object sender, EventArgs e)
        {
            LogService.RegistrarLog(
                "INFO", 
                $"🔄 Limpando o Log Verificação"
            );
            txtLog.Clear();
            pbMigracao.Value = 0;

            var tabelasSelecionadas = ObterTabelasSelecionadas();

            if (tabelasSelecionadas.Count == 0)
            {
                MessageBox.Show(
                    "Por favor, selecione ao menos uma tabela para migrar.", 
                    "Aviso", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                return;
            }

            LogService.RegistrarLog(
                "INFO", 
                $"🔄 Iniciando migração de {tabelasSelecionadas.Count} tabelas."
            );
            txtLog.AppendText($"🔄 Iniciando migração de {tabelasSelecionadas.Count} tabelas...{Environment.NewLine}");

            pbMigracao.Visible = true;
            btnMigrar.Enabled = false;

            // 🔹 Obtém o total de registros antes de iniciar a migração
            int totalRegistros = _firebirdService.ObterTotalRegistros(tabelasSelecionadas);
            pbMigracao.Maximum = Math.Max(1, totalRegistros);

            await Task.Run(() =>
            {
                foreach (var tabela in tabelasSelecionadas)
                {
                    try
                    {
                        LogService.RegistrarLog("INFO", $"📥 Migrando tabela: {tabela}");
                        Invoke(new Action(() => txtLog.AppendText($"📥 Migrando tabela: {tabela}...{Environment.NewLine}")));

                        bool truncarTabela = false;
                        bool apenasNovosRegistros = false;

                        if (_mySQLService.TabelaTemDados(tabela))
                        {
                            var resposta = MessageBox.Show($"A tabela {tabela} já contém dados no MySQL.{Environment.NewLine}{Environment.NewLine}" +
                                                           $"Escolha uma opção:{Environment.NewLine}{Environment.NewLine}" +
                                                           $"✔ [SIM] - Apaga todos os dados do MySQL antes da inserção{Environment.NewLine}" +
                                                           $"✔ [NÃO] - Insere apenas registros novos{Environment.NewLine}" +
                                                           $"✔ [CANCELAR] - Ignora essa tabela e segue para a próxima",
                                                           $"⚠️ Dados existentes detectados!",
                                                           MessageBoxButtons.YesNoCancel,
                                                           MessageBoxIcon.Warning);

                            if (resposta == DialogResult.Yes)
                            {
                                truncarTabela = true;
                                _mySQLService.TruncateTabela(tabela);
                                LogService.RegistrarLog("SUCCESS", $"🚀 Tabela {tabela} truncada!");
                                Invoke(new Action(() => txtLog.AppendText($"🚀 Tabela {tabela} truncada!{Environment.NewLine}")));
                            }
                            else if (resposta == DialogResult.No)
                            {
                                apenasNovosRegistros = true;
                            }
                            else if (resposta == DialogResult.Cancel)
                            {
                                Invoke(new Action(() => txtLog.AppendText($"⚠️ Migração cancelada para {tabela}.{Environment.NewLine}")));
                                continue;
                            }
                        }

                        int registrosMigrados = 0;

                        if (_firebirdService.FirebirdTemMaisColunasQueMySQL(tabela, _mySQLService))
                        {
                            MessageBox.Show($"Erro: A tabela {tabela} no Firebird tem mais colunas que no MySQL. Ajuste a estrutura do MySQL para continuar.",
                                            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        foreach (var lote in _firebirdService.ObterDadosTabelaEmLotes(tabela))
                        {
                            DataTable dados = lote.Copy();

                            if (dados.Rows.Count == 0)
                            {
                                Invoke(new Action(() => txtLog.AppendText($"⚠️ Nenhum dado encontrado na tabela {tabela}. Nenhum registro será inserido.{Environment.NewLine}")));
                                continue;
                            }

                            if (apenasNovosRegistros)
                            {
                                var pkColuna = "CODIGO";
                                var codigosExistentes = _mySQLService.ObterCodigosExistentes(tabela, pkColuna);

                                var novosDados = dados.AsEnumerable()
                                                      .Where(row => !codigosExistentes.Contains(row[pkColuna].ToString()))
                                                      .CopyToDataTable();

                                if (novosDados.Rows.Count == 0)
                                {
                                    Invoke(new Action(() => txtLog.AppendText($"⚠️ Todos os registros já existem. Nenhum novo dado inserido na tabela {tabela}.{Environment.NewLine}")));
                                    continue;
                                }

                                dados = novosDados; // Atualiza os dados filtrados
                            }

                            _mySQLService.InserirDadosTabela(tabela, dados);
                            registrosMigrados += dados.Rows.Count;

                            Invoke(new Action(() =>
                            {
                                pbMigracao.Value = Math.Min(pbMigracao.Maximum, pbMigracao.Value + dados.Rows.Count);
                            }));
                        }

                        LogService.RegistrarLog("SUCCESS", $"✅ Tabela {tabela} migrada com sucesso ({registrosMigrados} registros inseridos)!");
                        Invoke(new Action(() => txtLog.AppendText($"✅ Tabela {tabela} migrada com sucesso ({registrosMigrados} registros inseridos)!{Environment.NewLine}")));

                        if (_firebirdService.ColunaExiste(tabela, "Enviado"))
                        {
                            _firebirdService.AtualizarEnviado(tabela);

                                LogService.RegistrarLog("INFO", $"📤 Registros atualizados como enviados no Firebird para {tabela}.");
                                Invoke(new Action(() => txtLog.AppendText($"📤 Registros atualizados como enviados no Firebird para {tabela}.{Environment.NewLine}")));
                        }

                        if (_mySQLService.ColunaExiste(tabela, "Enviado"))
                        {
                           _mySQLService.AtualizarEnviado(tabela);

                                LogService.RegistrarLog("SUCCESS", $"📤 Registros atualizados como enviados no MySQL para {tabela}.");
                                Invoke(new Action(() => txtLog.AppendText($"📤 Registros atualizados como enviados no MySQL para {tabela}.{Environment.NewLine}")));
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.RegistrarLog("ERROR", $"Erro ao migrar {tabela}: {ex.Message}");
                        Invoke(new Action(() => txtLog.AppendText($"❌ Erro ao migrar {tabela}: {ex.Message}{Environment.NewLine}")));
                    }
                }
            });

            LogService.RegistrarLog("INFO", $"🎉 Migração concluída!");
            pbMigracao.Visible = false;
            btnMigrar.Enabled = true;
            txtLog.AppendText($"🎉 Migração concluída!{Environment.NewLine}");
        }
    }
}
