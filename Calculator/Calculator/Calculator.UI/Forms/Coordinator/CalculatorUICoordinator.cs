using System;
using System.Windows.Forms;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.Application.Input;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.UI.Tools.HistoryHelper;
using Calculator.Calculator.UI.Animations.TooleAnimations;

namespace Calculator.Calculator.UI.Forms.Coordinator
{
    public sealed class CalculatorUICoordinator : IDisposable // المنسق بين اليوأي و باقي الطبقات
    {
        private readonly Calculator_des Form;
        private readonly CalculatorController Controller;
        private readonly KeyboardController Keyboard = new();
        private readonly HistoryService History;

        private readonly SlideUpAnimator HistoryAnim;
        private readonly HistoryPanelController HistoryUi;
        private readonly CloseOutSideClicks HistoryCloser;
        private readonly HistoryView HistoryView;

        private readonly Action OnControllerChanged;
        private readonly Action OnHistoryChanged;
        private readonly Action<HistoryEntry> OnItemChosen;

        public CalculatorUICoordinator(Calculator_des form, CalculatorController controller,
            HistoryService history, HistoryView historyView, SlideUpAnimator historyAnim)
        {
            Form = form ?? throw new ArgumentNullException(nameof(form));
            Controller = controller ?? throw new ArgumentNullException(nameof(controller));
            History = history ?? throw new ArgumentNullException(nameof(history));
            HistoryView = historyView ?? throw new ArgumentNullException(nameof(historyView));
            HistoryAnim = historyAnim ?? throw new ArgumentNullException(nameof(historyAnim));

            PrepareHistoryView(HistoryView);

            var historyUi = new HistoryPanelController(Form, HistoryView, HistoryAnim);
            HistoryUi = historyUi;

            HistoryCloser = new CloseOutSideClicks(Form, HistoryView, isOpen: () => historyUi.IsOpen, closeAction
                : () => historyUi.Close()).Ignore(Form.HistoryButton);

            HistoryCloser.Start();
            OnControllerChanged = RefreshDisplaySafe;
            Controller.Changed += OnControllerChanged;

            OnItemChosen = entry =>
            {
                Controller.LoadExpression(entry.Expression);
                historyUi.Close();
            };

            HistoryView.ItemChosen += OnItemChosen;
            OnHistoryChanged = () => Form.SafeBeginInvoke(HistoryView.RefreshNow);
            History.Changed += OnHistoryChanged;

            RefreshDisplay();
            MassegeForHistoryOpen();
        }

// ==========================
//       History Seting
// ==========================

        private void PrepareHistoryView(Control hv) // تجهيز الهيستوري فيو قبل العرض
        {
            hv.Left = 0;
            hv.Width = Form.ClientSize.Width;
            hv.Top = Form.ClientSize.Height;
            hv.Visible = false;
            hv.Height = 0;
            Form.Controls.Add(hv);
            hv.BringToFront();
        }

        public void OpenOrCloseHistory() // يفتح ويسكر الفيو داخل الفورم
        {
            int targetHeight = (int)(Form.ClientSize.Height * 0.75);
            HistoryUi.Toggle(targetHeight);
        }

        public void CloseHistoryIfOpen() // إغلاق الهيستوري إن كانت مفتوحة
        {
            if (HistoryUi.IsOpen)
                HistoryUi.Close();
        }

        public void CloseFromButton(CalcCommand cmd) // يسكر الهيستوري عند ضغط أي زر بالحاسبة
        {
            CloseHistoryIfOpen();
            Controller.Execute(cmd);
        }

        public ProssesKeyboardInput RouteKey(Keys keyData) // يتحكم بعمل الأزرار الخاصة ب الهيستوري 
        {
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.KeyCode) == Keys.H) 
            {
                OpenOrCloseHistory();
                return ProssesKeyboardInput.Handled;
            }

            if (HistoryUi.IsOpen)
            {
                if (keyData == Keys.Up || keyData == Keys.Down)
                    return ProssesKeyboardInput.PassToBase;

                if (keyData == Keys.Left)
                {
                    HistoryUi.View.FocusList();
                    HistoryUi.View.ScrollHorizontal(-40);
                    return ProssesKeyboardInput.Handled;
                }

                if (keyData == Keys.Right)
                {
                    HistoryUi.View.FocusList();
                    HistoryUi.View.ScrollHorizontal(40);
                    return ProssesKeyboardInput.Handled;
                }

                if (keyData == Keys.Delete || keyData == Keys.Back)
                {
                    HistoryUi.View.DeleteSelected();
                    return ProssesKeyboardInput.Handled;
                }

                if (keyData == Keys.Escape)
                {
                    HistoryUi.View.ClearAll();
                    return ProssesKeyboardInput.Handled;
                }
            }

            if (Keyboard.TryMap(keyData, out var cmd))
            {
                CloseHistoryIfOpen();
                Controller.Execute(cmd);
                return ProssesKeyboardInput.Handled;
            }

            return ProssesKeyboardInput.NotHandled;
        }

        public void MassegeForHistoryOpen() // رسالة فتح الهيستوري
        {
            Form.HistoryToolTip.ToolTipTitle = ":السجل";
            Form.HistoryToolTip.SetToolTip(Form.HistoryButton, "(Ctrl + H) المحفوظات");
        }

// --------------------- DISPLAY & Clean ---------------------

        private void RefreshDisplaySafe() // تحديث أمن
        {
            if (Form.IsDisposed) return;
            Form.SafeBeginInvoke(RefreshDisplay);
        }

        private void RefreshDisplay() // تحديث شاشات العرض
        {
            Form.StorageBox.Text = Controller.Engine.TopLine;
            Form.StorageBox.SelectionStart = Form.StorageBox.TextLength;
            Form.StorageBox.ScrollToCaret();

            Form.ResultBox.Text = Controller.Engine.PreviewError != CalcError.None
                    ? GetErrorMessage(Controller.Engine.PreviewError) : Controller.Engine.BottomLine;
        }

        private static string GetErrorMessage(CalcError error) => error switch // رسائل الخطأ
        {
            CalcError.DivideByZero => "لا يمكنك القسمة على صفر",
            CalcError.InvalidExpression => "إدخال غير صالح",
            CalcError.Overflow => "الرقم كبير جداً",
            CalcError.TooLong => "التعبير طويل جداً",
            _ => "Error"
        };

        public void Dispose() // تنظيف
        {
            History.Changed -= OnHistoryChanged;
            HistoryView.ItemChosen -= OnItemChosen;
            Controller.Changed -= OnControllerChanged;

            HistoryCloser.Dispose();
            HistoryAnim.Dispose();
        }
    }
}