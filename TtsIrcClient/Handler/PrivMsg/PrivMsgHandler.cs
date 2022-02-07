using System.Collections.Concurrent;
using TwitchIrcHubClient;
using TwitchIrcHubClient.DataTypes.Parsed.FromTwitch;

namespace TtsIrcClient.Handler.PrivMsg;

public class PrivMsgHandler
{
    private readonly IrcHubClient _hub;

    private ConcurrentDictionary<int, List<int>> _channelEditorCache = new();

    public PrivMsgHandler(IrcHubClient hub)
    {
        _hub = hub;
        _hub.IncomingIrcEvents.OnNewIrcPrivMsg += OnNewIrcPrivMsg;
    }

    private async void OnNewIrcPrivMsg(int botUserId, IrcPrivMsg ircPrivMsg)
    {
        await HandleUserCommands(botUserId, ircPrivMsg);
        await HandleModeratorOrEditorCommands(botUserId, ircPrivMsg);
        await HandleEditorCommands(botUserId, ircPrivMsg);
        //Console.WriteLine($"{botUserId} <-- #{ircPrivMsg.RoomName} {ircPrivMsg.UserName}: {ircPrivMsg.Message}");
    }

    private async Task HandleUserCommands(int botUserId, IrcPrivMsg ircPrivMsg)
    {
        // TODO: user commands
    }

    private async Task HandleModeratorOrEditorCommands(int botUserId, IrcPrivMsg ircPrivMsg)
    {
        if (
            // broadcaster
            ircPrivMsg.RoomId != ircPrivMsg.UserId &&
            // mod
            !ircPrivMsg.Badges.ContainsKey("mod") &&
            // editor
            (!_channelEditorCache.ContainsKey(ircPrivMsg.RoomId) ||
             !_channelEditorCache[ircPrivMsg.RoomId].Contains(ircPrivMsg.UserId)) //&&
            // bot admin / bot owner
        )
            return;


        // TODO: mod commands
    }

    private async Task HandleEditorCommands(int botUserId, IrcPrivMsg ircPrivMsg)
    {
        if (
            // broadcaster
            ircPrivMsg.RoomId != ircPrivMsg.UserId &&
            // editor
            (!_channelEditorCache.ContainsKey(ircPrivMsg.RoomId) ||
             !_channelEditorCache[ircPrivMsg.RoomId].Contains(ircPrivMsg.UserId)) //&&
            // bot admin / bot owner
        )
            return;


        // TODO: editor commands
    }
}
