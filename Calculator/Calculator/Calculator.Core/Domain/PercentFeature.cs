using Calculator.Calculator.Core.enums;
using Calculator.Calculator.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Calculator.Core.Domain
{
    public static class PercentFeature // عملية %
    {
        public static void AppendPercent(List<Token> tokens, InputBuffer input)
        {
            if (tokens.Count == 0 && input.IsFresh && input.TryGetValue(out var vFresh) && input.Text != "-")
            {
                tokens.Add(Token.Num(vFresh));
                tokens.Add(Token.Percent());
                input.BeginNew();
                return;
            }

            if (!input.IsFresh && input.TryGetValue(out var v))
            {
                if (tokens.Count == 0 || tokens[^1].Type == TokenType.Operator)
                    tokens.Add(Token.Num(v));
                else if (tokens[^1].Type == TokenType.Number)
                    tokens[^1] = Token.Num(v);

                tokens.Add(Token.Percent());
                input.BeginNew();
                return;
            }

            if (tokens.Count > 0 &&
                (tokens[^1].Type == TokenType.Number ||
                 tokens[^1].Type == TokenType.Percent))
            {
                tokens.Add(Token.Percent());
            }
        }

        public static List<Token> ExpandForEvaluation(IReadOnlyList<Token> raw)
        {
            var eval = new List<Token>();

            for (int i = 0; i < raw.Count; i++)
            {
                var t = raw[i];

                if (t.Type == TokenType.Operator)
                {
                    eval.Add(t);
                    continue;
                }

                if (t.Type == TokenType.Percent)
                    continue;

                double value = t.Number;

                int pctCount = 0;
                int j = i + 1;

                while (j < raw.Count && raw[j].Type == TokenType.Percent)
                {
                    pctCount++;
                    j++;
                }

                if (pctCount > 0)
                {
                    Operator? contextOp = null;
                    double baseValue = 0;

                    if (eval.Count >= 2 &&
                        eval[^1].Type == TokenType.Operator &&
                        eval[^2].Type == TokenType.Number)
                    {
                        contextOp = eval[^1].Operator;
                        baseValue = eval[^2].Number;
                    }

                    for (int k = 0; k < pctCount; k++)
                    {
                        if (contextOp == Operator.Add ||
                            contextOp == Operator.Subtract)
                        {
                            value = baseValue * (value / 100.0);
                        }
                        else
                        {
                            value = value / 100.0;
                        }
                    }

                    eval.Add(Token.Num(value));
                    i = j - 1;
                    continue;
                }
                eval.Add(t);
            }
            return eval;
        }
    }
}