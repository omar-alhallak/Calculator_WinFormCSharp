using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Calculator.Calculator.UI.Animations.TooleAnimations
{
    public sealed class CloseOutSideClicks : IDisposable // تحكم بإغلاق الإنيميشن عن طريق ضغطات الماوس
    {
        private readonly Form Host;
        private readonly Control Target;
        private readonly Func<bool> IsOpen;
        private readonly Action CloseAction;

        private readonly HashSet<Control> Ignored = new();

        public CloseOutSideClicks(Form host, Control target, Func<bool> isOpen, Action closeAction)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            IsOpen = isOpen ?? throw new ArgumentNullException(nameof(isOpen));
            CloseAction = closeAction ?? throw new ArgumentNullException(nameof(closeAction));
        }

        public CloseOutSideClicks Ignore(Control c) // العناصر المستثناة
        {
            if (c != null) Ignored.Add(c);
            return this;
        }

        public void Start() // تفعيل الرقابة
        {
            AttachAllControls(Host);
            Host.ControlAdded += OnControlAdded;
        }

        private void OnControlAdded(object? sender, ControlEventArgs e) // ربط الحدث عند إضافة تول جديد
        {
            if (e.Control != null) AttachAllControls(e.Control);
        }

        private void AttachAllControls(Control root) // ربط الحدث على جميع العناصر
        {
            root.MouseDown += HandleMouseDown;
            foreach (Control ch in root.Controls)
                AttachAllControls(ch);
        }

        private void HandleMouseDown(object? sender, MouseEventArgs e) // تنفذ عند أي ضغطة ماوس لمعالجتها
        {
            if (!IsOpen()) return;
            if (sender is not Control src) return;

            foreach (var ig in Ignored)
            {
                if (ig == null) continue;
                if (src == ig || ig.Contains(src)) return;
            }

            if (src == Target || Target.Contains(src)) return;

            CloseAction();
        }

        public void Dispose() // يفسخ العقد مع الحدث
        {
            Host.ControlAdded -= OnControlAdded;
        }
    }
}