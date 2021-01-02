using System;

namespace Genocs.MicroserviceLight.Template.BusHost.Exceptions
{
    public class BackendServiceCallFailedException : Exception
    {
        public BackendServiceCallFailedException()
        {
        }

        public BackendServiceCallFailedException(string message) : base(message)
        {
        }

        public BackendServiceCallFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
