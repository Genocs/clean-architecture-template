namespace Genocs.MicroserviceLight.Template.Infrastructure.ServiceBus
{
    public class MassTransitSetting
    {
        public static string Position = "MassTransitSettings";
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
