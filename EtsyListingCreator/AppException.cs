using System;

namespace EtsyListingCreator
{
    public class AppException : Exception
    {
        public AppException(string message)
            : base(message)
        {
        }
    }
}