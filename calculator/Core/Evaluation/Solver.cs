using calculator.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core.Evaluation
{
    public static class Solver
    {
        public static CalcResult Solve(List<string> question) 
        {
            for (int i = 0; i < question.Count; i++)
            {
                if (!question.Contains("×") && !question.Contains("÷"))
                    break;

                if (question[i] == "×")
                {
                    double mul = BinaryOperation.twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = mul.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }
                if (question[i] == "÷")
                {
                    if (question[i + 1] == "0")
                    {
                        return CalcResult.Fail("Cant div by zero");
                    }
                    double div = BinaryOperation.twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = div.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }
            }

            for (int i = 0; i < question.Count; i++)
            {
                if (!question.Contains("+") && !question.Contains("-"))
                    break;

                if (question[i] == "-")
                {
                    double min = BinaryOperation.twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = min.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }
                if (question[i] == "+")
                {
                    double plus = BinaryOperation.twoNumbers(question[i - 1], question[i], question[i + 1]);
                    question[i - 1] = plus.ToString();
                    question.RemoveAt(i + 1);
                    question.RemoveAt(i);
                    i = 0;
                }
            }
            string temp = "";
            for (int i = 0; i < question.Count; i++)
            {
                temp += question[i];
            }
            return CalcResult.Ok(temp);
        }
    }
}
