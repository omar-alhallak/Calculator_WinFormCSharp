using Calculator.Calculator.Core.Domain;
using Calculator.Calculator.Core.Evaluation;
using Calculator.Calculator.Core.History;
using Calculator.Calculator.Core.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Calculator.Core.Input
{
    public class KeyboardController // لتفعيل أزرار الكمبيوتر
    {
        private readonly CalculatorEngine Engine;
        private readonly UndoRedoManager<CalculatorState> History;

        public KeyboardController(CalculatorEngine engine,UndoRedoManager<CalculatorState> history)
        {
            Engine = engine;
            History = history;
        }

        public bool HandleKey(KeyEventArgs e) 
        {
            // ==========================
            //       Undo / Redo
            // ==========================
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (History.TryUndo(Engine.Undo(), out var prev))
                    Engine.Redo(prev);
                return true;
            }

            if (e.Control && e.KeyCode == Keys.Y)
            {
                if (History.TryRedo(Engine.Undo(), out var next))
                    Engine.Redo(next);
                return true;
            }

            // ==========================
            //       Ctrl Shortcuts
            // ==========================

            // Ctrl + Backspace = ClearEntry
            if (e.Control && e.KeyCode == Keys.Back)
            {
                History.Push(Engine.Undo());
                Engine.ClearEntry();
                return true;
            }

            // Ctrl + Delete = ClearAll
            if (e.Control && e.KeyCode == Keys.Delete)
            {
                History.Push(Engine.Undo());
                Engine.ClearAll();
                return true;
            }

            // ==========================
            //      Clear / Delete
            // ==========================

            // Esc = Clear All
            if (e.KeyCode == Keys.Escape)
            {
                History.Push(Engine.Undo());
                Engine.ClearAll();
                return true;
            }

            // Delete = Clear Entry
            if (e.KeyCode == Keys.Delete)
            {
                History.Push(Engine.Undo());
                Engine.ClearEntry();
                return true;
            }

            // Backspace = حذف حرف
            if (e.KeyCode == Keys.Back)
            {
                History.Push(Engine.Undo());
                Engine.Backspace();
                return true;
            }

            // ==========================
            //           Equals
            // ==========================

            // Enter
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (Engine.PreviewError == CalcError.None)
                    History.Push(Engine.Undo());

                Engine.Equals(out _);
                return true;
            }

            // '=' بدون Shift
            if (e.KeyCode == Keys.Oemplus && !e.Shift)
            {
                if (Engine.PreviewError == CalcError.None)
                    History.Push(Engine.Undo());

                Engine.Equals(out _);
                return true;
            }

            // ===============
            //       Dot
            // ==============
            if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)
            {
                History.Push(Engine.Undo());
                Engine.InputDot();
                return true;
            }

            // ==========================
            //      Digits (Top Row)
            // ==========================
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Shift)
            {
                char digit = (char)('0' + (e.KeyCode - Keys.D0));
                History.Push(Engine.Undo());
                Engine.InputDigit(digit);
                return true;
            }

            // ==========================
            //      Digits (NumPad)
            // ==========================
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                char digit = (char)('0' + (e.KeyCode - Keys.NumPad0));
                History.Push(Engine.Undo());
                Engine.InputDigit(digit);
                return true;
            }

            // ==========================
            //    Operators - NumPad
            // ==========================
            if (e.KeyCode == Keys.Add)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('+');
                return true;
            }

            if (e.KeyCode == Keys.Subtract)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('-');
                return true;
            }

            if (e.KeyCode == Keys.Multiply)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('*');
                return true;
            }

            if (e.KeyCode == Keys.Divide)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('/');
                return true;
            }

            // ==========================
            //         % Percent
            // ==========================
            if (e.KeyCode == Keys.D5 && e.Shift)
            {
                History.Push(Engine.Undo());
                Engine.ApplyPercent();
                return true;
            }

            // ==========================
            //    Operators - Top Row
            // ==========================

            // Shift + =  => +
            if (e.KeyCode == Keys.Oemplus && e.Shift)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('+');
                return true;
            }

            // -
            if (e.KeyCode == Keys.OemMinus)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('-');
                return true;
            }

            // Shift + 8  => *
            if (e.KeyCode == Keys.D8 && e.Shift)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('*');
                return true;
            }

            // /
            if (e.KeyCode == Keys.OemQuestion)
            {
                History.Push(Engine.Undo());
                Engine.SelectOperator('/');
                return true;
            }

            return false;
        }
    }
}