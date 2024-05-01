using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AlgorithmsAndDataStructures
{
    class Project2
    {
        //the main method calls the menu system
        public static void Main()
        {
            Console.Write("Please input an equation: ");
            string userEquationString = Console.ReadLine();
            Console.WriteLine("\n");
            //adds a # to the end of the string so i know when it ends
            userEquationString += "#";

            var userEquationList = new List<char>();
            int i = 0;
            while (i != userEquationString.Length)
            {
                foreach (char x in userEquationString)
                {
                    userEquationList.Add(x);
                    i++;
                }
            }

            //this will put each character in the users input into an array seperately
            char[] userEquation = userEquationList.ToArray();

            //check to see if its in correctly
            foreach (char item in userEquationList)
            {
                Console.WriteLine(item.ToString());
            }

            string postFix = infixToPostfix(userEquation);
            Console.WriteLine("Postfix string: " + postFix);
            postFix = postFix.Replace("#", null);
            Console.WriteLine("Postfix string: " + postFix);

            string answer = evaluation(postFix);
            Console.WriteLine(answer);

        }
        public static int checkOperand(char userEquation)
        {
            char[] okayOperands = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            if (okayOperands.Contains(userEquation))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static int checkOperator(char userEquation)
        {
            char[] okayOperators = { '+', '-', '*', '/', '^', '<', '>', '=', '!' };
            if (okayOperators.Contains(userEquation))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //the in stack priority order
        public static int inStackPrio(char userEquation)
        {
            switch (userEquation)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                    //case '#':
                    //    return 5;
            }
            return 0;
        }
        //the incoming priority order
        public static int inComingPrio(char userEquation)
        {
            switch (userEquation)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 4;
            }
            return 0;
        }
        public static string infixToPostfix(char[] userEquation)
        {
            Console.WriteLine(userEquation);
            //stacks
            Stack<char> operatorStack = new Stack<char>();
            operatorStack.Push('#');
            //current operand
            string currentOperand = "";
            //final postfix string
            string postfixString = "";

            int count = 0;
            while (userEquation.Length > count)
            {
                //if its a number and has a space or # after it add it to postfix string along with the space
                if (checkOperand(userEquation[count]) == 1 && (userEquation[count + 1] == ' ' || userEquation[count + 1] == '#'))
                {
                    postfixString += userEquation[count] + " ";
                    count++;
                }
                //if its a number and does NOT have a space after it add to postfix
                else if (checkOperand(userEquation[count]) == 1 && userEquation[count + 1] != ' ')
                {
                    postfixString += userEquation[count];
                    count++;
                }
                else if (userEquation[count] == ' ')
                {
                    count++;
                }
                //checks if its a valid operator
                else if (checkOperator(userEquation[count]) == 1 && userEquation[count + 1] == ' ')
                {
                    //remove the top item from the stack until its empty or until stack>incoming
                    //and add the removed values to the postfix string with a space
                    while (inStackPrio(operatorStack.Peek()) >= inComingPrio(userEquation[count]))
                    {
                        postfixString += operatorStack.Pop() + " ";
                    }
                    //then add the current operator to the stack
                    operatorStack.Push(userEquation[count]);

                    count++;
                }

                //if the value is # unload the stack and return the string
                else if (userEquation[count] == '#')
                {
                    while (operatorStack.Count > 0)
                    {
                        postfixString += operatorStack.Pop() + " ";
                    }
                    count++;
                }
            }
            return postfixString;
        }
        public static string evaluation(string postFix)
        {
            //operators
            string[] okayOperators = { "+", "-", "*", "/", "^", "<", ">", "=", "!" };
            //splits the string so the operators and operands are all seperated
            string[] seperatedPostfixA = postFix.Split(" ");
            List<string> seperatedPostfixB = new List<string>();

            foreach (string item in seperatedPostfixA)
            {
                if (item != "")
                {
                    seperatedPostfixB.Add(item);
                }
            }

            Stack<string> calculateStack = new Stack<string>();

            foreach (string character in seperatedPostfixB)
            {
                if (okayOperators.Contains(character))
                {
                    double operand2 = Convert.ToDouble(calculateStack.Pop());
                    double operand1 = Convert.ToDouble(calculateStack.Pop());

                    calculateStack.Push(doOperation(operand1, operand2, character));
                }
                else
                {
                    calculateStack.Push(character);
                }
            }
            return calculateStack.Pop();
        }
        public static string doOperation(double operand1, double operand2, string character)
        {
            switch (character)
            {
                case "+":
                    return (Convert.ToString(operand1 + operand2));
                case "-":
                    return (Convert.ToString(operand1 - operand2));
                case "*":
                    return (Convert.ToString(operand1 * operand2));
                case "/":
                    return (Convert.ToString(operand1 / operand2));
                case "^":
                    return (Convert.ToString(Math.Pow(operand1, operand2)));
            }
            return "";

        }
    }
}