using System;
using Calculator.Calculator.Application.InterFaces;

namespace Calculator.Calculator.Infrastructure.Input
{
    public sealed class WinFormsClipboardService : IClipboardService
    {
        public void SetText(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            Clipboard.SetText(text);
        }

        public string GetText()
        {
            return Clipboard.ContainsText() ? Clipboard.GetText() : "";
        }

        public bool HasText() => Clipboard.ContainsText();
    }
}