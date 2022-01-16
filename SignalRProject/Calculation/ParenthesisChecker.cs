using System;
using System.Collections;
using System.Collections.Generic;

namespace SignalRProject
{
    public class ParenthesisChecker
    {
        public string CheckParenthesis(string StringToCheck)
        {
            string result = "Please enter some string to check.";

            if(StringToCheck.Length == 0)
                return result;

            Stack ParenthesisStack = new Stack();
            
            // setting result in case that the given string is incorrect
            result = "Wrong combination";

            for(var i = 0; i<StringToCheck.Length; i++)
            {
                char sign = StringToCheck[i];

                if(CheckForClosedParenthesis(sign))
                {
                    if(ParenthesisStack.Count == 0)
                    {
                        return result;
                    }

                    char signFromStack = (char)ParenthesisStack.Pop();

                    if(!CheckForCorrectParenthesis(sign, signFromStack))
                    {
                        return result;
                    }
                    
                }
                else
                {
                    if(CheckForOpenParenthesis(sign))
                    {
                        ParenthesisStack.Push(sign);
                    }
                }
                
            }

            if(ParenthesisStack.Count == 0)
                result = "The given combination is correct";

            return result;
        }

        private bool CheckForOpenParenthesis(char sign)
        {
            List<int> AsciiCodes = new List<int>() {40, 60, 91, 123};
            if(AsciiCodes.Contains((int)sign))
            {
                return true;
            }
            return false;
        }

        private bool CheckForClosedParenthesis(char sign) 
        {
            List<int> AsciiCodes = new List<int>() {41, 62, 93, 125};
            if(AsciiCodes.Contains((int)sign))
            {
                return true;
            }
            return false;
        }

        private bool CheckForCorrectParenthesis(char sign, char signFromStack)
        {
            int ClosingParenthesis = (int)sign;
            int OpeningParenthesis = (int)signFromStack;

            if(OpeningParenthesis == 40 && ClosingParenthesis == OpeningParenthesis+1)
            {
                return true;
            }
            if(ClosingParenthesis == OpeningParenthesis+2)
            {
                return true;
            }
            
            return false;
        }
    }
}