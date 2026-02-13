using Calculator.Calculator.Core.Domain;
using Calculator.Calculator.Core.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Calculator.Core.History
{
    public sealed record CalculatorState // Snapshot للعملية
        (
            List<Token> Tokens,
            string InputText,
            bool InputFresh,
            bool AfterEquals,
            string TopLine,
            string BottomLine,
            CalcError PreviewError
        );
}