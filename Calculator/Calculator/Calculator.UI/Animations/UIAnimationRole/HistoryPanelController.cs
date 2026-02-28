using System;
using System.Windows.Forms;
using Calculator.Calculator.UI.Tools.HistoryHelper;

namespace Calculator.Calculator.UI.Animations.TooleAnimations
{
    public sealed class HistoryPanelController : IDisposable // إدارة فتح وإغلاق اللوحة
    {
        private readonly Form Host;
        private readonly HistoryView view;
        private readonly SlideUpAnimator Anim;

        public HistoryView View => view;
        public bool IsOpen => view.Visible && view.Height > 0;

        public HistoryPanelController(Form host, HistoryView view, SlideUpAnimator anim)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            Anim = anim ?? throw new ArgumentNullException(nameof(anim));

            this.view.Visible = false;
            this.view.Height = 0;

            Anim.Completed += OnAnimCompleted;
        }

        private void OnAnimCompleted(bool opened) // لمعرفة إذا الانيميسن إنتهى
        {
            if (!opened) return;

            Host.BeginInvoke(new Action(() => view.FocusList()));
        }

        public void Toggle(int targetHeight) => Anim.Toggle(view, Host, targetHeight);

        public void Close() // إغلاق اللوحة فقط إذا مفتوحة
        {
            if (IsOpen) Anim.Close(view, Host);
        }

        public void Dispose() // يفسخ العقد مع الحدث
        {
            Anim.Completed -= OnAnimCompleted;
        }
    }
}