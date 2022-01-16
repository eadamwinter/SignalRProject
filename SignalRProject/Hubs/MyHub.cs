
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRProject
{
    public class MyHub : Hub
    {
        private readonly ParenthesisChecker _pc;

        public MyHub(ParenthesisChecker pc)
        {
            _pc = pc;
        }
        public void SendMessageToCaller(string input)
        {
            string result = _pc.CheckParenthesis(input);
            Clients.Caller.SendAsync("DisplayResult", result);
        }
    }

}