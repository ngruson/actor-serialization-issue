using Dapr.Actors.Client;
using Dapr.Actors;
using System.Text.Json;
using WebApi;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<PersonActor>();
    options.UseJsonSerialization = true;
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("/test", async (IActorProxyFactory actorProxyFactory) =>
{
    ActorProxyOptions actorProxyOptions = new()
    {
        UseJsonSerialization = true,
        JsonSerializerOptions = new JsonSerializerOptions()
    };

    Guid guid = Guid.CreateVersion7();
    ActorId actorId = new(guid.ToString());
    IPersonActor actorProxy = actorProxyFactory.CreateActorProxy<IPersonActor>(actorId, 
        nameof(PersonActor), actorProxyOptions);

    Person person = await actorProxy.ReturnPerson();

    return person;
});

app.MapActorsHandlers();

app.Run();