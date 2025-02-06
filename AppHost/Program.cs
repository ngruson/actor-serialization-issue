using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Dapr;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<RedisResource> redis = builder.AddRedis("redis");

IResourceBuilder<IDaprComponentResource> stateStore = builder.AddDaprStateStore("statestore",
    new DaprComponentOptions
    {
        LocalPath = "./components/statestore.yaml"
    });

builder.AddProject<Projects.WebApi>("webapi")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppProtocol = "https"
    })
    .WithReference(stateStore)
    .WaitFor(redis);

builder.Build().Run();