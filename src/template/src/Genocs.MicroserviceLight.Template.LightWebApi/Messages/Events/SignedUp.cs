namespace Genocs.MicroserviceLight.Template.Messages.Events
{
    using System;
    using Genocs.Microservices.Common.Messages;
    using Newtonsoft.Json;

    [MessageNamespace("identity")]
    public class SignedUp : IEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignedUp(Guid userId, string email, string role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
    }
}