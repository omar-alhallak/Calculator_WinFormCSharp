using calculator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.core
{
    public class KeyboardController
    {
        private readonly CalculatorEngine Engine;

        public KeyboardController(CalculatorEngine engine)
        {
            Engine = engine;
        }

        public bool HandleKey(KeyEventArgs e)
        {
            // ======================
            //   NUMBERS - Top Row
            // ======================
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Shift)
            {
                char digit = (char)('0' + (e.KeyCode - Keys.D0));
                Engine.InputDigit(digit);
                return true;
            }

            // ======================
            //    NUMBERS - NumPad
            // ======================
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                char digit = (char)('0' + (e.KeyCode - Keys.NumPad0));
                Engine.InputDigit(digit);
                return true;
            }

            // ======================
            //          Dot
            // ======================
            if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
            {
                Engine.InputDot();
                return true;
            }

            //   ======================
            // OPERATORS - Shift (Top Row)
            //   ======================

                  // Shift + 8 => *
            if (e.KeyCode == Keys.D8 && e.Shift)
            {
                Engine.SelectOperator('*');
                return true;
            }

                 // Shift + = => +
            if (e.KeyCode == Keys.Oemplus && e.Shift)
            {
                Engine.SelectOperator('+');
                return true;
            }

            // - (top row)
            if (e.KeyCode == Keys.OemMinus && !e.Shift)
            {
                Engine.SelectOperator('-');
                return true;
            }

            // / (top row)
            if (e.KeyCode == Keys.OemQuestion)
            {
                Engine.SelectOperator('/');
                return true;
            }

            // ======================
            //   OPERATORS - NumPad
            // ======================
            if (e.KeyCode == Keys.Add)
            {
                Engine.SelectOperator('+');
                return true;
            }

            if (e.KeyCode == Keys.Subtract)
            {
                Engine.SelectOperator('-');
                return true;
            }

            if (e.KeyCode == Keys.Multiply)
            {
                Engine.SelectOperator('*');
                return true;
            }

            if (e.KeyCode == Keys.Divide)
            {
                Engine.SelectOperator('/');
                return true;
            }

            // ======================
            //        EQUALS
            // ======================

                // = (بدون Shift)
            if (e.KeyCode == Keys.Oemplus && !e.Shift)
            {
                Engine.Equals(out _);
                return true;
            }

                // Enter
            if (e.KeyCode == Keys.Enter)
            {
                Engine.Equals(out _);
                return true;
            }

            // ======================
            //      BACKSPACE
            // ======================
            if (e.KeyCode == Keys.Back)
            {
                Engine.Backspace();
                return true;
            }

            // ======================
            //         ESCAPE
            // ======================
            if (e.KeyCode == Keys.Escape)
            {
                Engine.ClearAll();
                return true;
            }

            return false;
        }
    }
}