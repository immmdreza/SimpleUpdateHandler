# Simple Update Handler
A simple update handler for telegram bots in c#

This package is built mostly for Asp app and where you are dealing with `IServiceCollection` and
`IServiceProvider`.

The steps are simple:
1. Create a class where you're going to handle an update. Eg: `/start` command.
The class should be drived from `SimpleDiHandler<T>`, where T is incoming update type:
`Message`,`CallbackQuery` and etc.

Then override `HandleUpdate` abstract method, where you do what you want with your update.
```cs
using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace MyApplication.UpdateHandlers
{
    public class HandleStartCommand : SimpleDiHandler<Message>
    {
        protected override async Task HandleUpdate(SimpleContext<Message> ctx)
        {
            await ctx.Response("Started!");
        }
    }
}
```

`Response` is an extension method to make your life easier.

2. Add main update processor and your handlers to the service collection

```cs
builder.Services.AddUpdateProcessor(configure => configure
    .RegisterMessage<HandleStartCommand>(FilterCutify.OnCommand("start")));
```
> `ITelegramBotClient` should be added to service collection.
Simple and Quick.

3. Redirect your updates from controller to update processor

```cs
using Microsoft.AspNetCore.Mvc;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.Controllers;

public class WebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromServices]SimpleDiUpdateProcessor updateProcessor,
        [FromBody]Update update)
    {
        await updateProcessor.ProcessSimpleHandlerAsync(update);
        return Ok();
    }
}
```

4. you're done. you can repeat step 1 to handle different kind of updates.
And don't forget to register it ( step 2 )
