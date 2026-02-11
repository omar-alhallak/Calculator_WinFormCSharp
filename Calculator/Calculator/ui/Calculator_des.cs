using calculator.Core;
using Calculator.core;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace calculator
{
    public partial class Calculator_des : Form
    {
        private readonly CalculatorEngine Engine = new CalculatorEngine();
        private KeyboardController Keyboardbtn;

        public Calculator_des()
        {
            InitializeComponent();
            this.KeyPreview = true;
            Keyboardbtn = new KeyboardController(Engine);
        }

        private void RefreshDisplay()
        {
            lblStorage.Text = Engine.TopLine;
            txtResult.Text = Engine.BottomLine;
        }

        private void HandleOperator(char symbol)
        {
            Engine.SelectOperator(symbol);
            RefreshDisplay();
        }

        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is Button b && b.Tag != null)
            {
                Engine.InputDigit(b.Tag.ToString()[0]);
                RefreshDisplay();
            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Engine.InputDot();
            RefreshDisplay();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Engine.Backspace();
            RefreshDisplay();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            Engine.ClearAll();
            RefreshDisplay();
        }

        private void Calculator_des_KeyDown(object sender, KeyEventArgs e)
        {
           if(Keyboardbtn.HandleKey(e))
            {
                RefreshDisplay();
                e.SuppressKeyPress = true;
            }
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is Button b && b.Tag != null)
            {
                HandleOperator(b.Tag.ToString()[0]);
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (!Engine.Equals(out var error))
            {
                txtResult.Text = "Error";
                return;
            }
            RefreshDisplay();
        }
    }
}