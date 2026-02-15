using System;
using System.Collections.Generic;
using Calculator.Calculator.Domain.enums;

namespace Calculator.Calculator.Core.Model
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
        public static Token Percent() => new(TokenType.Percent, 0, default);
    }
}