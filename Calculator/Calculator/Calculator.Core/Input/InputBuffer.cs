using Calculator.Calculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Calculator.Core.Input
{
    public class InputBuffer // مسؤل عن إدخال الرقم الآتي
    {
        public string Text { get; private set; } = "0";
        public bool IsFresh { get; private set; } = true;

        private int CountDigits() // عدد الأرقام الفعلية داخل النص لنطبق الحد الموضوع للعمليات
        {
            int d = 0;
            foreach (char c in Text)
                if (char.IsDigit(c)) d++;
            return d;
        }

        public void BeginNew() => IsFresh = true; // ليأكد أن رقم تالي هو رقم

        public void Restore(string text, bool fresh) // تحدد إذا كان إدخال جديد أو لا
        {
            Text = string.IsNullOrWhiteSpace(text) ? "0" : text;
            IsFresh = fresh;
        }

        public void InputDigit(char digit)  
        {
            if (!char.IsDigit(digit)) return;

            if (!IsFresh && CountDigits() >= CalcLimits.MaxInputDigits) return;

            if (IsFresh)
            {
                Text = digit.ToString();
                IsFresh = false;
                return;
            }

            if (Text == "-")
            {
                Text += digit;  
                IsFresh = false;
                return;
            }

            if (Text == "0")
            {
                Text = digit.ToString();
                IsFresh = false;
                return;
            }

            Text += digit;
            IsFresh = false;
        }

        public void InputDot()
        {
            if (IsFresh)
            {
                Text = "0.";
                IsFresh = false;
                return;
            }

            if (Text == "-")
            {
                Text = "-0.";
                IsFresh = false;
                return;
            }

            if (!Text.Contains("."))
                Text += ".";
        }

        public void Backspace()
        {
            if (Text.Length <= 1)
            {
                Text = "0";
                IsFresh = true;
                return;
            }

            Text = Text.Substring(0, Text.Length - 1);

            if (Text == "-")
            {
                IsFresh = false;
                return;
            }

            if (Text.Length == 0)
            {
                Text = "0";
                IsFresh = true;
                return;
            }

            IsFresh = Text == "0";
        }

        public bool TryGetValue(out double value) // تحويل النص ل double
        {
            return double.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}