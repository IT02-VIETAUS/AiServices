using AiServices.Application;
using AiServices.Infrastructure;
using AiServices.Persistence;
using AiServices.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddHostedService<WorkerHeartbeatService>();

var host = builder.Build();
host.Run();
