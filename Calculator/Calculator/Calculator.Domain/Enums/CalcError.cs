using System;

namespace Calculator.Calculator.Domain.enums
{
    public enum CalcError
    {
        None,
        DivideByZero,
        Overflow,
        InvalidExpression,
        TooLong
    }
}