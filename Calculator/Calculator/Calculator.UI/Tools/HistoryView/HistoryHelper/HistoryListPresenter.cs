using System;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Application.History;

namespace Calculator.Calculator.UI.Tools.HistoryView.HistoryHelper
{
    public sealed class HistoryListPresenter // صلة الوصل بين الهيستوري و الفورم
    {
        private readonly ListView List;
        private HistoryService? History;

        public HistoryListPresenter(ListView list)
        {
            List = list ?? throw new ArgumentNullException(nameof(list));
        }

        public void Bind(HistoryService history)
        {
            History = history ?? throw new ArgumentNullException(nameof(history));
            Refresh();
        }

        public void Refresh()
        {
            if (History == null) return;

            List.BeginUpdate();
            List.Items.Clear();

            foreach (var entry in History.GetLast(5000))
            {
                var text = entry?.ToString();
                if (string.IsNullOrWhiteSpace(text)) continue;

                List.Items.Add(new ListViewItem(text) { Tag = entry });
            }

            List.EndUpdate();
            UpdateColumnWidth();
        }

        public void PrepareForKeyboard()
        {
            if (!List.IsHandleCreated) return;

            List.Focus();

            if (List.Items.Count > 0 && List.SelectedIndices.Count == 0)
            {
                List.Items[0].Selected = true;
                List.Items[0].Focused = true;
                List.EnsureVisible(0);
            }
        }

        public bool DeleteSelected()
        {
            if (History == null) return false;
            if (List.SelectedItems.Count == 0) return false;
            if (List.SelectedItems[0].Tag is not HistoryEntry entry) return false;

            bool ok = History.Delete(entry.Id);
            if (ok) Refresh();
            return ok;
        }

        public void ClearAll()
        {
            if (History == null) return;
            History.ClearAll();
            Refresh();
        }

        public bool TryGetSelected(out HistoryEntry? entry)
        {
            entry = null;
            if (List.SelectedItems.Count == 0) return false;
            if (List.SelectedItems[0].Tag is not HistoryEntry e) return false;

            entry = e;
            return true;
        }

        private void UpdateColumnWidth()
        {
            if (List.Columns.Count == 0) return;

            int maxTextWidth = 0;
            foreach (ListViewItem it in List.Items)
            {
                string t = it?.Text ?? "";
                int w = TextRenderer.MeasureText(t, List.Font).Width;
                if (w > maxTextWidth) maxTextWidth = w;
            }

            int desired = Math.Max(List.ClientSize.Width - 2, maxTextWidth + 30);
            List.Columns[0].Width = desired;
        }
    }
}