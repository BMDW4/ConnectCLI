using System;
using System.Collections.Generic;

namespace WmdaConnect.Shared
{
    public class WmdaConnectException : Exception
    {
        public List<ErrorBase> Errors { get; } = new List<ErrorBase>();
        public WmdaConnectException(string message) : base(message)
        {
        }

        public WmdaConnectException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}