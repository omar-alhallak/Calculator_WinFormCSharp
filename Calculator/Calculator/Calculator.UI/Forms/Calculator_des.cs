using System;
using System.Windows.Forms;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.UI.Forms.Coordinator;

namespace Calculator.Calculator.UI.Forms
{
    public partial class Calculator_des : Form
    {
        private readonly CalculatorComposition Composition;
        private readonly CalculatorUICoordinator UI;

        public Calculator_des()
        {
            InitializeComponent();
            KeyPreview = true;
            Composition = new CalculatorComposition(this);

            UI = new CalculatorUICoordinator(this, Composition.Controller, Composition.History,
                Composition.HistoryView, new SlideUpAnimator());
        }

        public Button HistoryButton => btnHistory;
        public ToolTip HistoryToolTip => MessageForHistory;
        public RichTextBox StorageBox => txtStorage;
        public RichTextBox ResultBox => txtResult;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            UI.Dispose();
            base.OnFormClosed(e);
        }

// --------------------------
//        Buttons
// --------------------------
        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;
            UI.CloseFromButton(new CalcCommand(CalcCommandType.Digit, b.Tag.ToString()![0]));
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;
            UI.CloseFromButton(new CalcCommand(CalcCommandType.Operator, b.Tag.ToString()![0]));
        }

        private void btnDot_Click(object sender, EventArgs e) => UI.CloseFromButton(new CalcCommand(CalcCommandType.Dot));
        private void btnPercent_Click(object sender, EventArgs e) => UI.CloseFromButton(new CalcCommand(CalcCommandType.Percent));
        private void btnEquals_Click(object sender, EventArgs e) => UI.CloseFromButton(new CalcCommand(CalcCommandType.Equals));
        private void btnBackSpace_Click(object sender, EventArgs e) => UI.CloseFromButton(new CalcCommand(CalcCommandType.Backspace));
        private void btnAC_Click(object sender, EventArgs e) => UI.CloseFromButton(new CalcCommand(CalcCommandType.ClearAll));
        private void btnCE_Click(object sender, EventArgs e) => UI.CloseFromButton(new CalcCommand(CalcCommandType.ClearEntry));
        private void btnHistory_Click(object sender, EventArgs e) => UI.OpenOrCloseHistory();

// --------------------------
//        Keyboard 
// --------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var r = UI.RouteKey(keyData);

            if (r == ProssesKeyboardInput.Handled)
                return true;

            if (r == ProssesKeyboardInput.PassToBase)
                return base.ProcessCmdKey(ref msg, keyData);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Calculator_des_Load(object sender, EventArgs e)
        {
            UI.MassegeForHistoryOpen();
        }
    }
}