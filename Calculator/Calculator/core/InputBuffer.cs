using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.core
{
    public class InputBuffer
    {
        public string Text { get; private set; } = "0";
        public bool DidntStart { get; private set; } = true;

        private const int MaxDigits = 16;

        private int CountDigits() // لمعرفة كم رقم في نص
        {
            int d = 0;
            foreach (char c in Text)
                if (char.IsDigit(c)) d++;
            return d;
        }

        public void InterNumber() // عند أستدعائها تجبر على إدخال رقم
        {
            DidntStart = true;
        }

        public void Set(string text, bool Start = true) // تحدد القيمة الأفتراضبة للنص 
        {
            Text = string.IsNullOrWhiteSpace(text) ? "0" : text;
            DidntStart = Start;
        }

        public void InputDigit(char digit) // تتأكد أن المدخل رقم
        {
            if (!char.IsDigit(digit)) return;

            if (!DidntStart && CountDigits() >= MaxDigits) return;

            if (DidntStart)
            {
                Text = digit.ToString();
                DidntStart = false;
                return;
            }

            if (Text == "0")
            {
                Text = digit.ToString();
                DidntStart = false;
                return;
            }

            Text += digit;
            DidntStart = false;
        }

        public void InputDot() // إذا أدخل . بالبداية 
        {
            if (DidntStart)
            {
                Text = "0.";
                DidntStart = false;
                return;
            }

            if (!Text.Contains("."))
                Text += ".";
        }

        public void Backspace() // زر الحذف
        {
            if (Text.Length <= 1)
            {
                Text = "0";
                DidntStart = true;
                return;
            }

            Text = Text.Substring(0, Text.Length - 1);

            if (Text == "-" || Text.Length == 0)
            {
                Text = "0";
                DidntStart = true;
                return;
            }

            DidntStart = (Text == "0");
        }

        public bool TryGetValue(out double value) // تحويل للأمان
        {
            return double.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}