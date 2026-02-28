using System;
using System.Drawing;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.UI.Tools.HistoryHelper.HistoryHelper;

namespace Calculator.Calculator.UI.Tools.HistoryHelper
{
    public partial class HistoryView : UserControl
    {
        private readonly HistoryListPresenter presenter;
        private readonly HistoryListDrawer Drawer;

        public event Action<HistoryEntry>? ItemChosen;

        public HistoryView()
        {
            InitializeComponent();

            presenter = new HistoryListPresenter(lstHistory, lblMessageEmpty);
            Drawer = new HistoryListDrawer(lstHistory);
            Drawer.AttachOnce();

            lstHistory.SizeChanged += (_, __) => presenter.RequestRefresh();
            lstHistory.DoubleClick += (_, __) => RaiseChosen();
        }

        public void SetHistory(HistoryService history) => presenter.Bind(history);

        public void RefreshNow() => presenter.RequestRefresh();

        public void FocusList() => presenter.FocusFirstIfNothingSelected();

        public void ScrollHorizontal(int dx)
        {
            if (presenter.IsRefreshing) return;
            ListViewScroller.ScrollBy(lstHistory, dx);
        }

        public void DeleteSelected() => presenter.DeleteSelected();

        public void ClearAll() => presenter.ClearAll();

        private void RaiseChosen()
        {
            if (presenter.TryGetSelected(out var entry) && entry != null)
                ItemChosen?.Invoke(entry);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var pen = new Pen(Color.FromArgb(120, 255, 255, 255), 1);
            e.Graphics.DrawLine(pen, 0, 0, Width, 0);
        }

        private void btnDelete_Click(object sender, EventArgs e) => DeleteSelected();
        private void btnMC_Click(object sender, EventArgs e) => ClearAll();
    }
}