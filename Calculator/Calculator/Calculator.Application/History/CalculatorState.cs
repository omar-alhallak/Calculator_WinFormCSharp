using System;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Domain.enums;

namespace Calculator.Calculator.Application.History
{
    public sealed record CalculatorState // Snapshot للعملية
        (
            IReadOnlyList<Token> Tokens,
            string InputText,
            bool InputFresh,
            bool AfterEquals,
            string TopLine,
            string BottomLine,
            CalcError PreviewError
        );
}