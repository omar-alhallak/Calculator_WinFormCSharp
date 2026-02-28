using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Calculator.Calculator.UI.Tools.HistoryHelper.HistoryHelper
{
    public static class ListViewScroller // تفعيل الأسهم الجانبية
    {
        private const int LVM_SCROLL = 0x1014;

        public static void ScrollBy(ListView list, int dx)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (!list.IsHandleCreated) return;

            SendMessage(list.Handle, LVM_SCROLL, dx, nint.Zero);
        }

        [DllImport("user32.dll")]
        private static extern nint SendMessage(nint hWnd, int msg, nint wParam, nint lParam);
    }
}