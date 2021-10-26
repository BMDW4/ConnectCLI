using System;
using System.Collections.Generic;

namespace WmdaConnect.Shared
{
    public class ErrorBase
    {
        public ErrorBase()
        {

        }

        protected ErrorBase(string type, string code, string message)
        {
            Type = type;
            Code = code;
            Message = message;
        }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        public Guid ErrorId { get; set; }
    }
}