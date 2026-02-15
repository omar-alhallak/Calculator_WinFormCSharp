using System;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Infrastructure.Persistence;

namespace Calculator.Calculator.Application.History
{
    public sealed class HistoryService // مسؤل عن عملية التخزين
    {
        private readonly FileHistoryRepository Repo;
        private readonly List<HistoryEntry> Items;
        public event Action? Changed;

        public int SafetyCap { get; }

        public HistoryService(FileHistoryRepository repo, int safetyCap = 5000)
        {
            Repo = repo;
            SafetyCap = Math.Max(100, safetyCap);
            Items = Repo.Load().ToList();
            TrimIfNeeded(); 
        }

        public IReadOnlyList<HistoryEntry> GetAll() => Items.AsReadOnly(); // يرجع العمليات للقراءة

        public IReadOnlyList<HistoryEntry> GetLast(int count) // يرجع آخر عنصر
        {
            count = Math.Max(0, count);
            if (count == 0) return Array.Empty<HistoryEntry>();

            return Items.TakeLast(count).Reverse().ToList();
        }  

        public void Add(string expression, string result) // تخزين العملية
        {
            if (!IsValidHistoryEntry(expression, result)) return;

            Items.Add(new HistoryEntry
            {
                Expression = expression.Trim(),
                Result = result.Trim()
            });

            TrimIfNeeded();
            TrySave();
        }

        public bool Delete(Guid id) // حذف عملية
        {
            int idx = Items.FindIndex(x => x.Id == id);
            if (idx < 0) return false;

            Items.RemoveAt(idx);
            TrySave();
            return true;
        }

        public void ClearAll() // تفريغ الملف
        {
            if (Items.Count == 0) return;

            Items.Clear();
            TrySave();
        }

        private void TrimIfNeeded() // إذا تم تجاوز الحد يحذف الأفدم ويضيف الأحدث
        {
            if (Items.Count <= SafetyCap) return;

            int removeCount = Items.Count - SafetyCap;
            Items.RemoveRange(0, removeCount);
        }

        private void TrySave() // حفظ 
        {
            try
            {
                Repo.Save(Items);
            }
            catch(IOException)  {  }
            Changed?.Invoke();
        }

        private static bool IsValidHistoryEntry(string expression, string result) // تحقق من الإدخال
        {
            if (string.IsNullOrWhiteSpace(expression)) return false;
            if (string.IsNullOrWhiteSpace(result)) return false;

            string expr = expression.Trim();

            if (EndsWithOperator(expr)) return false;

            if (result.Trim().Equals("Error", StringComparison.OrdinalIgnoreCase)) return false;

            if (expr.EndsWith(".", StringComparison.Ordinal)) return false;

            if (expr == "-" || expr == "+") return false;

            if (double.TryParse(expr, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var onlyNumber)) return false;

            bool hasOp =
                expr.Contains('+') ||
                expr.Contains('×') || expr.Contains('*') ||
                expr.Contains('÷') || expr.Contains('/') ||
                expr.Contains('%') ||
                HasBinaryMinus(expr);

            if (!hasOp) return false;

            if (expr.Contains('%') && !expr.Contains('+') && !HasBinaryMinus(expr) &&
                !expr.Contains('×') && !expr.Contains('*') &&
                !expr.Contains('÷') && !expr.Contains('/'))
            {
                var numPart = expr.Replace("%", "").Trim();
                if (double.TryParse(numPart, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var p))
                {
                    if (p == 0) return false; 
                }
            }

            return true;
        }

        private static bool HasBinaryMinus(string expr) // لتمييز أن -6 ليست عملية
        {
            for (int i = 0; i < expr.Length; i++)
            {
                if (expr[i] != '-' && expr[i] != '−')  continue;

                int j = i - 1;
                while (j >= 0 && char.IsWhiteSpace(expr[j]))
                    j--;

                if (j < 0) continue;

                char prev = expr[j];

                if (char.IsDigit(prev) || prev == '%') return true;
            }

            return false;
        }

        private static bool EndsWithOperator(string expr) // لتأكد أن التعبير لا يحتوي عملية
        {
            expr = expr.TrimEnd();

            return expr.EndsWith("+") ||
                   expr.EndsWith("-") ||
                   expr.EndsWith("×") || expr.EndsWith("*") ||
                   expr.EndsWith("÷") || expr.EndsWith("/");
        }
    }
}