using System.Diagnostics;
using TtsIrcClient.Handler.PrivMsg;
using TtsIrcClient.Model;
using TwitchIrcHubClient;

namespace TtsIrcClient;

public class Bot
{
    private readonly IrcHubClient _hub;
    private readonly PrivMsgHandler _privMsgHandler;
    private readonly int _botUserId;

    public Bot()
    {
        if (string.IsNullOrEmpty(Program.ConfigRoot.TwitchIrcHub.AppIdKey))
            throw new InvalidOperationException("No AppIdKey!");
        if (string.IsNullOrEmpty(Program.ConfigRoot.TwitchIrcHub.HubRootUri))
            throw new InvalidOperationException("No HubRootUri!");

        using TtsDbContext dbContext = new TtsDbContext();

        string? botUserIdStr = dbContext.BotData.Find("userId")?.Value;
        if (string.IsNullOrEmpty(botUserIdStr))
            throw new InvalidOperationException("No UserId found in DB!");
        _botUserId = int.Parse(botUserIdStr);

        _hub = new IrcHubClient(Program.ConfigRoot.TwitchIrcHub.AppIdKey, Program.ConfigRoot.TwitchIrcHub.HubRootUri);
        _hub.IncomingIrcEvents.OnConnId += OnConnId;

        _privMsgHandler = new PrivMsgHandler(_hub);
    }

    private void OnConnId(string connId)
    {
        Console.WriteLine($"Received connId: {connId}");
        Stopwatch sw = Stopwatch.StartNew();
        using TtsDbContext dbContext = new TtsDbContext();

        sw.Stop();
        Console.WriteLine($"Context creation: {sw.Elapsed.TotalMilliseconds} ms");
        sw = Stopwatch.StartNew();
        List<int> roomIds = dbContext.Channels
            .Where(channel => channel.Enabled)
            .Select(channel => channel.RoomId)
            .ToList();
        _hub.Api.Connections.SetChannels(_botUserId, roomIds).ConfigureAwait(false);
        sw.Stop();
        Console.WriteLine($"Query execution and SetChannels: {sw.Elapsed.TotalMilliseconds} ms");
    }
}
