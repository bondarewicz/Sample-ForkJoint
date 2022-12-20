namespace ForkJoint.Api.Components.ItineraryPlanners;

using System;
using System.Threading.Tasks;
using Activities;
using MassTransit;
using ForkJoint.Domain.Leg;

public class LabelsRoutingSlip :
    IItineraryPlanner<RequestLabelGeneration>
{
    private readonly Uri _labelAddress;
    private readonly Uri _conversionAddress;

    public LabelsRoutingSlip(IEndpointNameFormatter formatter)
    {
        _labelAddress = new Uri($"exchange:{formatter.ExecuteActivity<GenerateLabelActivity, GenerateLabelArguments>()}");
        _conversionAddress = new Uri($"exchange:{formatter.ExecuteActivity<ConvertLabelActivity, ConvertLabelArguments>()}");
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
                leg.LegData,
                leg.ZplData
            });    
            
            builder.AddActivity(nameof(ConvertLabelActivity), _conversionAddress, new
            {
                leg.LegData,
                leg.ZplData,
                leg.PdfData
            });   
        }
    }
}