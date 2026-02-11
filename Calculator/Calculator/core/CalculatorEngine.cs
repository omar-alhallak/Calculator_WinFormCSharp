using Calculator.core;
using System.Globalization;

namespace calculator.Core
{
    public class CalculatorEngine
    {
        private enum State
        {
            NumberFirst,
            OperatorSelected,
            ShowingResult
        }

        private readonly InputBuffer input = new InputBuffer();

        private State state = State.NumberFirst;

        private double Num1 = 0;                 // الرقم الأول
        private Operator? Oper = null;          // العملية المختارة

        public string TopLine { get; private set; } = "0";     // lblStorage
        public string BottomLine { get; private set; } = "0";  // txtResult

        // --------------------
        //       Helpers
        // --------------------
        private static string F(double v) => v.ToString("G15", CultureInfo.InvariantCulture);

        // ترجع الحاسبة لوضع "إدخال الرقم الأول"د
        private void ReturnStateFirstNum()
        {
            if (input.TryGetValue(out var v))
                Num1 = v;
            else
                Num1 = 0;

            TopLine = input.Text;
            BottomLine = input.Text;
            Oper = null;
            state = State.NumberFirst;
        }

        // تفعل عندما تختار عملية ولسا ما كتبت الرقم الثاني
        private void UpdateOperatorHeaderOnly()
        {
            var info = OperatorInfo.ByType[Oper!.Value];
            TopLine = F(Num1) + " " + info.DisplaySymbol;
            BottomLine = F(Num1);
        }

        // لعرض العملية فوق والناتج تحت
        private void UpdatePreview()
        {
            var info = OperatorInfo.ByType[Oper!.Value];

            string bText = input.Text;
            TopLine = F(Num1) + " " + info.DisplaySymbol + " " + bText;

            if (!input.TryGetValue(out var b))
            {
                BottomLine = bText;
                return;
            }

            if (Oper == Operator.Divide && b == 0)
            {
                BottomLine = "Error";
                return;
            }

            double r = info.Apply(Num1, b);
            BottomLine = F(r);
        }

        // مسؤلة عن العملية الحسابية بالكامل (=)زر
        private bool Evaluate(out string? error)
        {
            error = null;

            if (Oper is null)
                return true;

            double b;
            if (input.DidntStart)
            {
                b = Num1;
            }
            else if (!input.TryGetValue(out b))
            {
                b = Num1;
            }

            if (Oper == Operator.Divide && b == 0)
            {
                error = "Cannot divide by zero";
                BottomLine = "Error";
                return false;
            }

            var info = OperatorInfo.ByType[Oper.Value];
            double r = info.Apply(Num1, b);

            TopLine = F(Num1) + " " + info.DisplaySymbol + " " + F(b);
            BottomLine = F(r);

            Num1 = r;
            input.Set(BottomLine);
            Oper = null;
            state = State.ShowingResult;

            return true;
        }

        // --------------------
        //     Button API
        // --------------------
        public void ClearAll() // تصفر كلشي
        {
            input.Set("0");
            Num1 = 0;
            Oper = null;
            state = State.NumberFirst;
            TopLine = "0";
            BottomLine = "0";
        }

        public void InputDigit(char d) // عند أختيار أحد الأرقام
        {
            if (state == State.ShowingResult && Oper is null)
            {
                ClearAll();
            }

            input.InputDigit(d);

            if (state == State.OperatorSelected && Oper is not null)
            {
                UpdatePreview();
            }
            else
            {
                TopLine = input.Text;
                BottomLine = input.Text;
                state = State.NumberFirst;
            }
        }

        public void InputDot() // عند أختيار (.) نقطة 
        {
            if (state == State.ShowingResult && Oper is null)
            {
                ClearAll();
            }

            input.InputDot();

            if (state == State.OperatorSelected && Oper is not null)
            {
                UpdatePreview();
            }
            else
            {
                TopLine = input.Text;
                BottomLine = input.Text;
                state = State.NumberFirst;
            }
        }

        public void SelectOperator(char symbol) // عند أختيار أحد العمليات
        {
            if (!OperatorInfo.FromSymbol.TryGetValue(symbol, out var op))
                return;

            if (BottomLine == "Error")
                return;

            if (state == State.OperatorSelected && Oper is not null && input.DidntStart)
            {
                Oper = op;
                UpdateOperatorHeaderOnly();
                return;
            }

            if (input.TryGetValue(out var v))
                Num1 = v;

            Oper = op;
            state = State.OperatorSelected;
            input.InterNumber();
            UpdateOperatorHeaderOnly();
        }

        public bool Equals(out string? error) // حساب تلقائي
        {
            if (Oper is null)
            {
                error = null;
                state = State.ShowingResult;
                TopLine = TopLine; 
                BottomLine = input.Text;
                return true;
            }

            return Evaluate(out error);
        }

        public void Backspace() // حذف آخر عنصر
        {
            if (state == State.ShowingResult && Oper is null)
            {
                state = State.NumberFirst;
            }

            if (state == State.OperatorSelected && Oper is not null && input.DidntStart)
            {
                Oper = null;
                input.Set(F(Num1), false);
                ReturnStateFirstNum();
                return;
            }

            input.Backspace();

            if (state == State.OperatorSelected && Oper is not null)
            {
                if (input.DidntStart)
                {
                    UpdateOperatorHeaderOnly();
                    return;
                }

                UpdatePreview();
                return;
            }

            ReturnStateFirstNum();
        }
    }
}