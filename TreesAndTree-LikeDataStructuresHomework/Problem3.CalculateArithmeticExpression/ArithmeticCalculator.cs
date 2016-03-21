using System;
namespace Problem3.CalculateArithmeticExpression
{
    using System.Collections.Generic;

    public class ArithmeticCalculator
    {
        private static void Main()
        {
            string arithmeticExpression = Console.ReadLine();
            string[] tokensCollection = arithmeticExpression.Split(' ');
            double result = Double.NaN;
            try
            {
                var output = GetReversePolishNotation(tokensCollection);
                result = CalculateExpression(output);
            }
            catch (ArgumentException)
            {

                Console.WriteLine("Error");
                return;
            }

            Console.WriteLine(result);
        }

        private static double CalculateExpression(Queue<string> reversePolishNotation)
        {
            Stack<double> values = new Stack<double>();
            while (reversePolishNotation.Count != 0)
            {
                var currentToken = reversePolishNotation.Dequeue();
                double number;
                if (double.TryParse(currentToken, out number))
                {
                    values.Push(number);
                }
                else
                {
                    if (values.Count < 2)
                    {
                        throw new ArgumentException("Error");
                    }

                    if (currentToken == "^")
                    {
                        double power = values.Pop();
                        double valueToRaise = values.Pop();
                        double resultToPush = Math.Pow(valueToRaise, power);
                        values.Push(resultToPush);
                    }
                    else if (currentToken == "*")
                    {
                        double argumentOne = values.Pop();
                        double argumentTwo = values.Pop();
                        double result = argumentOne * argumentTwo;
                        values.Push(result);
                    }
                    else if (currentToken == "/")
                    {
                        double argumentOne = values.Pop();
                        double argumentTwo = values.Pop();
                        double result = argumentTwo / argumentOne;
                        values.Push(result);
                    }
                    else if (currentToken == "+")
                    {
                        double argumentOne = values.Pop();
                        double argumentTwo = values.Pop();
                        double result = argumentOne + argumentTwo;
                        values.Push(result);
                    }
                    else if (currentToken == "-")
                    {
                        double argumentOne = values.Pop();
                        double argumentTwo = values.Pop();
                        double result = argumentTwo - argumentOne;
                        values.Push(result);
                    }
                }
            }

            if (values.Count != 1)
            {
                throw new ArgumentException("Error");
            }

            return values.Pop();
        }

        private static Queue<string> GetReversePolishNotation(string[] tokensCollection)
        {
            Queue<string> output = new Queue<string>();
            Stack<string> operatorsStack = new Stack<string>();
            foreach (var token in tokensCollection)
            {
                double number;
                if (double.TryParse(token, out number))
                {
                    output.Enqueue(token);
                }
                else if (token == "(")
                {
                    operatorsStack.Push(token);
                }
                else if (token == "^" ||
                         token == "*" || token == "/" ||
                         token == "+" || token == "-")
                {
                    Operator currentOperator = new Operator(token);
                    string operatorOnTop = string.Empty;
                    if (operatorsStack.Count != 0)
                    {
                        operatorOnTop = operatorsStack.Peek();
                    }

                    Operator operatorOnTopOfTheStack = new Operator(operatorOnTop);
                    bool shouldPopOperator = operatorsStack.Count != 0 &&
                                             ((currentOperator.IsLeftAssociative &&
                                               currentOperator.Precedence <= operatorOnTopOfTheStack.Precedence) ||
                                              (currentOperator.IsRightAssociative &&
                                               currentOperator.Precedence < operatorOnTopOfTheStack.Precedence));
                    while (shouldPopOperator)
                    {
                        output.Enqueue(operatorsStack.Pop());
                        if (operatorsStack.Count != 0)
                        {
                            operatorOnTopOfTheStack = new Operator(operatorsStack.Peek());
                        }

                        shouldPopOperator = operatorsStack.Count != 0 &&
                                            ((currentOperator.IsLeftAssociative &&
                                              currentOperator.Precedence <= operatorOnTopOfTheStack.Precedence) ||
                                             (currentOperator.IsRightAssociative &&
                                              currentOperator.Precedence < operatorOnTopOfTheStack.Precedence));
                    }

                    operatorsStack.Push(token);
                }
                else if (token == ")")
                {
                    var popedOperator = operatorsStack.Pop();
                    while (popedOperator != "(")
                    {
                        output.Enqueue(popedOperator);
                        popedOperator = operatorsStack.Pop();
                    }
                }
                else
                {
                    throw new ArgumentException("Token not recognized");
                }
            }

            while (operatorsStack.Count != 0)
            {
                output.Enqueue(operatorsStack.Pop());
            }
            return output;
        }
    }
}
