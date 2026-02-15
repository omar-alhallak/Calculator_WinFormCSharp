using System;
using System.Collections.Generic;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Domain.enums;

namespace Calculator.Calculator.Application.Engine
{
    public static class PercentFeature // عملية %
    {
        public static void AppendPercent(List<Token> tokens, InputBuffer input) // كيفية إدخال % عند نقر الزر
        {
            if (tokens.Count == 0 && input.IsFresh && input.TryGetValue(out var vFresh) && input.Text != "-")
            {
                tokens.Add(Token.Num(vFresh));

                if (ExceededPercentLimit(tokens)) return;

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

                if (ExceededPercentLimit(tokens)) return;

                tokens.Add(Token.Percent());
                input.BeginNew();
                return;
            }

            if (tokens.Count > 0 && (tokens[^1].Type == TokenType.Number || tokens[^1].Type == TokenType.Percent))
            {
                if (ExceededPercentLimit(tokens)) return;

                tokens.Add(Token.Percent());
            }          
        }

        private static bool ExceededPercentLimit(List<Token> tokens) // حد إدخال
        {
            int chain = 0;
            int i = tokens.Count - 1;

            while (i >= 0 && tokens[i].Type == TokenType.Percent)
            {
                chain++;
                i--;
            }

            return chain >= CalcLimits.MaxPercentChain;
        }

        public static List<Token> ExpandForEvaluation(IReadOnlyList<Token> raw) // تفسير % قبل الحساب
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

                    if (eval.Count >= 2 && eval[^1].Type == TokenType.Operator && eval[^2].Type == TokenType.Number) 
                    {
                        contextOp = eval[^1].Operator;
                        baseValue = eval[^2].Number;
                    }

                    for (int k = 0; k < pctCount; k++)
                    {
                        if (contextOp == Operator.Add || contextOp == Operator.Subtract) 
                            value = baseValue * (value / 100.0);
                        else
                            value = value / 100.0;
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