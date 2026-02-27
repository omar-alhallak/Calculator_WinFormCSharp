using Calculator.Calculator.Application.Engine;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.Application.Input;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.Infrastructure.Input;
using Calculator.Calculator.Infrastructure.Persistence;


namespace Calculator.Calculator.UI
{
    public partial class Calculator_des : Form
    {
        private readonly CalculatorController Controller;
        private readonly KeyboardController Keyboard1 = new();
        private readonly HistoryService historyService;
        private History_des? HistoryForm;

        private readonly string HistoryPath = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.LocalApplicationData), "Calculator", "history.json");

        public Calculator_des()
        {
            InitializeComponent();

            KeyPreview = true;
            Directory.CreateDirectory(Path.GetDirectoryName(HistoryPath)!);
            var repo = new FileHistoryRepository(HistoryPath);
            historyService = new HistoryService(repo);
            var engine = new CalculatorEngine();
            var undo = new UndoRedoManager<CalculatorState>(200);
            var clipboard = new WinFormsClipboardService();
            var selection = new WinFormsSelectionService(this);
            Controller = new CalculatorController(engine, historyService, undo, clipboard, selection);
            Controller.Changed += () => { RefreshDisplay(); };
            RefreshDisplay();
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

        // Buttons -> Commands
        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is not System.Windows.Forms.Button b || b.Tag is null) return;
            Controller.Execute(new CalcCommand(CalcCommandType.Digit, b.Tag.ToString()![0]));
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Controller.Execute(new CalcCommand(CalcCommandType.Dot));
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is not System.Windows.Forms.Button b || b.Tag is null) return;
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
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.KeyCode) == Keys.H) 
            {
                btnHistory.PerformClick();
                return true;
            }

            if (Keyboard1.TryMap(keyData, out var cmd))
            {
                Controller.Execute(cmd);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Calculator_des_Load(object sender, EventArgs e)
        {
            MessageForHistory.ToolTipTitle = "السجل";
            MessageForHistory.BackColor = Color.FromArgb(30, 30, 30);
            MessageForHistory.SetToolTip(btnHistory, "الذاكرة (Ctrl+H)");
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (HistoryForm != null && !HistoryForm.IsDisposed) return;

            HistoryForm = new History_des(historyService);

            HistoryForm.FormClosed += (s, args) =>
            {
                if (HistoryForm?.SelectedEntry != null)
                    Controller.LoadExpression(HistoryForm.SelectedEntry.Expression);

                HistoryForm = null;
            };

            HistoryForm.Show(this);
        }
    }
}