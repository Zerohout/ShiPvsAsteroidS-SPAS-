namespace Akhmerov_HomeWork_1
{
    using System;
    using System.Runtime.Serialization;

    internal class GameObjectException : Exception
    {
        public GameObjectException()
        {
        }

        public GameObjectException(string message) : base(message)
        {
        }
        
        protected GameObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}