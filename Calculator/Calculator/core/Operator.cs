using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core
{
    public enum Operator
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Percent
    }

    public class OperatorInfo
    {
        public record Op(char Symbol, string DisplaySymbol,int Precedence, Func<double, double, double> Apply);

        public static readonly Dictionary<Operator, Op> ByType = new()
        {
            [Operator.Add] = new Op('+', "+", 1, (a, b) => a + b),
            [Operator.Subtract] = new Op('-', "-", 1, (a, b) => a - b),
            [Operator.Multiply] = new Op('*', "×", 2, (a, b) => a * b),
            [Operator.Divide] = new Op('/', "÷", 2, (a, b) => a / b),
        };

        public static readonly Dictionary<char, Operator> FromSymbol = new()
        {
            ['+'] = Operator.Add,
            ['-'] = Operator.Subtract,
            ['*'] = Operator.Multiply,
            ['/'] = Operator.Divide,
            ['%'] = Operator.Percent
        };
    }
}