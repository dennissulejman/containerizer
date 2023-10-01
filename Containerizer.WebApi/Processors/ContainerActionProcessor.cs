using System.Threading.Channels;

namespace Containerizer.WebApi.Processors;

public class ContainerActionProcessor : IHostedService
{
    private readonly Channel<Action> _channel;

    public ContainerActionProcessor(Channel<Action> channel)
    {
        _channel = channel;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Factory.StartNew(async () =>
        {
            while (!_channel.Reader.Completion.IsCompleted)
            {
                Action act = await _channel.Reader.ReadAsync();
                act();
            }
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
