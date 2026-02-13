using Calculator.Calculator.Core.Domain;
using Calculator.Calculator.Core.History;
using Calculator.Calculator.Core.Input;
using Calculator.Calculator.Core.enums;
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
        private readonly UndoRedoManager<CalculatorState> History = new(200);
        private readonly KeyboardController keyboard;

        public Calculator_des()
        {
            InitializeComponent();
            KeyPreview = true;
            keyboard = new KeyboardController(Engine, History);
            RefreshDisplay();
        }

        private void ExecuteHelper(Action action, bool recordUndo = true)
        {
            if (recordUndo)
                History.Push(Engine.Undo());

            action();
            RefreshDisplay();
        }

        private string GetErrorMessage(CalcError error) => error switch
        {
            CalcError.DivideByZero => "لا يمكنك القسمة على صفر",
            CalcError.InvalidExpression => "إدخال غير صالح",
            CalcError.Overflow => "الرقم كبير جدًا",
            CalcError.TooLong => "التعبير طويل جدًا",
            _ => "Error"
        };

        private void RefreshDisplay()
        {
            lblStorage.Text = Engine.TopLine;

            if (Engine.PreviewError != CalcError.None)
                txtResult.Text = GetErrorMessage(Engine.PreviewError);
            else
                txtResult.Text = Engine.BottomLine;
        }

        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;

            char d = b.Tag.ToString()![0];
            ExecuteHelper(() => Engine.InputDigit(d));
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            ExecuteHelper(() => Engine.InputDot());
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;

            char op = b.Tag.ToString()![0];
            ExecuteHelper(() => Engine.SelectOperator(op));
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            bool canRecord = Engine.PreviewError == CalcError.None;
            ExecuteHelper(() => Engine.Equals(out _), recordUndo: canRecord);
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            ExecuteHelper(() => Engine.Backspace());
        }
        private void btnAC_Click(object sender, EventArgs e)
        {
            ExecuteHelper(() => Engine.ClearAll());
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            ExecuteHelper(() => Engine.ClearEntry());
        }

        private void Calculator_des_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyboard.HandleKey(e))
            {
                RefreshDisplay();
                e.SuppressKeyPress = true;
            }
        } 
    }
}