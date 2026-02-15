using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Calculator.Calculator.Infrastructure.Input
{
    public static class ArrowNavigator // تفعيل الأسهم
    {
        private const double AlignWeight = 2.5;

        public static bool TryMove(Form form, Keys key) // المسؤلة عن عمل الأسهم
        {
            if (key is not (Keys.Left or Keys.Right or Keys.Up or Keys.Down)) return false;

            var current = form.ActiveControl as Control;
            if (current is null) return false;

            if (current is TextBoxBase) return false;

            var focusables = GetFocusableControls(form).ToList();
            if (focusables.Count == 0) return true; 

            var centers = focusables.ToDictionary(c => c, CenterOnScreen);
            var cur = centers[current];
            var directional = FindDirectionalTargets(key, focusables, centers, current, cur).ToList();

            Control? best = null;

            if (directional.Count > 0)
                best = ChooseBestTarget(key, directional, centers, cur);
            else   
                best = PickWrapTarget(key, focusables, centers, current, cur);

            if (best is not null)
                best.Focus();

            return true;
        }

        private static IEnumerable<Control> FindDirectionalTargets // تحدد الأتجاه المطلول الذهاب إليه
            (
            Keys key,
            List<Control> focusables,
            Dictionary<Control, Point> centers,
            Control current,
            Point cur
            ) {
            foreach (var c in focusables)
            {
                if (c == current) continue;
                var p = centers[c];

                bool ok = key switch
                {
                    Keys.Left => p.X < cur.X - 1,
                    Keys.Right => p.X > cur.X + 1,
                    Keys.Up => p.Y < cur.Y - 1,
                    Keys.Down => p.Y > cur.Y + 1,
                    _ => false
                };

                if (ok) yield return c;
            }
        }

        private static Control ChooseBestTarget // تحسب شو أفضل أتجاه ممكن ترحله
            (
            Keys key,
            IEnumerable<Control> candidates,
            Dictionary<Control, Point> centers,
            Point cur
            ) {
            Control? best = null;
            double bestScore = double.MaxValue;

            foreach (var c in candidates)
            {
                var p = centers[c];
                int dx = p.X - cur.X;
                int dy = p.Y - cur.Y;

                double alignPenalty = key switch
                {
                    Keys.Up or Keys.Down => Math.Abs(dx) * AlignWeight,
                    Keys.Left or Keys.Right => Math.Abs(dy) * AlignWeight,
                    _ => 0
                };

                double dist = Math.Sqrt(dx * dx + dy * dy);
                double score = dist + alignPenalty;

                if (score < bestScore)
                {
                    bestScore = score;
                    best = c;
                }
            }

            return best!;
        }

        private static Control? PickWrapTarget // إذا وصل سهم لإخر نقطة ترجعه للبداية
            (
            Keys key,
            List<Control> focusables,
            Dictionary<Control, Point> centers,
            Control current,
            Point cur
            ) {
            var others = focusables.Where(c => c != current).ToList();
            if (others.Count == 0) return null;

            int minY = others.Min(c => centers[c].Y);
            int maxY = others.Max(c => centers[c].Y);
            int minX = others.Min(c => centers[c].X);
            int maxX = others.Max(c => centers[c].X);

            IEnumerable<Control> edge = key switch
            {
                Keys.Up => others.Where(c => centers[c].Y == maxY),
                Keys.Down => others.Where(c => centers[c].Y == minY),
                Keys.Left => others.Where(c => centers[c].X == maxX),
                Keys.Right => others.Where(c => centers[c].X == minX),
                _ => Enumerable.Empty<Control>()
            };

            Control? best = null;
            int bestAlign = int.MaxValue;

            foreach (var c in edge)
            {
                var p = centers[c];
                int align = key switch
                {
                    Keys.Up or Keys.Down => Math.Abs(p.X - cur.X),
                    Keys.Left or Keys.Right => Math.Abs(p.Y - cur.Y),
                    _ => int.MaxValue
                };

                if (align < bestAlign)
                {
                    bestAlign = align;
                    best = c;
                }
            }

            return best;
        }

        private static Point CenterOnScreen(Control c) // تحدد مركز الشاشة
        {
            var topLeftOnScreen = c.Parent?.PointToScreen(new Point(c.Left, c.Top)) ?? new Point(c.Left, c.Top);

            return new Point(topLeftOnScreen.X + c.Width / 2, topLeftOnScreen.Y + c.Height / 2);
        }

        private static IEnumerable<Control> GetFocusableControls(Control root) // لتحدد مين العناصر الي حتتنقل عليها
        {
            foreach (Control c in root.Controls)
            {
                if (c.TabStop && c.CanSelect && c.Enabled && c.Visible) yield return c;

                foreach (var child in GetFocusableControls(c)) yield return child;
            }
        }
    }
}