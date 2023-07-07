using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public class ValueIsNullException : Exception
    {
        public ValueIsNullException()
        {
        }

        public ValueIsNullException(string? message) : base(message)
        {
        }

        public ValueIsNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ValueIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}