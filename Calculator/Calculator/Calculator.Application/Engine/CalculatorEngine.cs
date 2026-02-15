using System;
using System.Collections.Generic;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.Application.Evaluation;

namespace Calculator.Calculator.Application.Engine
{
    public class CalculatorEngine // محرك الآلة الحاسبة
    {
        private readonly InputBuffer Input = new();
        private readonly List<Token> Tokens = new();
        private bool AfterEquals = false;

        public CalcError PreviewError { get; private set; } = CalcError.None;
        public string TopLine { get; private set; } = "";
        public string BottomLine { get; private set; } = "0";

        /// =======================
        ///        Buttons
        /// =======================
        public void ClearAll() // لحذف كل شيء تصفير
        {
            Tokens.Clear();
            Input.Restore("0", true);
            AfterEquals = false;
            PreviewError = CalcError.None;
            TopLine = "";
            BottomLine = "0";
        }

        public void ClearEntry() // حذف جزء
        {
            if (!Input.IsFresh || Tokens.Count == 0) 
            {
                Input.Restore("0", true);
                UpdatePreview();
                return;
            }

            if (Tokens[^1].Type == TokenType.Operator) return;

            if (Tokens[^1].Type == TokenType.Percent)
            {
                while (Tokens.Count > 0 && Tokens[^1].Type == TokenType.Percent)
                    Tokens.RemoveAt(Tokens.Count - 1);

                if (Tokens.Count > 0 && Tokens[^1].Type == TokenType.Number)
                    Tokens.RemoveAt(Tokens.Count - 1);

                if (Tokens.Count == 0)
                {
                    Input.Restore("0", true);
                    TopLine = "";
                    BottomLine= "0";
                    PreviewError= CalcError.None;
                    return;
                }
                    
                Input.BeginNew();
                UpdatePreview();
                return;
            }

            if (Tokens[^1].Type == TokenType.Number)
            {
                Tokens.RemoveAt(Tokens.Count - 1);
                Input.BeginNew();
                UpdatePreview();
                return;
            }
        }

        public void InputDigit(char d) // عملية إدخال الأرقام
        {
            if (Tokens.Count > 0 && Tokens[^1].Type == TokenType.Percent) return;

            if (AfterEquals)
            {
                Tokens.Clear();
                TopLine = "";
                AfterEquals = false;
            }

            Input.InputDigit(d);
            UpdatePreview();
        }

        public void InputDot() // إدخال الفاصلة 
        {
            if (Tokens.Count > 0 && Tokens[^1].Type == TokenType.Percent) return;

            if (AfterEquals)
            {
                Tokens.Clear();
                TopLine = "";
                AfterEquals = false;
            }

            Input.InputDot();
            UpdatePreview();
        }

        public void SelectOperator(char symbol) // آلية إضافة عملية حسابية بكل حالاتها
        {
            if (symbol == '-' && Tokens.Count == 0 && !Input.IsFresh && Input.Text == "-")
            {
                Input.Restore("0", true);
                UpdatePreview();
                return;
            }

            if (symbol == '-' && Tokens.Count == 0 && Input.IsFresh && Input.Text == "0")
            {
                Input.Restore("-", false);
                UpdatePreview();
                return;
            }

            if (!OperatorInfo.FromSymbol.TryGetValue(symbol, out var op))  return;

            if (Input.IsFresh && Tokens.Count > 0 && Tokens[^1].Type == TokenType.Percent) 
            {
                if (Tokens.Count >= CalcLimits.MaxTokens - 1)
                {
                    PreviewError = CalcError.TooLong;
                    UpdatePreview();
                    return;
                }

                Tokens.Add(Token.Op(op));
                Input.BeginNew();
                UpdatePreview();
                return;
            }

            if (AfterEquals)
            {
                AfterEquals = false;
                Tokens.Clear();

                if (Input.TryGetValue(out var vEq))
                    Tokens.Add(Token.Num(vEq));
                else
                    Tokens.Add(Token.Num(0));

                Input.BeginNew();
            }

            if (Tokens.Count > 0 && Tokens[^1].Type == TokenType.Operator && Input.IsFresh)
            {
                Tokens[^1] = Token.Op(op);
                UpdatePreview();
                return;
            }

            if (!Input.TryGetValue(out var v))
                v = 0;

            if (Tokens.Count >= CalcLimits.MaxTokens - 2)
            {
                PreviewError = CalcError.TooLong;
                UpdatePreview();
                return;
            }

            if (Tokens.Count == 0 || Tokens[^1].Type == TokenType.Operator)
                Tokens.Add(Token.Num(v));
            else
                Tokens[^1] = Token.Num(v);

            Tokens.Add(Token.Op(op));
            Input.BeginNew();
            UpdatePreview();
        }

