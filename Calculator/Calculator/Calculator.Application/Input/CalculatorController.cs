using System;
using System.Text;
using Calculator.Calculator.Domain.enums;
using Calculator.Calculator.Application.Engine;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.Application.InterFaces;


namespace Calculator.Calculator.Application.Input
{
    // العقل مسؤل عن ربط بين
    //         UI
    //           و ال
    //       Engine
    public sealed class CalculatorController 
    {
        private readonly CalculatorEngine Engine1;
        private readonly HistoryService Historys;
        private readonly UndoRedoManager<CalculatorState> Undo1;
        private readonly IClipboardService Clipboard;
        private readonly ISelectionService Selection;
        public event Action? Changed;

        public CalculatorEngine Engine => Engine1;

        public CalculatorController
            (
            CalculatorEngine engine,
            HistoryService history,
            UndoRedoManager<CalculatorState> undo,
            IClipboardService clipboard,
            ISelectionService selection
            )
        {
            Engine1 = engine;
            Historys = history;
            Undo1 = undo;
            Clipboard = clipboard;
            Selection = selection;

            Historys.Changed += () => Changed?.Invoke();
        }

        public void Execute(CalcCommand cmd) // يحول أي ضغطة كيبورد إلى تعليمة حسابية
        {
            // =========================
            //       Undo / Redo
            // =========================
            if (cmd.Type == CalcCommandType.Undo)
            {
                if (Undo1.TryUndo(Engine1.Undo(), out var prev))
                    Engine1.Redo(prev);

                Changed?.Invoke();
                return;
            }

            if (cmd.Type == CalcCommandType.Redo)
            {
                if (Undo1.TryRedo(Engine1.Undo(), out var next))
                    Engine1.Redo(next);

                Changed?.Invoke();
                return;
            }

            // =========================
            //          Copy
            // =========================
            if (cmd.Type == CalcCommandType.Copy)
            {
                if (Selection.HasSelection())
                    Clipboard.SetText(Selection.GetSelectedText());
                else
                    Clipboard.SetText(Engine1.BottomLine ?? "0");

                return;
            }

            // =========================
            //          Paste
            // =========================
            if (cmd.Type == CalcCommandType.Paste)
            {
                if (!Clipboard.HasText()) return;

                var beforePaste = Engine1.Undo();
                PasteAsTyping(Clipboard.GetText());
                var afterPaste = Engine1.Undo();

                if (HasStateChanged(beforePaste, afterPaste))
                    Undo1.Push(beforePaste);

                Changed?.Invoke();
                return;
            }

            // =========================
            //      LoadExpression
            // =========================
            if (cmd.Type == CalcCommandType.LoadExpression)
            {
                if (string.IsNullOrWhiteSpace(cmd.Text)) return;

                var beforeLoad = Engine1.Undo();
                Engine1.ClearAll();
                PasteAsTyping(cmd.Text!);

                var afterLoad = Engine1.Undo();
                if(HasStateChanged(beforeLoad,afterLoad))
                    Undo1.Push(beforeLoad);

                Changed?.Invoke();
                return;
            }

            // =========================
            //        أي تعديل
            // =========================
            var before = Engine1.Undo();
            string exprBefore = Engine1.TopLine;

            switch (cmd.Type)
            {
                case CalcCommandType.Digit:
                    if (cmd.Char is null) return;
                    Engine1.InputDigit(cmd.Char!.Value);
                    break;

                case CalcCommandType.Dot:
                    Engine1.InputDot();
                    break;

                case CalcCommandType.Operator:
                    if (cmd.Char is null) return;
                    Engine1.SelectOperator(cmd.Char!.Value);
                    break;

                case CalcCommandType.Percent:
                    Engine1.ApplyPercent();
                    break;

                case CalcCommandType.Backspace:
                    Engine1.Backspace();
                    break;

                case CalcCommandType.ClearEntry:
                    Engine1.ClearEntry();
                    break;

                case CalcCommandType.ClearAll:
                    Engine1.ClearAll();
                    break;

                case CalcCommandType.Equals:
                    {
                        bool ok = Engine1.Equals(out var err);

                        if (ok && err == CalcError.None && Engine1.PreviewError == CalcError.None) 
                        {
                            string exprToSave = RemoveTrailingOperators(exprBefore);

                            if (!string.IsNullOrWhiteSpace(exprToSave) && ContainsOperator(exprToSave)) 
                                Historys.Add(exprToSave, Engine1.BottomLine);
                        }

                        break;
                    }
            }
            var after = Engine1.Undo();

            if (HasStateChanged(before, after))
                Undo1.Push(before);

            Changed?.Invoke();
        }

        private static string RemoveTrailingOperators(string? expr) // إزالة العمليات في النهاية
        {
            if (string.IsNullOrWhiteSpace(expr)) return "";

            string s = expr.Trim();

            while (s.Length > 0)
            {
                char last = s[^1];

                if (last is '+' or '-' or '×' or '÷' or '*' or '/')
                    s = s[..^1].TrimEnd();
                else
                    break;
            }

            return s;
        }

        private static bool ContainsOperator(string expr) // تأكد من العمليات 
        {
            foreach (char c in expr)
            {
                if (c is '+' or '-' or '×' or '÷' or '*' or '/' or '%') return true;
            }
            return false;
        }

        private void PasteAsTyping(string raw) // تنظيم النسخ
        {
            if (string.IsNullOrWhiteSpace(raw)) return;

            string s = raw.Trim().Replace(" ", "").Replace(',', '.');

            var filtered = new StringBuilder();
            foreach (char c in s)
            {
                if (char.IsDigit(c) || c == '.' || c == '+' || c == '-' || c == '*' || 
                    c == '/' || c == '%' || c == '×' || c == '÷')
                {
                    filtered.Append(c);
                }
            }

            s = filtered.ToString();
            if (s.Length == 0) return;

            foreach (char c in s)
            {
                if (char.IsDigit(c))
                    Engine1.InputDigit(c);
                else if (c == '.')
                    Engine1.InputDot();
                else if (c == '×')
                    Engine1.SelectOperator('*');
                else if (c == '÷')
                    Engine1.SelectOperator('/');
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                    Engine1.SelectOperator(c);
                else if (c == '%')
                    Engine1.ApplyPercent();
            }
        }

        public void LoadExpression(string expression) 
        {
            Execute(new CalcCommand(CalcCommandType.LoadExpression, Text: expression));
        }

        private static bool HasStateChanged(CalculatorState a, CalculatorState b) // Undo
        {
            if (a.InputText != b.InputText) return true;
            if (a.InputFresh != b.InputFresh) return true;
            if (a.AfterEquals != b.AfterEquals) return true;
            if (a.PreviewError != b.PreviewError) return true;
            if (a.Tokens.Count != b.Tokens.Count) return true;

            for (int i = 0; i < a.Tokens.Count; i++)
                if (!a.Tokens[i].Equals(b.Tokens[i])) return true;

            return false;
        }
    }
}