using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace calculator.Core
{
    public enum Operator
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public static class OperatorInfo
    {
        public record Op(char Symbol, string DisplaySymbol, Func<double, double, double> Apply);

        public static readonly Dictionary<Operator, Op> ByType = new()
        {
            [Operator.Add] = new Op('+', "+", (a, b) => a + b),
            [Operator.Subtract] = new Op('-', "-", (a, b) => a - b),
            [Operator.Multiply] = new Op('*', "×", (a, b) => a * b),
            [Operator.Divide] = new Op('/', "÷", (a, b) => a / b),
        };

        public static readonly Dictionary<char, Operator> FromSymbol = new()
        {
            ['+'] = Operator.Add,
            ['-'] = Operator.Subtract,
            ['*'] = Operator.Multiply,
            ['/'] = Operator.Divide,
        };
    }
}