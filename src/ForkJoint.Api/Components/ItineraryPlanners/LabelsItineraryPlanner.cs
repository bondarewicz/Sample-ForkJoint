using MassTransit.Courier.Contracts;

namespace ForkJoint.Api.Components.ItineraryPlanners;

using System;
using System.Threading.Tasks;
using Activities;
using MassTransit;
using ForkJoint.Domain.Leg;

public class LabelsItineraryPlanner :
    IItineraryPlanner<RequestLabelGeneration>
{
    private readonly Uri _labelAddress;

    public LabelsItineraryPlanner(IEndpointNameFormatter formatter)
    {
        _labelAddress = new Uri($"exchange:{formatter.ExecuteActivity<GenerateLabelActivity, GenerateLabelArguments>()}");
    }

    public async Task PlanItinerary(BehaviorContext<FutureState, RequestLabelGeneration> context, IItineraryBuilder builder)
    {
        var createLegLabel = context.Message;

        builder.AddVariable(nameof(RequestLabelGeneration.ShipmentId), createLegLabel.ShipmentId);
        builder.AddVariable(nameof(RequestLabelGeneration.ShipmentLineId), createLegLabel.ShipmentLineId);

        var leg = createLegLabel.Leg;

        //todo this throws if Labels == null
        if (leg.Labels)
        {
            builder.AddActivity(nameof(GenerateLabelActivity), _labelAddress, new
            {
                // leg.Labels,
                // leg.Invoice,
                // leg.Receipt,
                leg.LegData
            });    
            
            //todo 
            // builder.AddActivity(nameof(ConvertLabelActivity), _labelAddress, new
            // {
            //     leg.Labels,
            //     leg.Invoice,
            //     leg.Receipt,
            //     leg.LegData
            // });   
        }
    }
}