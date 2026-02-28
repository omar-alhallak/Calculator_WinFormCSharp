using System;
using System.Windows.Forms;

namespace Calculator.Calculator.UI.Forms.Coordinator
{
    public static class SafeInvoke // تنفيذ 
    {                              // UI Thread بطريقة آمنة
        public static void SafeBeginInvoke(this Control c, Action action)
        {
            if (c == null || action == null) return;
            if (c.IsDisposed || c.Disposing) return;
            if (!c.IsHandleCreated) return;

            try { c.BeginInvoke(action); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }
    }
}