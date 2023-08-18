## TL;DR
More or less a sandbox for me to play with React, [(Microsoft) Orleans](https://learn.microsoft.com/en-us/dotnet/orleans/), ASP.NET and [RavenDB](https://ravendb.net/). A fresh new stack I call the ROAR stack.

## A bit more...
The SPA, hosted by [Vercel](https://vercel.com/), will be a dashboard that contains tiles with different graphs that represent some data either pushed by IoT devices at my home or requested from APIs. The tiles themselves will be updated in real time via SignalR which will use the Orleans cluster as a backplane for messaging those clients. Grains, the central primitive of Microsoft Orleans, will do the work of being connected to some thing or API that will produce data. I made a pretty graphic just to show how much I am invested in playing around with this awesome stack that I literally just made up for internal enlightment, grins and *science*! 🧪


![dashy](https://github.com/nonware/Dashy/assets/19826250/73d30f5a-c3d7-4924-96b9-f28842e83417)
