using calculator.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core.Parsing
{
    public static class ExpressionTokenizer
    {
        public static List<string> Tokenize(string quest)
        {
            List<string> question = new List<string>();

            string temporary = "";
            for (int i = 0; i < quest.Length; i++)
            {
                if (OperatorHelper.isOperator(quest[i]))
                {
                    question.Add(temporary);
                    question.Add(quest[i].ToString());
                    temporary = "";
                }
                else
                {
                    temporary += quest[i];
                }
            }
            if (temporary.Length > 0)
            {
                question.Add(temporary);
            }
            return question;
        }
    }
}