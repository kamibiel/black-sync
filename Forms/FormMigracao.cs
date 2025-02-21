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

            cbMarcarTodas.Text = marcarTodas ? "Desmarcar todas a tabelas" : "Selecionar todas as tabelas";

            for (int i = 0; i< clbTabelasFirebird.Items.Count; i++)
            {
                clbTabelasFirebird.SetItemChecked(i, marcarTodas);
            }
        }

        private List<string> ObterTabelasSelecionadas()
        {
            return clbTabelasFirebird.CheckedItems.Cast<string>().ToList();
        }

        private async void btnMigrar_Click(object sender, EventArgs e)
        {

            var tabelasSelecionadas = ObterTabelasSelecionadas();

            if (tabelasSelecionadas.Count == 0)
            {
                MessageBox.Show("Por favor, selecione ao menos uma tabela para migrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogService.RegistrarLog("INFO", $"🔄 Iniciando migração de {tabelasSelecionadas.Count} tabelas.");

            pbMigracao.Visible = true;
            pbMigracao.Value = 0;
            btnMigrar.Enabled = false;
            Invoke(new Action(() => txtLog.Clear()));
            txtLog.AppendText($"🔄 Iniciando migração de {tabelasSelecionadas.Count} tabelas...{Environment.NewLine}");

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

                            // 🔹 Atualiza progress bar de forma segura
                            Invoke(new Action(() =>
                            {
                                pbMigracao.Value = Math.Min(pbMigracao.Maximum, pbMigracao.Value + dados.Rows.Count);
                            }));
                        }

                        LogService.RegistrarLog("SUCCESS", $"✅ Tabela {tabela} migrada com sucesso ({registrosMigrados} registros inseridos)!");
                        Invoke(new Action(() => txtLog.AppendText($"✅ Tabela {tabela} migrada com sucesso ({registrosMigrados} registros inseridos)!{Environment.NewLine}")));

                        if (_firebirdService.ColunaExiste(tabela, "Enviado"))
                        {
                            var respostaEnviadoFirebird = MessageBox.Show($"Deseja realizar update da coluna ENVIADO da tabela: {tabela} do Banco de Dados Firebird?{Environment.NewLine}", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (respostaEnviadoFirebird == DialogResult.Yes)
                            {
                                _firebirdService.AtualizarEnviado(tabela);

                                LogService.RegistrarLog("INFO", $"📤 Registros atualizados como enviados no Firebird para {tabela}.");
                                Invoke(new Action(() => txtLog.AppendText($"📤 Registros atualizados como enviados no Firebird para {tabela}.{Environment.NewLine}")));
                            }
                            else
                            {
                                LogService.RegistrarLog("INFO", $"📤 Ação cancelada, não foi realizado a atualização na tabela: {tabela}.");
                                Invoke(new Action(() => txtLog.AppendText($"📤 Ação cancelada, não foi realizado a atualização na tabela: {tabela}.{Environment.NewLine}")));
                            }
                        }

                        if (_mySQLService.ColunaExiste(tabela, "Enviado"))
                        {
                            var respostaEnviadoMySQL = MessageBox.Show($"Deseja realizar update da coluna ENVIADO da tabela: {tabela} do Banco de Dados MySQL?{Environment.NewLine}", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (respostaEnviadoMySQL == DialogResult.Yes)
                            {
                                _mySQLService.AtualizarEnviado(tabela);

                                LogService.RegistrarLog("SUCCESS", $"📤 Registros atualizados como enviados no MySQL para {tabela}.");
                                Invoke(new Action(() => txtLog.AppendText($"📤 Registros atualizados como enviados no MySQL para {tabela}.{Environment.NewLine}")));
                            }
                            else
                            {
                                LogService.RegistrarLog("INFO", $"📤 Ação cancelada, não foi realizado a atualização na tabela: {tabela}.");
                                Invoke(new Action(() => txtLog.AppendText($"📤 Ação cancelada, não foi realizado a atualização na tabela: {tabela}.{Environment.NewLine}")));
                            }
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

            _formPrincipal.AtualizarServicos();
        }
    }
}
