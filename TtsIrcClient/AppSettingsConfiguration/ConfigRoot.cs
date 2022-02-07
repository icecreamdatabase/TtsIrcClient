using System.Diagnostics.CodeAnalysis;

namespace TtsIrcClient.AppSettingsConfiguration;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class ConfigRoot
{
    public ConnectionStrings ConnectionStrings { get; init; } = new();
    public DiscordWebhooks DiscordWebhooks { get; init; } = new();
    public TwitchIrcHub TwitchIrcHub { get; init; } = new();
}