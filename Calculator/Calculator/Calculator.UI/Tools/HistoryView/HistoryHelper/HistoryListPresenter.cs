using System;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Application.History;

namespace Calculator.Calculator.UI.Tools.HistoryHelper
{
    public sealed class HistoryListPresenter // مسؤول ربط بين يوأي والهيستوري سيرفس
    {
        private readonly ListView List;
        private readonly Label? EmptyLabel;
        private HistoryService? History;

        private bool RefreshQueued;
        private bool isRefreshing;
        public bool IsRefreshing => isRefreshing;

        public HistoryListPresenter(ListView list, Label? emptyLabel = null)
        {
            List = list ?? throw new ArgumentNullException(nameof(list));
            EmptyLabel = emptyLabel;

            PrepareEmptyLabel();
        }

        public void Bind(HistoryService history) // "ربط "مصدر البيانات
        {
            History = history ?? throw new ArgumentNullException(nameof(history));
            RequestRefresh();
        }

        public void RequestRefresh() // تحديث أمن لمنع المشاكل
        {
            if (History == null) return;
            if (List.IsDisposed || List.Disposing) return;

            if (!List.IsHandleCreated)
            {
                if (RefreshQueued) return;
                RefreshQueued = true;

                void OnHandleCreated(object? s, EventArgs e)
                {
                    List.HandleCreated -= OnHandleCreated;
                    List.BeginInvoke(new Action(RefreshCore));
                }

                List.HandleCreated += OnHandleCreated;
                return;
            }

            if (RefreshQueued) return;
            RefreshQueued = true;

            List.BeginInvoke(new Action(RefreshCore));
        }

        private void RefreshCore() // التحديث الحقيقي
        {
            RefreshQueued = false;
            if (History == null) return;
            if (List.IsDisposed || List.Disposing) return;
            if (isRefreshing) return;

            isRefreshing = true;

            List.BeginUpdate();
            try
            {
                List.Items.Clear();

                foreach (var entry in History.GetLast(5000))
                {
                    var text = entry?.ToString();
                    if (string.IsNullOrWhiteSpace(text)) continue;
                    List.Items.Add(new ListViewItem(text) { Tag = entry });
                }

                EnsureSingleColumn();
                UpdateColumnWidth();
            }
            finally
            {
                List.EndUpdate();
                isRefreshing = false;
            }

            UpdateEmptyVisibility();
        }

        public void FocusFirstIfNothingSelected()
        {
            if (!List.IsHandleCreated) return;
            if (List.Items.Count == 0) return;

            List.Focus();

            if (List.SelectedIndices.Count == 0)
            {
                List.Items[0].Selected = true;
                List.Items[0].Focused = true;
                List.EnsureVisible(0);
            }
        }

        public bool DeleteSelected()
        {
            if (History == null) return false;
            if (isRefreshing) return false;

            if (List.SelectedItems.Count == 0) return false;
            if (List.SelectedItems[0].Tag is not HistoryEntry entry) return false;

            bool ok = History.Delete(entry.Id);
            if (ok) RequestRefresh();
            return ok;
        }

        public void ClearAll()
        {
            if (History == null) return;
            if (isRefreshing) return;

            History.ClearAll();
            RequestRefresh();
        }

        public bool TryGetSelected(out HistoryEntry? entry) // جلب العنصر
        {
            entry = null;
            if (List.SelectedItems.Count == 0) return false;
            if (List.SelectedItems[0].Tag is not HistoryEntry e) return false;

            entry = e;
            return true;
        }

// ---------------------------------------
//              Empty label 
// ---------------------------------------

        private void PrepareEmptyLabel()
        {
            if (EmptyLabel == null) return;

            EmptyLabel.BringToFront();

            EmptyLabel.Visible = false;

            List.SizeChanged += (_, __) =>
            {
                if (EmptyLabel == null) return;
                EmptyLabel.Bounds = List.Bounds;
                EmptyLabel.BringToFront();
            };
        }

        private void UpdateEmptyVisibility()
        {
            if (EmptyLabel == null) return;

            bool isEmpty = List.Items.Count == 0;

            EmptyLabel.Bounds = List.Bounds;
            EmptyLabel.Visible = isEmpty;
            EmptyLabel.BringToFront();
        }

 // --------------------------------------
//              ListView column
// ---------------------------------------

        private void EnsureSingleColumn()
        {
            if (List.Columns.Count == 0)
                List.Columns.Add("", Math.Max(10, List.ClientSize.Width - 2));
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