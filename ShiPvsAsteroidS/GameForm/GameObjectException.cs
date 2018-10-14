using System;
using System.Runtime.Serialization;

namespace ShiPvsAsteroidS.GameForm
{
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