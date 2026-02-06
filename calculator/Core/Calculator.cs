using calculator.Core.Evaluation;
using calculator.Core.Operations;
using calculator.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core
{
    public static class Calculator 
    {
            public static CalcResult calculate(string quest)
            {
                List<string> question = ExpressionTokenizer.Tokenize(quest);
            if (question.Count == 0)
                return CalcResult.Ok("0");

                if (question[0] == "")
                {
                    question.RemoveAt(0);
                }

            if (question.Count > 1 && question[0] == "-") 
            {
                question[0] = question[1];
                question.RemoveAt(1);
            }

                return Solver.Solve(question);
            }      
        }
    }