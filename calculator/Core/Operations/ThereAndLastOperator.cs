using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core.Operations
{
    public static class ThereAndLastOperator
    {
        public static bool LastisOperator(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            char last = text.Last();
            return last == '+' || last == '×' || last == '÷' || last == '-';
        }
        public static bool ThereisOperator(string text)
        {
            return text.Contains('+') || text.Contains('×') || text.Contains('÷') || text.Contains('-');
        }
    }
}