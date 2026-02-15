using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Calculator.Calculator.UI;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.Application.Input;
using Calculator.Calculator.Application.Engine;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.Infrastructure.Input;
using Calculator.Calculator.Infrastructure.Persistence;

namespace calculator
{
    public partial class Calculator_des : Form
    {
        private readonly CalculatorController Controller;
        private readonly KeyboardController Keyboard1 = new();
        private readonly HistoryService historyService;

        private readonly string _historyPath = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.LocalApplicationData), "Calculator", "history.json");

        private HistoryEntry? Last1;
        private HistoryEntry? Last2;

        public Calculator_des()
        {
            InitializeComponent();
            KeyPreview = true;
            Directory.CreateDirectory(Path.GetDirectoryName(_historyPath)!);
            var repo = new FileHistoryRepository(_historyPath);
            historyService = new HistoryService(repo);
            var engine = new CalculatorEngine();
            var undo = new UndoRedoManager<CalculatorState>(200);
            var clipboard = new WinFormsClipboardService();
            var selection = new WinFormsSelectionService(this);
            Controller = new CalculatorController(engine, historyService, undo, clipboard, selection);
            Controller.Changed += () =>
            {
                RefreshDisplay();
                UpdateHistoryLabels();
            };
            RefreshDisplay();
            UpdateHistoryLabels();
        }

        private string GetErrorMessage(CalcError error) => error switch
        {
            CalcError.DivideByZero => "لا يمكنك القسمة على صفر",
            CalcError.InvalidExpression => "إدخال غير صالح",
            CalcError.Overflow => "الرقم كبير جداً",
            CalcError.TooLong => "التعبير طويل جداً",
            _ => "Error"
        };

        private void RefreshDisplay()
        {
            txtStorage.Text = Controller.Engine.TopLine;
            txtStorage.SelectionStart = txtStorage.TextLength;
            txtStorage.ScrollToCaret();
            txtResult.Text = Controller.Engine.PreviewError != CalcError.None
                ? GetErrorMessage(Controller.Engine.PreviewError) : Controller.Engine.BottomLine;
        }

        private void UpdateHistoryLabels()
        {
            var lastTwo = historyService.GetLast(2);

            Last1 = lastTwo.Count > 0 ? lastTwo[0] : null;
            Last2 = lastTwo.Count > 1 ? lastTwo[1] : null;

            lblTopLine.Text = Last1 is null ? "" : $"{Last1.Expression} = {Last1.Result}";
            lblSecondLine.Text = Last2 is null ? "" : $"{Last2.Expression} = {Last2.Result}";
        }

        // Buttons -> Commands
        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;
            Controller.Execute(new CalcCommand(CalcCommandType.Digit, b.Tag.ToString()![0]));
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.Dot));
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;
            Controller.Execute(new CalcCommand(CalcCommandType.Operator, b.Tag.ToString()![0]));
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.Percent));
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.Equals));
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.Backspace));
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.ClearAll));
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.ClearEntry));
        }

        // Keyboard
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if (ArrowNavigator.TryMove(this, key))
                return true;

            if (Keyboard1.TryMap(keyData, out var cmd))
            {
                Controller.Execute(cmd);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Panel Paint
        private void PanelHistory_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.Gray))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                Rectangle rect = PanelHistory.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void lblTopLine_Click(object sender, EventArgs e)
        {
            if (Last1 is null) return;
            Controller.LoadExpression(Last1.Expression);
        }

        private void lblSecondLine_Click(object sender, EventArgs e)
        {
            if (Last2 is null) return;
            Controller.LoadExpression(Last2.Expression);
        }

        private void PanelHistory_Click(object sender, EventArgs e)
        {
            using var f = new History_des(historyService);
            if (f.ShowDialog(this) == DialogResult.OK && f.SelectedEntry != null)
            {
                Controller.LoadExpression(f.SelectedEntry.Expression);
            }
        }

        private void lblNameHistory_Click(object sender, EventArgs e)
        {
            using var f = new History_des(historyService);
            if (f.ShowDialog(this) == DialogResult.OK && f.SelectedEntry != null)
            {
                Controller.LoadExpression(f.SelectedEntry.Expression);
            }
        }
    }
}