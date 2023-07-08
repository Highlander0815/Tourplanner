using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    internal class ResponseErrorOfApiException : Exception
    {
        public ResponseErrorOfApiException()
        {
        }

        public ResponseErrorOfApiException(string? message) : base(message)
        {
        }

        public ResponseErrorOfApiException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ResponseErrorOfApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}