        public void ApplyPercent() // زر %
        {
            if (PreviewError != CalcError.None) return;

            if(AfterEquals)
            {
                AfterEquals = false;
                Tokens.Clear();
                TopLine = "";
            }

            PercentFeature.AppendPercent(Tokens, Input);
            UpdatePreview();
        }

        public bool Equals(out CalcError error) // زر اليساوي
        {
            error = CalcError.None;

            if (AfterEquals) return true;

            if (Tokens.Count == 0 && Input.IsFresh)
            {
                TopLine = "";
                BottomLine = "0";
                PreviewError = CalcError.None;
                return true;
            }

            if (PreviewError != CalcError.None)
            {
                error = PreviewError;
                return false;
            }

            var eval = BuildEvaluationList();

            if (!MathEvaluator.TryEvaluate(eval, out var result, out var evalError))
            {
                PreviewError = evalError;
                error = evalError;
                return false;
            }

            string shown = NumberFormatter.Format(result);
            TopLine = shown;
            BottomLine = shown;

            Input.Restore(NumberFormatter.ToInputString(result), true);
            Tokens.Clear();
            AfterEquals = true;

            PreviewError = CalcError.None;
            return true;
        }

        public void Backspace() // عملية الرجوع للخلف
        {
            if (!Input.IsFresh)
            {
                Input.Backspace();
                UpdatePreview();
                return;
            }

            if (Tokens.Count == 0)
            {
                Input.Restore("0", true);
                UpdatePreview();
                return;
            }

            Tokens.RemoveAt(Tokens.Count - 1);

            if (Tokens.Count == 0)
            {
                Input.Restore("0", true);
                UpdatePreview();
                return;
            }

            var last = Tokens[^1];

            if (last.Type == TokenType.Number)
            {
                Tokens.RemoveAt(Tokens.Count - 1);
                Input.Restore(NumberFormatter.ToInputString(last.Number), false);
            }
            else
                Input.Restore("0", true);

            UpdatePreview();
        }

        // =============================
        //    Display rules + preview
        // =============================
        private void UpdatePreview() // عملية تحديث العرض عند كل ضغطة
        {
            TopLine = BuildTopLine(Tokens);

            if (!Input.IsFresh)
                TopLine = string.IsNullOrWhiteSpace(TopLine) ? Input.Text : TopLine + " " + Input.Text;

            var eval = BuildEvaluationList();

            if (eval.Count == 0)
            {
                if (Input.Text == "-")
                    BottomLine = "0";
                else
                    BottomLine = Input.Text;

                PreviewError = CalcError.None;
                return;
            }

            if (MathEvaluator.TryEvaluate(eval, out var preview, out var evalError))
            {
                BottomLine = NumberFormatter.Format(preview);
                PreviewError = CalcError.None;
            }
            else
            {
                PreviewError = evalError;
                BottomLine = Input.Text;
            }
        }

        private List<Token> BuildEvaluationList() // تهيئة العملية الحسابية قبل حسابها
        {
            var eval = new List<Token>(Tokens);

            if (eval.Count > 0 && eval[^1].Type == TokenType.Operator && Input.IsFresh)
                eval.RemoveAt(eval.Count - 1);

            if (!Input.IsFresh && Input.TryGetValue(out var v))
                eval.Add(Token.Num(v));

            return PercentFeature.ExpandForEvaluation(eval);
        }

        private static string BuildTopLine(IReadOnlyList<Token> tokens) // بناء السطر العلوي 
        {
            if (tokens.Count == 0) return "";

            var parts = new List<string>(tokens.Count);

            foreach (var t in tokens)
            {
                if (t.Type == TokenType.Number)
                    parts.Add(NumberFormatter.Format(t.Number));
                else if (t.Type == TokenType.Operator) 
                    parts.Add(OperatorInfo.ByType[t.Operator].DisplaySymbol);
                else if (t.Type == TokenType.Percent)
                    parts.Add("%");
            }

            return string.Join(" ", parts);
        }

        /// =================
        ///    Undo / Redo
        /// =================

        internal CalculatorState Undo() // Ctrl + Z
        {
            return new CalculatorState(new List<Token>(Tokens),
                Input.Text,
                Input.IsFresh,
                AfterEquals,
                TopLine,
                BottomLine,
                PreviewError
            );
        }

        internal void Redo(CalculatorState s) // Ctrl + Y
        {
            Tokens.Clear();
            Tokens.AddRange(s.Tokens);

            Input.Restore(s.InputText, s.InputFresh);
            AfterEquals = s.AfterEquals;

            TopLine = s.TopLine;
            BottomLine = s.BottomLine;
            PreviewError = s.PreviewError;
        } 
    }
}