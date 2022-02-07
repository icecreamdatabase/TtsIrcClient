FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY *.sln .
COPY TwitchIrcHubClient/*.csproj ./TwitchIrcHubClient/
COPY TtsIrcClient/*.csproj ./TtsIrcClient/

RUN ls -la
RUN ls -la TwitchIrcHubClient
RUN ls -la TtsIrcClient

RUN dotnet restore

COPY TwitchIrcHubClient/. ./TwitchIrcHubClient/
COPY TtsIrcClient/. ./TtsIrcClient/

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "TtsIrcClient.dll"]