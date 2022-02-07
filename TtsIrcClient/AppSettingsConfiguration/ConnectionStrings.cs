using System.Diagnostics.CodeAnalysis;

namespace TtsIrcClient.AppSettingsConfiguration;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class ConnectionStrings
{
    private readonly string? _icdbV3Db;

    public string? TtsDb
    {
        //Try env var first else use appsettings.json
        get => Environment.GetEnvironmentVariable(@"TTSIRCCLIENT_CONNECTIONSTRINGS_DB") ?? _icdbV3Db;
        init => _icdbV3Db = value;
    }
}
