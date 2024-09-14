using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Calculation
    {
        private string[] operations_signs = new string[] { "+", "-", "/", "*" };
        private string result { get; }

        private int priority(string sign)
        {
            switch (sign)
            {
                case "*": return 1;
                case "/": return 1;
                default : return 0;
            }
        }

        private string[] SplitExpression(string expression)
        {
            string pattern = @"(\d+|[-+*/])";

            MatchCollection matches = Regex.Matches(expression, pattern);

            string[] result = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                result[i] = matches[i].Value;
            }

            return result;
        }

        private List<string> ConvertToRPN(string expression)
        {
            Stack<string> stack = new Stack<string>();
            List<string> result = new List<string>();

            string[] expression_array = SplitExpression(expression);

            foreach (string symbol in expression_array)
            {
                if (!operations_signs.Any(sign => symbol == sign))
                {
                    result.Add(symbol);
                }
                else
                {
                    if (stack.Count == 0)
                        stack.Push(symbol);
                    else
                    {
                        while (stack.Count != 0 && priority(symbol) <= priority(stack.Peek()))
                        {
                            result.Add(stack.Pop());
                        }
                        stack.Push(symbol);
                    }
                }
            }

            while (stack.Count != 0) 
            { 
                result.Add(stack.Pop());
            }

            return result;
        }

        public string CalculateRPN(string expression)
        {
            List<string> rpn_expression = ConvertToRPN(expression);

            Stack<string> stack = new Stack<string>();
            int a, b;

            foreach (string obj in rpn_expression)
            {
                if (!operations_signs.Any(sign => obj == sign))
                {
                    stack.Push(obj);
                }
                else
                {
                    b = Int32.Parse(stack.Pop());
                    a = Int32.Parse(stack.Pop());
                    switch (obj)
                    {
                        case "+": stack.Push((a + b).ToString()); break;
                        case "-": stack.Push((a - b).ToString()); break;
                        case "*": stack.Push((a * b).ToString()); break;
                        case "/": stack.Push((a / b).ToString()); break;
                    }
                }
            }
            return stack.Pop();
        }
    }
}
