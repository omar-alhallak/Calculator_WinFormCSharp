using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Core.Evaluation
{
    public class CalcResult
    {
        public bool Success { get; }
        public string Value { get; }
        public string Error { get; }

        private CalcResult(bool success, string value, string error)
        {
            Success = success;
            Value = value;
            Error = error;
        }

        public static CalcResult Ok(string value) => new CalcResult(true, value, "");
        public static CalcResult Fail(string error) => new CalcResult(false, "0", error);
    }
}