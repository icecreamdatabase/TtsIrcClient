using System.Diagnostics.CodeAnalysis;

namespace TtsIrcClient.AppSettingsConfiguration;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class DiscordWebhooks
{
    public string? Main { get; init; }
}
