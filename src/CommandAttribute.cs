using System;

namespace StreamChat.Cli
{
    public class CommandDescriptorAttribute : Attribute
    {
        public CommandDescriptorAttribute(string entity, string action) : this(entity, action, (string[])null)
        {
        }

        public CommandDescriptorAttribute(string entity, string action, string parameter) : this(entity, action, new[] {parameter})
        {
        }

        public CommandDescriptorAttribute(string entity, string action, string[] parameters)
        {
            Entity = entity;
            Action = action;
            Parameters = parameters;
        }

        public string Entity {get; set;}
        public string Action {get; set;}
        public string[] Parameters {get; set;}
    }
}