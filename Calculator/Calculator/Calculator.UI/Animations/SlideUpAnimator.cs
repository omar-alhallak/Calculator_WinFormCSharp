using System;
using System.Diagnostics;

namespace Calculator.Calculator.UI
{
    public sealed class SlideUpAnimator : IDisposable // إنميشن لفتح الذاكرة
    {
        private readonly System.Windows.Forms.Timer timer = new();
        private readonly Stopwatch SW = new();

        private Control? Panel;
        private Form? Host;

        private int BeginH;
        private int EndH;

        private bool Opening;

        public int DurationMs { get; set; } = 350; 
        public int TickMs { get => timer.Interval; set => timer.Interval = Math.Max(5, value); }

        public bool IsRunning => timer.Enabled;

        public event Action<bool>? Completed;

        public SlideUpAnimator()
        {
            timer.Interval = 10;  timer.Tick += Tick;
        }

        public void Toggle(Control panel, Form host, int targetHeight) // تحكم بإغلاق وتسكير الإنيميشن
        {
            if (IsRunning) return;

            bool isOpen = panel.Visible && panel.Height > 0;
            if (isOpen) Close(panel, host);
            else Open(panel, host, targetHeight);
        }

        public void Open(Control panel, Form host, int targetHeight) // يفتح اللوحة
        {
            if (IsRunning) return;

            Panel = panel ?? throw new ArgumentNullException(nameof(panel));
            Host = host ?? throw new ArgumentNullException(nameof(host));

            Opening = true;

            BeginH = Math.Max(0, Panel.Height);
            EndH = Math.Max(0, targetHeight);

            if (EndH == 0)
            {
                Panel.Visible = false;
                Panel.Height = 0;
                Completed?.Invoke(true);
                return;
            }

            Panel.Visible = true;
            Panel.BringToFront();
            LayoutPosition(BeginH);

            PauseLayout(Panel, true);

            SW.Restart();
            timer.Start();
        }

        public void Close(Control panel, Form host) // يغلق اللوحة
        {
            if (IsRunning) return;

            Panel = panel ?? throw new ArgumentNullException(nameof(panel));
            Host = host ?? throw new ArgumentNullException(nameof(host));

            Opening = false;

            BeginH = Math.Max(0, Panel.Height);
            EndH = 0;

            if (BeginH == 0)
            {
                Panel.Visible = false;
                Completed?.Invoke(false);
                return;
            }

            Panel.BringToFront();
            LayoutPosition(BeginH);

            PauseLayout(Panel, true);

            SW.Restart();
            timer.Start();
        }

        private void Tick(object? sender, EventArgs e) // حساب أوقات اللزمة للإنيميشن
        {
            if (Panel == null || Host == null)
            {
                StopInternal(false);
                return;
            }

            if (Panel.IsDisposed || Host.IsDisposed || Host.Disposing) 
            {
                StopInternal(false);
                return;
            }

            if(!Host.IsHandleCreated||!Panel.IsHandleCreated)
            {
                StopInternal(false);
                return;
            }

            double t = SW.Elapsed.TotalMilliseconds / Math.Max(1, DurationMs);
            if (t >= 1.0) t = 1.0;

            double eased = SmoothStop(t);

           int h=Lerp(BeginH, EndH, eased);

            LayoutPosition(h);

            if (t >= 1.0)
            {
                bool opened = Opening;
                StopInternal(opened);
            }
        }

        private void StopInternal(bool opened) // تنظيف وتثبيت للإنيميشن
        {
            timer.Stop();
            SW.Stop();

            if (Panel != null)
            {
                if (opened)
                {
                    Panel.Height = EndH;
                    Panel.Visible = true;
                }
                else
                {
                    Panel.Height = 0;
                    Panel.Visible = false;
                }

                LayoutPosition(Panel.Height);
                PauseLayout(Panel, false);
            }

            Completed?.Invoke(opened);
        }

        private void LayoutPosition(int height) // مكان اللوحة على الشاشة
        {
            if (Panel == null || Host == null) return;

            height = Math.Max(0, height);

            Panel.Left = 0;
            Panel.Width = Host.ClientSize.Width;
            Panel.Height = height;
            Panel.Top = Host.ClientSize.Height - height;
        }

        private static int Lerp(int a, int b, double t) => (int)Math.Round(a + (b - a) * t); // حساب 

        private static double SmoothStop(double t) // تحكم بسلاسة الإنيميشن سريعا أبطئ ناعمة جداً
        {
            double p = 1.0 - t;
            return 1.0 - (p * p * p);
        }

        private static void PauseLayout(Control c, bool enable) // إيقاف مؤقت لإعادة ترتيب العناصر أثناء الإنميشن
        {
            if (enable)
                c.SuspendLayout();
            else
            {
                c.ResumeLayout(true);
                c.Invalidate(true);
            }
        }

        public void Dispose() // تنظيف المؤقتات
        {
            timer.Stop();
            timer.Tick -= Tick;
            timer.Dispose();
            SW.Stop();
        }
    }
}