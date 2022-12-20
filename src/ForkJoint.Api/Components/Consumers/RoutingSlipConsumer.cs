using System.Threading.Tasks;
using MassTransit.Courier.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Components.Consumers;

public class RoutingSlipConsumer : 
    IConsumer<RoutingSlipCompleted>, 
    IConsumer<RoutingSlipFaulted>,
    IConsumer<RoutingSlipActivityCompleted>,
    IConsumer<RoutingSlipActivityFaulted>
{
    readonly ILogger<RoutingSlipConsumer> _logger;
    
    //todo unable to observe below transition events, probably missing startup setup
    public RoutingSlipConsumer(ILogger<RoutingSlipConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<RoutingSlipCompleted> context)
    {
        _logger.LogDebug($"{nameof(RoutingSlipCompleted)} for {context.Message.ToString()}");
    }

    public async Task Consume(ConsumeContext<RoutingSlipFaulted> context)
    {
        // throw new System.NotImplementedException();
    }

    public async Task Consume(ConsumeContext<RoutingSlipActivityCompleted> context)
    {
        _logger.LogDebug($"{nameof(RoutingSlipActivityCompleted)} for {context.Message.ToString()}");
    }

    public async Task Consume(ConsumeContext<RoutingSlipActivityFaulted> context)
    {
        // throw new System.NotImplementedException();
    }
}