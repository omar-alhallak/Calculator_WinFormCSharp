using calculator.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace calculator
{
    public partial class Calculator_des : Form
    {
        public Calculator_des()
        {
            InitializeComponent();
        }
        char[] opers = { '+', '-', '×', '÷', '%' };
        bool isOp = false;
        bool isDot = true;
        bool isZero = true;
        bool isNum = true;
        bool thefirstoper = true;
        protected static bool divByZero = false;
        private void btnClick(object sender, EventArgs e)
        {
            thefirstoper = true;
            Button button = (Button)sender;
            if (divByZero)
            {
                txtResult.Text = "0";
                divByZero = false;
            }
            if (button.Text != "0" && !isNum)
            {
                isZero = true;
                isOp = true;
                isNum = true;
            }

            if (button.Text == "0" && isOp || !isZero)
            {
                if (txtResult.Text.Last() == '0')
                {
                    return;
                }
                isZero = true;
                return;

            }
            else if (button.Text == "0")
            {
                if (ThereAndLastOperator.LastisOperator(txtResult.Text))
                {
                    txtResult.Text += button.Text;
                    isOp = true;
                    return;
                }
            }
            if (isOp)
            {
                string temp = txtResult.Text;
                txtResult.Text = "";
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    txtResult.Text += temp[i];
                }

            }
            isOp = false;
            if (txtResult.Text == "0")
            {
                txtResult.Text = "";
                a = 0;
            }
            txtResult.Text += button.Text;
           
        }

        private void AClear(object sender, EventArgs e)
        {
            a = 0;
            thefirstoper = true;
            isDot = true;
            txtResult.Text = "0";
        }

        private void delete(object sender, EventArgs e)
        {

            thefirstoper = true;
            isOp = false;
            char[] txt = txtResult.Text.ToCharArray();
            txtResult.Text = "";
            if (txt.Length == 1)
            {
                txtResult.Text = "0";
                a = 0;
            }
            else
            {
                if (txt.Last() == '.')
                {
                    for (int i = 0; i < txt.Length - 1; i++)
                    {

                        txtResult.Text += txt[i];
                        isDot = true;
                        if (txtResult.Text.Last() == '0')
                        {
                            isZero = false;
                            isNum = false;
                            a = 0;
                        }
                    }
                }
                else
                {
                    if (txt.Last() == '-' || txt.Last() == '+' || txt.Last() == '÷' || txt.Last() == '×')
                    {
                        for (int i = 0; i < txt.Length - 1; i++)
                        {

                            txtResult.Text += txt[i];
                            isOp = true;
                            if (txtResult.Text.Last() == '0')
                            {
                                isZero = false;
                                isNum = false;                             
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < txt.Length - 1; i++)
                        {

                            txtResult.Text += txt[i];

                        }
                    }
                }
            }
        }


        private void operators(object sender, EventArgs e)
        {
            a = 0;
            isOp = false;
            string temp = txtResult.Text;
            Button button = (Button)sender;
            if (button.Text == "-" && txtResult.Text == "0")
            {
                txtResult.Text = "-";
                thefirstoper = false;
            }
            if (ThereAndLastOperator.LastisOperator(txtResult.Text) && thefirstoper)
            {
                txtResult.Text = "";
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    txtResult.Text += temp[i];

                }
                txtResult.Text += button.Text;

            }
            else if (txtResult.Text.Last() != '.' && thefirstoper)
            {
                txtResult.Text += button.Text;
            }
            isDot = true;
        }

        private void BtnDot(object sender, EventArgs e)
        {
            isOp = false;
            if (ThereAndLastOperator.LastisOperator(txtResult.Text))
            {
                txtResult.Text += "0" + ".";
                isDot = false;
            }
            if (isDot)
            {
                txtResult.Text += ".";
                isDot = false;
            }
            if (!isDot)
            {
                return;
            }
        }

        int a = 0;
        private void ModClick(object sender, EventArgs e)
        {
            a++;

            if (ThereAndLastOperator.LastisOperator(txtResult.Text))
            {
                return;
            }

            if (!ThereAndLastOperator.ThereisOperator(txtResult.Text) && a <= 2)
            {
                if (double.TryParse(txtResult.Text, out double value))
                {
                    txtResult.Text = (value / 100).ToString();
                }
                return;
            }

            if (a <= 2)
            {
                string expr = txtResult.Text;
                string leftPart = "";
                string rightPart = "";
                char op = '\0';

                for (int i = expr.Length - 1; i >= 0; i--)
                {
                    char c = expr[i];

                    if (c == '+' || c == '×' || c == '÷')
                    {
                        op = c;
                        leftPart = expr.Substring(0, i);
                        rightPart = expr.Substring(i + 1);
                        break;
                    }

                    if (c == '-')
                    {  
                        if (i == 0) continue;

                        char prev = expr[i - 1];
                        if (prev == '+' || prev == '-' || prev == '×' || prev == '÷')
                            continue;

                        op = c;
                        leftPart = expr.Substring(0, i);
                        rightPart = expr.Substring(i + 1);
                        break;
                    }
                }

                if (op == '\0')
                    return;

                if (!double.TryParse(rightPart, out double right))
                    return;

                double newRight;

                if (op == '+' || op == '-')
                {
                    var leftRes = calculator.Core.Calculator.calculate(leftPart);
                    if (!leftRes.Success || !double.TryParse(leftRes.Value, out double A))
                        return;

                    newRight = A * (right / 100.0);
                }
                else
                {
                    newRight = right / 100.0;
                }

                txtResult.Text = leftPart + op + newRight.ToString();
            }
        }

        private void res_Click(object sender, EventArgs e)
        {
            string question = "";
            if (ThereAndLastOperator.LastisOperator(txtResult.Text) || txtResult.Text.Last() == '.')
            {
                question = txtResult.Text.Substring(0, txtResult.Text.Length - 1);
                lblStorage.Text = question;
                txtResult.Text = "";
            }
            else
            {
                question = txtResult.Text;
                lblStorage.Text = question;
                txtResult.Text = "";
            }
            var result = calculator.Core.Calculator.calculate(question);
            if(!result.Success)
            {
                divByZero = true;
                txtResult.Text = result.Error;
            }
            else
            {
                txtResult.Text = result.Value;
            }
            if (txtResult.Text.Contains('.'))
                isDot = false;
        }
    }
}