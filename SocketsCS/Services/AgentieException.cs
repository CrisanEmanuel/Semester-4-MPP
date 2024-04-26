using System;

namespace Services
{
    public class AgentieException : Exception
    {
        public AgentieException():base() { }

        public AgentieException(string msg) : base(msg) { }

        public AgentieException(string msg, Exception ex) : base(msg, ex) { }

    }
}