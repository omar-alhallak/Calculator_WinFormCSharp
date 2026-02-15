using System;
using Calculator.Calculator.Domain.enums;

namespace Calculator.Calculator.Application.Input
{
    public sealed class KeyboardController // لتفعيل أزرار الكمبيوتر
    {
        public bool TryMap(Keys keyData, out CalcCommand cmd)
        {
            cmd = default;

            Keys key = keyData & Keys.KeyCode;
            bool ctrl = (keyData & Keys.Control) == Keys.Control;
            bool shift = (keyData & Keys.Shift) == Keys.Shift;

            // Undo / Redo
            if (ctrl && key == Keys.Z) { cmd = new(CalcCommandType.Undo); return true; }
            if (ctrl && key == Keys.Y) { cmd = new(CalcCommandType.Redo); return true; }

            // Copy / Paste
            if (ctrl && key == Keys.C) { cmd = new(CalcCommandType.Copy); return true; }
            if (ctrl && key == Keys.V) { cmd = new(CalcCommandType.Paste); return true; }

            // Ctrl shortcuts
            if (ctrl && key == Keys.Back) { cmd = new(CalcCommandType.ClearEntry); return true; }
            if (ctrl && key == Keys.Delete) { cmd = new(CalcCommandType.ClearAll); return true; }

            // Clear / Backspace
            if (key == Keys.Escape) { cmd = new(CalcCommandType.ClearAll); return true; }
            if (key == Keys.Delete) { cmd = new(CalcCommandType.ClearEntry); return true; }
            if (key == Keys.Back) { cmd = new(CalcCommandType.Backspace); return true; }

            // Equals: Enter و =
            if (key == Keys.Enter || key == Keys.Return)
            {
                cmd = new(CalcCommandType.Equals);
                return true;
            }

            // '=' بدون Shift
            if (key == Keys.Oemplus && !shift)
            {
                cmd = new(CalcCommandType.Equals);
                return true;
            }

            // Percent: Shift+5  => %
            if (key == Keys.D5 && shift)
            {
                cmd = new(CalcCommandType.Percent);
                return true;
            }

            // Dot
            if (key == Keys.Decimal || key == Keys.OemPeriod)
            {
                cmd = new(CalcCommandType.Dot);
                return true;
            }

            // Digits top row (بدون Shift)
            if (key >= Keys.D0 && key <= Keys.D9 && !shift)
            {
                char d = (char)('0' + (key - Keys.D0));
                cmd = new(CalcCommandType.Digit, d);
                return true;
            }

            // Digits numpad
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                char d = (char)('0' + (key - Keys.NumPad0));
                cmd = new(CalcCommandType.Digit, d);
                return true;
            }

            // Operators (numpad)
            if (key == Keys.Add) { cmd = new(CalcCommandType.Operator, '+'); return true; }
            if (key == Keys.Subtract) { cmd = new(CalcCommandType.Operator, '-'); return true; }
            if (key == Keys.Multiply) { cmd = new(CalcCommandType.Operator, '*'); return true; }
            if (key == Keys.Divide) { cmd = new(CalcCommandType.Operator, '/'); return true; }

            // Operators (top row)
            if (key == Keys.Oemplus && shift) { cmd = new(CalcCommandType.Operator, '+'); return true; } // Shift+=
            if (key == Keys.OemMinus) { cmd = new(CalcCommandType.Operator, '-'); return true; }         // -
            if (key == Keys.D8 && shift) { cmd = new(CalcCommandType.Operator, '*'); return true; }      // Shift+8
            if (key == Keys.OemQuestion) { cmd = new(CalcCommandType.Operator, '/'); return true; }      // /

            return false;
        }
    }
}