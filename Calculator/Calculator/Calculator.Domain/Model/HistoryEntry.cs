using System;

namespace Calculator.Calculator.Core.Model
{
    public sealed record HistoryEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Expression { get; init; } = "";
        public string Result { get; init; } = "";
        public override string ToString() => $"{Expression} = {Result}";
    }
}