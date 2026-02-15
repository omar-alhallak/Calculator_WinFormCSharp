using System;
using System.Globalization;

namespace Calculator.Calculator.Application.Evaluation
{
    public static class NumberFormatter // لتنظيم طريقة عرض الأرقام
    {
        public static string Format(double value) // عملية التنظيم
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) return "Error";

            double abs = Math.Abs(value);

            if (value == 0) return "0";

            if (abs >= 1e-9 && abs < 1e15) return value.ToString("G15", CultureInfo.InvariantCulture);

            return value.ToString("0.############E+0", CultureInfo.InvariantCulture);
        }

        public static string ToInputString(double value) // عملية التحويل
        {
            return value.ToString("G15", CultureInfo.InvariantCulture);
        }
    }
}