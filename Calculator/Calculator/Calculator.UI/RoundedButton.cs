using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Calculator.Calculator.UI
{
    public class RoundedButton : Button // الزر المدور
    {
        public float Roundness { get; set; } = 0.65f;

        private GraphicsPath? OutPath;
        private GraphicsPath? InPath;

        private int LastW, LastH;
        private float LastRound;

        private bool Pressed;
        private bool Hover;

        public RoundedButton() 
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;
            DoubleBuffered = true;
            TabStop = false;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true); 
        }

        protected override void OnResize(EventArgs e) // حساب المسارات عند تغييرالحجم
        {
            base.OnResize(e);
            BuildPaths();
            Invalidate();
        }

        protected override void Dispose(bool disposing) // تنظيف موارد
        {
            if (disposing)
            {
                OutPath?.Dispose();
                InPath?.Dispose();
            }
            base.Dispose(disposing);
        }

        private static GraphicsPath CreateRoundedPath(Rectangle rect, int radius) // تبني الحواف الدائرية
        {
            var path = new GraphicsPath();
            if (rect.Width <= 0 || rect.Height <= 0) return path;

            int d = Math.Max(2, radius);
            int max = Math.Min(rect.Width, rect.Height);
            if (d > max) d = max;

            path.StartFigure();
            path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void BuildPaths() // بناء حدود الزر 
        {
            if (Width <= 2 || Height <= 2) return;

            if (OutPath != null &&LastW == Width &&LastH == Height &&Math.Abs(LastRound - Roundness) < 0.0001f) return;

            OutPath?.Dispose();
            InPath?.Dispose();

            LastW = Width;
            LastH = Height;
            LastRound = Roundness;

            int radius = (int)(Height * Roundness);
            radius = Math.Max(2, Math.Min(radius, Math.Min(Width, Height)));

            var outerRect = new Rectangle(0, 0, Width, Height);
            OutPath = CreateRoundedPath(outerRect, radius);
            Region = new Region(OutPath);

            int inset = 1; 
            var innerRect = new Rectangle(inset, inset, Width - inset * 2, Height - inset * 2);
            int innerRadius = Math.Max(2, radius - inset * 2);
            InPath = CreateRoundedPath(innerRect, innerRadius);
        }

        protected override void OnPaint(PaintEventArgs e) // تستدعى عند الرسم(تحسين الجودة تنعيم الحواف)م
        {
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            if (OutPath == null || InPath == null)
                BuildPaths();

            Color parentBack = Parent?.BackColor ?? BackColor;
            using (var parentBrush = new SolidBrush(parentBack))
            {
                g.FillPath(parentBrush, OutPath!);
            }

            using (var clipRegion = new Region(OutPath!))
            {
                g.SetClip(clipRegion, CombineMode.Replace);

                Color fill = BackColor;

                if (Hover)
                    fill = ControlPaint.Light(fill, 0.06f);

                if (Pressed)
                    fill = ControlPaint.Dark(fill, 0.06f);

                int offset = Pressed ? 1 : 0;
                g.TranslateTransform(0, offset);

                using (var brush = new SolidBrush(fill))
                    g.FillPath(brush, InPath!);

                int a = 70; 
                if (!Pressed)
                {
                    using var pen = new Pen(Color.FromArgb(a, ControlPaint.Light(fill, 0.4f)), 1);
                    g.DrawPath(pen, InPath!);
                }
                else
                {
                    using var pen = new Pen(Color.FromArgb(a, ControlPaint.Dark(fill, 0.4f)), 1);
                    g.DrawPath(pen, InPath!);
                }

                TextRenderer.DrawText(g, Text, Font, new Rectangle(0, 0, Width, Height), ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                g.ResetTransform();
                g.ResetClip();
            }
        }

        // =================
        //    Mouse Event
        // =================

        protected override void OnMouseEnter(EventArgs e) 
        {
            base.OnMouseEnter(e);
            Hover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e) 
        {
            base.OnMouseLeave(e);
            Hover = false;
            Pressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e) 
        {
            base.OnMouseDown(e);
            Pressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e) 
        {
            base.OnMouseUp(e);
            Pressed = false;
            Invalidate();
        }
    }
}