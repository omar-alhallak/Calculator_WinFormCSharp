using System;
using System.Drawing;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Application.History;

namespace Calculator.Calculator.UI
{
    public partial class History_des : Form
    {
        private readonly HistoryService history;
        public HistoryEntry? SelectedEntry { get; private set; }
        public History_des(HistoryService history)
        {
            InitializeComponent();
            this.history = history;
            LoadHistory();
        }

        private void LoadHistory()
        {
            lbHistory.Items.Clear();
            var items = history.GetLast(5000);
            foreach (var item in items)
            {
                lbHistory.Items.Add(item);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItem is not HistoryEntry entry) return;

            history.Delete(entry.Id);
            LoadHistory();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            history.ClearAll();
            LoadHistory();
        }

        private void lbHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItem is not HistoryEntry entry) return;
            SelectedEntry= entry;
            DialogResult=DialogResult.OK;
            Close();
        }
    }
}