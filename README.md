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

## What else?
1. ### Carry handlers
I'm not sure if the naming is related but this type of handlers can help wait for an update inside a hander
> You need to inject `SimpleDiUpdateProcessor` in your handler ( Example below )

3. ### Dependency injection on handlers
You can use everything you added to `IServiceCollection` in update handlers ( Just like Controllers, i'm not going to tell more )

3. ### Extensions
There are a lot of extension methods to help you handle your updates.

Example with a hell of chaining
```cs
public class HandlerInHandler : SimpleDiHandler<Message>
{
    private readonly SimpleDiUpdateProcessor _processor;

    // Di
    public HandlerInHandler(SimpleDiUpdateProcessor processor)
    {
        _processor = processor;
    }

    protected override async Task HandleUpdate(SimpleContext<Message> ctx)
    {
        // Extension method
        await ctx.Response("Say hello ...");
        
        // Wait for a user response in private chat for 30 secs.
        await _processor.CarryUserResponse(ctx.Update.From!.Id, privateOnly: true)
             
            // Preform an action if response is not null
            .IfNotNull(async x =>
            {
                // Send "Hello There!" to user, if the text matches
                await x.If("^hello ", async y => await y.Response("Hello There!"))
                    .Else(async x=> await x.Response("Undefined response."));
            })
            
            // If the response is null, then it's timed out
            .Else(async x => await x.Response("You're timed out."));
    }
}
```
