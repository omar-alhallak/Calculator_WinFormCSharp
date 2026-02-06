using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core.Operations
{
    public class BinaryOperation
    {
        public static double twoNumbers(string num11, string op, string num22)
        {
            double num1 = double.Parse(num11);
            double num2 = double.Parse(num22);
            try
            {
                switch (op)
                {
                    case "×":
                        return num1 * num2;
                    case "÷":
                        return num1 / num2;
                    case "+":
                        return num1 + num2;
                    case "-":
                        return num1 - num2;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }
    }
}