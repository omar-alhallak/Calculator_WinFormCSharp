using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core
{
    public class InputNumber
    {
        public string Text { get; private set; } = "0";

        private bool _isNewEntry = true;
        private const int MaxDigits = 16;

        private int CountDigits()
        {
            int digits = 0;
            foreach (char c in Text)
                if (char.IsDigit(c)) digits++;
            return digits;
        }

        public void StartNewEntry()
        {
            _isNewEntry = true;
        }

        public void InputDigit(char digit)
        {
            if (!char.IsDigit(digit)) return;
 
            if (!_isNewEntry && CountDigits() >= MaxDigits) return;

            if (_isNewEntry || Text == "0")
            {
                Text = digit.ToString();
                _isNewEntry = false;
                return;
            }

            Text += digit;
        }

        public void InputDecimal()
        {
            if (_isNewEntry)
            {
                Text = "0.";
                _isNewEntry = false;
                return;
            }

            if (!Text.Contains("."))
                Text += ".";
        }

        public void Backspace()
        {
            if (_isNewEntry) return;

            if (Text.Length <= 1)
            {
                ClearEntry();
                return;
            }

            Text = Text.Substring(0, Text.Length - 1);

            if (Text.Length == 0 || Text == "-")
                ClearEntry();
        }

        public void ClearEntry()
        {
            Text = "0";
            _isNewEntry = true;
        }

        public bool TryGetValue(out double value)
        {
            return double.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}