using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator.Calculator.UI.Tools.HistoryHelper
{
    public sealed class HistoryListDrawer // كسؤول عن رسم الفيو داخل الهيستوري
    {
        private readonly ListView List;
        private bool Attached;

        public Color SelectedBackColor { get; set; } = Color.FromArgb(70, 255, 165, 0);
        public Color SelectedTextColor { get; set; } = Color.White;

        public Color RowSeparatorColor { get; set; } = Color.FromArgb(120, 255, 255, 255);
        public int RowSeparatorThickness { get; set; } = 1;

        public HistoryListDrawer(ListView list)
        {
            List = list ?? throw new ArgumentNullException(nameof(list));
        }

        public void AttachOnce()
        {
            if (Attached) return;

            List.OwnerDraw = true;
            List.DrawItem += (_, e) => e.DrawBackground();
            List.DrawSubItem += DrawRow;
            List.DrawColumnHeader += (_, e) => e.DrawBackground();
            Attached = true;
        }

        private void DrawRow(object? sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            if (e.Item == null) return;

            bool selected = e.Item.Selected;

            using (var bg = new SolidBrush(selected ? SelectedBackColor : List.BackColor)) e.Graphics.FillRectangle(bg, e.Bounds);

            string text = e.SubItem?.Text ?? e.Item.Text ?? "";
            TextRenderer.DrawText(e.Graphics, text, List.Font, e.Bounds, selected ? SelectedTextColor
                : List.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            using var pen = new Pen(RowSeparatorColor, RowSeparatorThickness);
            e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
        }
    }
}