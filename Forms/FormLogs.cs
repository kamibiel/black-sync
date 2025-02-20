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
    public partial class FormLogs : Form
    {
        public FormLogs()
        {
            InitializeComponent();
            CarregarLogsDoDia();
        }

        /// <summary>
        /// Carrega os logs do dia atual e exibe no TextBox.
        /// </summary>
        private void CarregarLogsDoDia()
        {
            txtLogs.Clear();

            List<LogEntry> logs = LogService.ObterLogsDoDia();

            if (logs.Count == 0)
            {
                txtLogs.AppendText("Nenhum log registrado hoje.");
                return;
            }

            foreach (var log in logs)
            {
                txtLogs.AppendText($"[{log.DataHora}] {log.Tipo}: {log.Mensagem}{Environment.NewLine}");
            }
        }
    }
}
