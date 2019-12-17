using System;

namespace Core
{
    public class Message
    {
        public string Name { get; private set; }
        public string Id { get; private set; }

        public Message(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}