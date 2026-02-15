using System;

namespace Calculator.Calculator.Application.InterFaces
{
    public interface ISelectionService
    {
        bool HasSelection();
        string GetSelectedText();
    }
}