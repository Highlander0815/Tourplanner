using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public class NoToursException : Exception
    {
        public NoToursException()
        {
        }

        public NoToursException(string? message) : base(message)
        {
        }

        public NoToursException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoToursException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}