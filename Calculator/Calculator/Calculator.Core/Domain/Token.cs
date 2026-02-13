using Calculator.Calculator.Core.Domain;
using Calculator.Calculator.Core.History;
using Calculator.Calculator.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Calculator.Core.enums;

namespace Calculator.Calculator.Core.Domain
{
    public readonly struct Token // يمثل العملية الحسابية
    {
        public TokenType Type { get; }
        public double Number { get; }
        public Operator Operator { get; }

        private Token(TokenType type, double number, Operator op)
        {
            Type = type;
            Number = number;
            Operator = op;
        }

        public static Token Num(double v) => new(TokenType.Number, v, default);
        public static Token Op(Operator op) => new(TokenType.Operator, 0, op);
    }
}