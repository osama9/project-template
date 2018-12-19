using System;
using System.Collections.Generic;

namespace ProjectTemplate.Core.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public List<string> Errors { get; protected set; }

        public string SqlExceptionMessage { get; protected set; }

        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception inner) : base(message, inner)
        {

        }

        public BusinessException(List<string> erros)
        {
            this.Errors = erros;
        }

        protected BusinessException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {

        }
    }
}