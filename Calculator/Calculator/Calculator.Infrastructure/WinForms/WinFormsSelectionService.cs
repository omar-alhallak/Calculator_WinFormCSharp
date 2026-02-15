using System;
using System.Windows.Forms;
using Calculator.Calculator.Application.InterFaces;

namespace Calculator.Calculator.Infrastructure.Input
{
    public sealed class WinFormsSelectionService : ISelectionService
    {
        private readonly Form Form1;

        public WinFormsSelectionService(Form form)
        {
            Form1 = form;
        }

        public bool HasSelection()
        {
            return Form1.ActiveControl is TextBoxBase tb && tb.SelectionLength > 0;
        }

        public string GetSelectedText()
        {
            if (Form1.ActiveControl is TextBoxBase tb && tb.SelectionLength > 0) return tb.SelectedText;

            return "";
        }
    }
}