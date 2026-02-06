using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core.Operations
{
    public class OperatorHelper
    {
        public static bool isOperator(char ch)
        {
            if (ch == '+' || ch == '-' || ch == '×' || ch == '÷')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
