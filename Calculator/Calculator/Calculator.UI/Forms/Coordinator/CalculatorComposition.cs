using System;
using System.Windows.Forms;
using Calculator.Calculator.Application.Input;
using Calculator.Calculator.Application.Engine;
using Calculator.Calculator.Application.History;
using Calculator.Calculator.Infrastructure.Input;
using Calculator.Calculator.UI.Tools.HistoryHelper;
using Calculator.Calculator.Infrastructure.Persistence;

namespace Calculator.Calculator.UI.Forms.Coordinator
{
    public sealed class CalculatorComposition // تجهيز وربط مكونات الحاسبة مع بعضها
    {
        public CalculatorController Controller { get; }
        public HistoryService History { get; }
        public HistoryView HistoryView { get; }

        public CalculatorComposition(Form hostForm)
        {
            if (hostForm is null) throw new ArgumentNullException(nameof(hostForm));

            string historyPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "Calculator", "history.json");

            Directory.CreateDirectory(Path.GetDirectoryName(historyPath)!);

            var repo = new FileHistoryRepository(historyPath);
            History = new HistoryService(repo);

            var engine = new CalculatorEngine();
            var undo = new UndoRedoManager<CalculatorState>(200);
            var clipboard = new WinFormsClipboardService();
            var selection = new WinFormsSelectionService(hostForm);

            Controller = new CalculatorController(engine, History, undo, clipboard, selection);

            HistoryView = new HistoryView();
            HistoryView.SetHistory(History);
        }
    }
}