using System;
using Calculator.Calculator.Core.Model;
using Calculator.Calculator.Domain.enums;

namespace Calculator.Calculator.Application.Evaluation
{
    public static class MathEvaluator // مسؤل عن تنفيذ العمليات الحسابية
    {
        // مسؤلة عن معالجة الأخطاء
        public static bool TryEvaluate(IReadOnlyList<Token> tokens, out double result, out CalcError error) 
        {
            result = 0;
            error = CalcError.None;

            if (tokens.Count == 0)
            {
                error = CalcError.InvalidExpression;
                return false;
            }

            var values = new Stack<double>();
            var ops = new Stack<Operator>();

            foreach (var t in tokens)
            {
                if (t.Type == TokenType.Number)
                {
                    values.Push(t.Number);
                    continue;
                }

                if(t.Type!=TokenType.Operator)
                {
                    error = CalcError.InvalidExpression;
                    return false;
                }

                while (ops.Count > 0 && OperatorInfo.ByType[ops.Peek()].Precedence >= OperatorInfo.ByType[t.Operator].Precedence) 
                {
                    if (!ApplyOperator(values, ops.Pop(), out result, out error)) return false;
                }

                ops.Push(t.Operator);
            }

            while (ops.Count > 0)
            {
                if (!ApplyOperator(values, ops.Pop(), out result, out error)) return false;
            }

            if (values.Count != 1)
            {
                error = CalcError.InvalidExpression;
                return false;
            }

            result = values.Pop();

            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                error = CalcError.Overflow;
                return false;
            }

            error = CalcError.None;
            return true;
        }

        // تنفيذ العملية الي في أعلى الستاك
        private static bool ApplyOperator(Stack<double> values, Operator op, out double tmp, out CalcError error)
        {
            tmp = 0;
            error = CalcError.None;

            if (values.Count < 2)
            {
                error = CalcError.InvalidExpression;
                return false;
            }

            double b = values.Pop();
            double a = values.Pop();

            if (op == Operator.Divide && b == 0)
            {
                error = CalcError.DivideByZero;
                return false;
            }

            tmp = OperatorInfo.ByType[op].Apply(a, b);

            if (double.IsNaN(tmp) || double.IsInfinity(tmp))
            {
                error = CalcError.Overflow;
                return false;
            }

            values.Push(tmp);
            return true;
        }
    }
}