using System;
using System.Drawing;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.UI.Tools.HistoryView.HistoryHelper;

namespace Calculator.Calculator.UI.Tools.HistoryView
{
    public partial class HistoryView : UserControl
    {
        private readonly HistoryListPresenter Presenter;
        private readonly HistoryListDrawer Drawer;

        public event Action<HistoryEntry>? ItemChosen;

        public HistoryView()
        {
            InitializeComponent();

            Presenter = new HistoryListPresenter(lstHistory);
            Drawer = new HistoryListDrawer(lstHistory);
            Drawer.Attach();
            MassegeEmpty();

            lstHistory.SizeChanged += (_, __) => RefreshNow();
            lstHistory.DoubleClick += (_, __) => RaiseChosen();
        }

        public void SetHistory(HistoryService history)
        {
            Presenter.Bind(history);
            lblMessageEmpty.Visible = lstHistory.Items.Count == 0;
        }

        public void RefreshNow()
        {
            Presenter.Refresh();
            MassegeEmpty();
        }

        private void MassegeEmpty()
        {
            bool isEmbty = lstHistory.Items.Count == 0;
            lblMessageEmpty.Visible = isEmbty;
        }

        public void FocusList() => Presenter.PrepareForKeyboard();

        public void ScrollHorizontal(int dx) => ListViewScroller.ScrollBy(lstHistory, dx);

        public void DeleteSelected() => Presenter.DeleteSelected();

        public void ClearAll() => Presenter.ClearAll();

        private void RaiseChosen()
        {
            if (Presenter.TryGetSelected(out var entry) && entry != null)
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