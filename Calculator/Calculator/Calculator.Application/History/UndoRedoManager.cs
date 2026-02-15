using System;

namespace Calculator.Calculator.Application.History
{
    public class UndoRedoManager<T> // Ctrl + Z / Ctrl + Y
    {
        private readonly int Limit;
        private readonly Stack<T> undo = new();
        private readonly Stack<T> redo = new();

        public UndoRedoManager(int limit = 200)
        {
            Limit = limit;
        }

        public void Clear() // تصفير الستاكات
        {
            undo.Clear();
            redo.Clear();
        }

        public void Push(T state) // عملية الإضافة
        {
            undo.Push(state);

            if (undo.Count > Limit)
            {
                var arr = undo.ToArray(); 
                undo.Clear();
                for (int i = Limit - 1; i >= 0; i--)
                    undo.Push(arr[i]); 
            }

            redo.Clear();
        }

        public bool TryUndo(T current, out T prev)  
        {
            prev = default!;
            if (undo.Count == 0) return false;

            redo.Push(current);
            prev = undo.Pop();
            return true;
        }

        public bool TryRedo(T current, out T next)  
        {
            next = default!;
            if (redo.Count == 0) return false;

            undo.Push(current);
            next = redo.Pop();
            return true;
        }
    }
}