namespace Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;

public class MassTransitSettings
{
    public static string Position = "MassTransitSettings";
    public string HostName { get; init; } = "localhost";
    public string VirtualHost { get; init; } = "/";
    public string UserName { get; init; } = "guest";
    public string Password { get; init; } = "guest";
}
