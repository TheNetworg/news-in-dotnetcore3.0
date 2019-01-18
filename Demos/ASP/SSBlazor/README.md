Server Side Blazor
---
Oproti standardnímu Blazoru nevyžaduje WebAssembly. Server Side Blazor komunikuje s frontendem pomocí SignalR.
## Demo
### SSBlazor.App
* Standardní Blazor aplikace
* Rozdíl oproti normální Blazor aplikaci - používá <b>blazor.server.js</b> namísto <b>blazor.webassembly.js</b>
* Dotnet kód neběží v prohlížeči
### SSBlazor.Server
* Blazor server
* Veškerý dotnet kód běží na serveru
* Developer tools - network - nestsahuje se žádné dll - otevře se websocket
* dotnet run
