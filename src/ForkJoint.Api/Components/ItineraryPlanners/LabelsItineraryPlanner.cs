using MassTransit.Courier.Contracts;

namespace ForkJoint.Api.Components.ItineraryPlanners;

using System;
using System.Threading.Tasks;
using Activities;
using MassTransit;
using ForkJoint.Domain.Leg;

public class LabelsItineraryPlanner :
    IItineraryPlanner<CreateLegLabel>
{
    private readonly Uri _labelAddress;

    public LabelsItineraryPlanner(IEndpointNameFormatter formatter)
    {
        _labelAddress = new Uri($"exchange:{formatter.ExecuteActivity<GenerateLabelActivity, GenerateLabelArguments>()}");
    }

    public async Task PlanItinerary(BehaviorContext<FutureState, CreateLegLabel> context, IItineraryBuilder builder)
    {
        var createLegLabel = context.Message;

        builder.AddVariable(nameof(CreateLegLabel.ShipmentId), createLegLabel.ShipmentId);
        builder.AddVariable(nameof(CreateLegLabel.ShipmentLineId), createLegLabel.ShipmentLineId);

        var leg = createLegLabel.Leg;

        //todo this throws if Labels == null
        if (leg.Labels)
        {
            builder.AddActivity(nameof(GenerateLabelActivity), _labelAddress, new
            {
                leg.Labels,
                leg.Invoice,
                leg.Receipt,
                leg.LegData
            });    
        }
        
        // builder.AddSubscription(new Uri("rabbitmq://localhost/log-events"), RoutingSlipEvents.All);
    }
}