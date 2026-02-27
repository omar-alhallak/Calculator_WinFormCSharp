using System;

namespace Calculator.Calculator.Domain.enums
{
    public enum CalcCommandType
    {
        Digit,
        Dot,
        Operator,
        Percent,
        Equals,
        Backspace,
        ClearEntry,
        ClearAll,
        Undo,
        Redo,
        Copy,
        Paste,
        OpenHistory,
        LoadExpression   
    }

    public readonly record struct CalcCommand(CalcCommandType Type, char? Char = null, string? Text = null);
}