namespace Genocs.MicroserviceLight.Template.Shared.Commands
{
    public class SimpleMessage : Interfaces.ICommand
    {
        public string MessageId { get; set; }

        public string MessageBody { get; set; }

    }
}
