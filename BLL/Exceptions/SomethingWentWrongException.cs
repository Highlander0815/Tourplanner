﻿using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    internal class SomethingWentWrongException : Exception
    {
        public SomethingWentWrongException()
        {
        }

        public SomethingWentWrongException(string? message) : base(message)
        {
        }

        public SomethingWentWrongException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SomethingWentWrongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}