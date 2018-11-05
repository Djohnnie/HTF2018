using System;

namespace HTF2018.Backend.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(String message) : base(message)
        {

        }
    }
}