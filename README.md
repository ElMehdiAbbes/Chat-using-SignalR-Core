# Chat app using SignalR Core

Chat web application using SignalR.

## Framework
[![Generic badge](https://img.shields.io/badge/.NET%20CORE-3.1-red.svg)](https://shields.io/)

## Requirements

Please make sure to install one of the following

[![Generic badge](https://img.shields.io/badge/SDK-Download-blue.svg)](https://download.visualstudio.microsoft.com/download/pr/9f010da2-d510-4271-8dcc-ad92b8b9b767/d2dd394046c20e0563ce5c45c356653f/dotnet-runtime-3.1.0-win-x64.exe
)
[![Generic badge](https://img.shields.io/badge/RUNTIME-Download-blue.svg)](https://download.visualstudio.microsoft.com/download/pr/639f7cfa-84f8-48e8-b6c9-82634314e28f/8eb04e1b5f34df0c840c1bffa363c101/dotnet-sdk-3.1.100-win-x64.exe
)

## SignalR Core Code changes
### Server Side
- SignalR service
```csharp
services.AddSignalR();
```
- Adding new Hub in the startup.cs file
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapHub<ChatHub>("/chathub");
});
```
- Hub Method example
```csharp
public async Task SendMessage(string user, string message)
{
    HashSet<string> connectedUsers = new HashSet<string>();
    string key = user;
    foreach (var connectionId in connections.GetConnections(key))
    {
        connectedUsers.Add(connectionId);
    }
    // Send Messages to all users
    await Clients.All.SendAsync("ReceiveMessage", user, message);
    // Send messages to specific users
    // await Clients.Clients(connectedUsers.ToList()).SendAsync("ReceiveMessage", user, message);
}
```

### Client Side

- Install SignalR js lib
[SignalR Core client installation](https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-3.1#install-the-signalr-client-package)

- Start Connection
```js
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
```

- Call method
```js
connection.invoke("SendMessage", user, message).catch(function (err) {
    return console.error(err.toString());
});
```

- Receive method
```js
connection.on("ReceiveMessage", function (user, message) {
    // your code here ...
});
```
