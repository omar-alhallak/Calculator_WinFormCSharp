using System;

namespace Calculator.Calculator.Application.InterFaces
{
    public interface IClipboardService
    {
        void SetText(string text);
        string GetText();
        bool HasText();
    }
}
