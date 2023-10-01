using System.Threading.Channels;
using Containerizer.WebApi.Abstractions.Repositories;
using Containerizer.WebApi.Abstractions.Services;
using Containerizer.WebApi.Processors;
using Containerizer.WebApi.Repositories;
using Containerizer.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Channel<Action>>(Channel.CreateUnbounded<Action>());
builder.Services.AddHostedService<ContainerActionProcessor>();

builder.Services.AddSingleton<IContainerRepository, ContainerRepository>();
builder.Services.AddScoped<IContainerService, ContainerService>();

builder.Services.AddLogging(log => log.AddConsole());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
