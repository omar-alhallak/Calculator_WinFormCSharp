using System;

namespace Calculator.Calculator.Application.Engine
{
    internal static class CalcLimits // يحدد الحد الأقصر للحاسية 
    {
        public const int MaxInputDigits = 16;   
        public const int MaxTokens = 5000;
        public const int MaxPercentChain = 8;
    }
}