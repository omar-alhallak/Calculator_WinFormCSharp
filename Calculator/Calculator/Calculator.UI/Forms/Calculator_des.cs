using System;
using Calculator.Calculator.UI.Tools.HistoryView;
using Calculator.Calculator.Application.Engine;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.Application.Input;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.Infrastructure.Input;
using Calculator.Calculator.Infrastructure.Persistence;
using Calculator.Calculator.UI.Animations.TooleAnimations;

namespace Calculator.Calculator.UI.Forms
{
    public partial class Calculator_des : Form
    {
        private readonly CalculatorController Controller;
        private readonly KeyboardController Keyboard1 = new();
        private readonly HistoryService historyService;

        private readonly SlideUpAnimator _historyAnim = new();
        private HistoryPanelController? _historyUi;
        private CloseOutSideClicks? _historyCloser;

        private readonly string HistoryPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "Calculator", "history.json");

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
            Controller.Changed += RefreshDisplay;

            var hv = new HistoryView();
            hv.SetHistory(historyService);

            hv.Left = 0;
            hv.Width = ClientSize.Width;
            hv.Top = ClientSize.Height;
            hv.Visible = false;
            hv.Height = 0;

            Controls.Add(hv);
            hv.BringToFront();

            hv.ItemChosen += entry =>
            {
                Controller.LoadExpression(entry.Expression);
                _historyUi?.Close();
            };

            historyService.Changed += () =>
            {
                if (!IsDisposed)
                    BeginInvoke(new Action(hv.RefreshNow));
            };

            _historyUi = new HistoryPanelController(this, hv, _historyAnim);

            _historyCloser = new CloseOutSideClicks(
                this,
                hv,
                isOpen: () => _historyUi != null && _historyUi.IsOpen,
                closeAction: () => _historyUi?.Close()
            ).Ignore(btnHistory);

            _historyCloser.Start();

            RefreshDisplay();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _historyCloser?.Dispose();
            _historyAnim.Dispose();
            base.OnFormClosed(e);
        }

        private void RefreshDisplay()
        {
            txtStorage.Text = Controller.Engine.TopLine;
            txtStorage.SelectionStart = txtStorage.TextLength;
            txtStorage.ScrollToCaret();

            txtResult.Text = Controller.Engine.PreviewError != CalcError.None
                ? GetErrorMessage(Controller.Engine.PreviewError)
                : Controller.Engine.BottomLine;
        }

        private static string GetErrorMessage(CalcError error) => error switch
        {
            CalcError.DivideByZero => "لا يمكنك القسمة على صفر",
            CalcError.InvalidExpression => "إدخال غير صالح",
            CalcError.Overflow => "الرقم كبير جداً",
            CalcError.TooLong => "التعبير طويل جداً",
            _ => "Error"
        };

        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;
            CloseHistoryIfOpen();
            Controller.Execute(new CalcCommand(CalcCommandType.Digit, b.Tag.ToString()![0]));
        }

        private void Op_Click(object sender, EventArgs e)
        {
            if (sender is not Button b || b.Tag is null) return;
            CloseHistoryIfOpen(); 
            Controller.Execute(new CalcCommand(CalcCommandType.Operator, b.Tag.ToString()![0]));
        }

        private void btnDot_Click(object sender, EventArgs e) { CloseHistoryIfOpen(); Controller.Execute(new CalcCommand(CalcCommandType.Dot)); }
        private void btnPercent_Click(object sender, EventArgs e) { CloseHistoryIfOpen(); Controller.Execute(new CalcCommand(CalcCommandType.Percent)); }
        private void btnEquals_Click(object sender, EventArgs e) { CloseHistoryIfOpen(); Controller.Execute(new CalcCommand(CalcCommandType.Equals)); }
        private void btnBackSpace_Click(object sender, EventArgs e) { CloseHistoryIfOpen(); Controller.Execute(new CalcCommand(CalcCommandType.Backspace)); }
        private void btnAC_Click(object sender, EventArgs e) { CloseHistoryIfOpen(); Controller.Execute(new CalcCommand(CalcCommandType.ClearAll)); }
        private void btnCE_Click(object sender, EventArgs e) { CloseHistoryIfOpen(); Controller.Execute(new CalcCommand(CalcCommandType.ClearEntry)); }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            int targetHeight = (int)(ClientSize.Height * 0.75);
            _historyUi?.Toggle(targetHeight);
        }

        private void CloseHistoryIfOpen()
        {
            if (_historyUi != null && _historyUi.IsOpen)
                _historyUi.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.KeyCode) == Keys.H)
            {
                btnHistory.PerformClick();
                return true;
            }

            if (_historyUi != null && _historyUi.IsOpen)
            {
                if (keyData == Keys.Up || keyData == Keys.Down)
                    return base.ProcessCmdKey(ref msg, keyData);

                if (keyData == Keys.Left)
                {
                    _historyUi.View.FocusList();
                    _historyUi.View.ScrollHorizontal(-40);
                    return true;
                }

                if (keyData == Keys.Right)
                {
                    _historyUi.View.FocusList();
                    _historyUi.View.ScrollHorizontal(40);
                    return true;
                }

                if (keyData == Keys.Delete || keyData == Keys.Back)
                {
                    _historyUi.View.DeleteSelected();
                    return true;
                }

                if (keyData == Keys.Escape)
                {
                    _historyUi.View.ClearAll();
                    return true;
                }
            }

            if (Keyboard1.TryMap(keyData, out var cmd))
            {
                Controller.Execute(cmd);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}