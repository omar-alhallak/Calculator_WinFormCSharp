using calculator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace calculator
{
    public partial class Calculator_des : Form
    {
        private readonly InputNumber _Num = new InputNumber();
        private Operator? _selectedOp = null;

        public Calculator_des()
        {
            InitializeComponent();
        }

        private void RefreshDisplay()
        {
            txtResult.Text = _Num.Text;
        }

        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is Button b && b.Tag != null)
            {
                _Num.InputDigit(b.Tag.ToString()[0]);
                RefreshDisplay();
            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            _Num.InputDecimal();
            RefreshDisplay();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _Num.Backspace();
            RefreshDisplay();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            _Num.ClearEntry();
            RefreshDisplay();
        }

        private void Calculator_des_KeyDown(object sender, KeyEventArgs e)
        {
            // أرقام الصف العلوي
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                char digit = (char)('0' + (e.KeyCode - Keys.D0));
                _Num.InputDigit(digit);
                RefreshDisplay();
                e.SuppressKeyPress = true;
                return;
            }

            // أرقام النمباد
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                char digit = (char)('0' + (e.KeyCode - Keys.NumPad0));
                _Num.InputDigit(digit);
                RefreshDisplay();
                e.SuppressKeyPress = true;
                return;
            }

            // Dot
            if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)
            {
                _Num.InputDecimal();
                RefreshDisplay();
                e.SuppressKeyPress = true;
                return;
            }

            // Backspace
            if (e.KeyCode == Keys.Back)
            {
                _Num.Backspace();
                RefreshDisplay();
                e.SuppressKeyPress = true;
                return;
            }

            // Escape = Clear
            if (e.KeyCode == Keys.Escape)
            {
                _Num.ClearEntry();
                RefreshDisplay();
                e.SuppressKeyPress = true;
            }
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;

            char symbol = b.Tag.ToString()[0];

            if (!OperatorInfo.FromSymbol.TryGetValue(symbol, out var op))
                return;

            _selectedOp = op;
            txtResult.Text += OperatorInfo.ByType[op].DisplaySymbol;
        }
    }
}