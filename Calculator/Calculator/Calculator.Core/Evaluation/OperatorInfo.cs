using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Calculator.Calculator.Core.enums;

namespace Calculator.Calculator.Core.Evaluation
{
    public static class OperatorInfo // لتنظيم عرض العمليات للمستخدم
    {
        // مثل حاوية تحوي كل القيم المراد تحويلها
        public sealed record Op(char Symbol, string DisplaySymbol, int Precedence, Func<double, double, double> Apply);

        public static readonly Dictionary<Operator, Op> ByType = new() // كل رمز ما هو لإستخدامه في الحساب
        {
            [Operator.Add] = new Op('+', "+", 1, (a, b) => a + b),
            [Operator.Subtract] = new Op('-', "-", 1, (a, b) => a - b),
            [Operator.Multiply] = new Op('*', "×", 2, (a, b) => a * b),
            [Operator.Divide] = new Op('/', "÷", 2, (a, b) => a / b),
        };

        public static readonly Dictionary<char, Operator> FromSymbol = new() // يحول الرمز إلى enum
        {
            ['+'] = Operator.Add,
            ['-'] = Operator.Subtract,
            ['*'] = Operator.Multiply,
            ['/'] = Operator.Divide,
        };
    }
}