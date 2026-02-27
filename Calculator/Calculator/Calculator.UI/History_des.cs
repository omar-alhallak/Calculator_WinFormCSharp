using System;
using System.Drawing;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Application.History;

namespace Calculator.Calculator.UI
{
    public partial class History_des : Form
    {
        private readonly HistoryService History;
        public HistoryEntry? SelectedEntry { get; private set; }

        public History_des(HistoryService history)
        {
            InitializeComponent();
            History = history;

            SetupList();
            LoadHistory();
            this.Deactivate += (s, e) => this.Close();
        }

        private void SetupList() // تجهيز الليست للعرض
        {
            lstHistory.Columns.Clear();
            lstHistory.Columns.Add("", Math.Max(10, lstHistory.ClientSize.Width - 2));

            lstHistory.OwnerDraw = true;
            lstHistory.DrawItem += (_, e) => e.DrawBackground();
            lstHistory.DrawSubItem += LstHistory_DrawSubItem;
            lstHistory.DrawColumnHeader += (_, e) => e.DrawBackground();

            lstHistory.SizeChanged += (_, __) => UpdateColumnWidth();
            lstHistory.DoubleClick += (_, __) => SelectCurrentAndClose();
        }

        private void LstHistory_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e) // 
        {
            if (e.ColumnIndex != 0) return;
            if (e.Item == null) return;

            bool selected = e.Item.Selected;
            bool focused = lstHistory.Focused;

            Color selBack = Color.FromArgb(70, 255, 165, 0);
            Color selText = Color.White;

            using (var bg = new SolidBrush(selected ? selBack : lstHistory.BackColor)) e.Graphics.FillRectangle(bg, e.Bounds);

            string text = e.SubItem?.Text ?? e.Item?.Text ?? "";
            TextRenderer.DrawText(e.Graphics, text, lstHistory.Font, e.Bounds, selected ? selText : lstHistory.ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            using var pen = new Pen(Color.FromArgb(120, 255, 255, 255), 1);

            if (e.ItemIndex == 0)
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top + 1, e.Bounds.Right, e.Bounds.Top + 1);

            e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);

            if (selected && focused)
            {
                using var border = new Pen(Color.FromArgb(160, 255, 165, 0), 1);
                e.Graphics.DrawRectangle(border, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            }
        }

        private void LoadHistory()
        {
            lstHistory.BeginUpdate();
            lstHistory.Items.Clear();

            foreach (var entry in History.GetLast(5000))
            {
                var text = entry?.ToString();
                if (string.IsNullOrWhiteSpace(text)) continue;

                lstHistory.Items.Add(new ListViewItem(text) { Tag = entry });
            }

            lstHistory.EndUpdate();
            UpdateColumnWidth();
        }

        private void UpdateColumnWidth()
        {
            if (lstHistory.Columns.Count == 0) return;

            int maxTextWidth = 0;

            foreach (ListViewItem it in lstHistory.Items)
            {
                string t = it?.Text ?? "";
                int w = TextRenderer.MeasureText(t, lstHistory.Font).Width;
                if (w > maxTextWidth) maxTextWidth = w;
            }

            int desired = Math.Max(lstHistory.ClientSize.Width - 2, maxTextWidth + 30);
            lstHistory.Columns[0].Width = desired;
        }

        private void SelectCurrentAndClose()
        {
            if (lstHistory.SelectedItems.Count == 0) return;
            if (lstHistory.SelectedItems[0].Tag is not HistoryEntry entry) return;

            SelectedEntry = entry;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstHistory.SelectedItems.Count == 0) return;
            if (lstHistory.SelectedItems[0].Tag is not HistoryEntry entry) return;

            History.Delete(entry.Id);
            LoadHistory();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            History.ClearAll();
            LoadHistory();
        }
    }
}