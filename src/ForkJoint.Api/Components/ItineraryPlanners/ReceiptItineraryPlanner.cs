using System;
using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;
using ForkJoint.Api.Models;
using ForkJoint.Domain.Receipt;
using MassTransit;

namespace ForkJoint.Api.Components.ItineraryPlanners;

public class ReceiptItineraryPlanner :
    IItineraryPlanner<CreateReceipt>
{
    private readonly Uri _receiptAddress;

    public ReceiptItineraryPlanner(IEndpointNameFormatter formatter)
    {
        _receiptAddress = new Uri($"exchange:{formatter.ExecuteActivity<GenerateReceiptActivity, GenerateReceiptArguments>()}");
    }
    
    public async Task PlanItinerary(BehaviorContext<FutureState, CreateReceipt> context, IItineraryBuilder builder)
    {
        var createReceipt = context.Message;
        
        builder.AddVariable(nameof(CreateReceipt.ShipmentId), createReceipt.ShipmentId);
        builder.AddVariable(nameof(CreateReceipt.ShipmentLineId), createReceipt.ShipmentLineId);
        
        
        if (createReceipt.Quantity > 0)
        {
            builder.AddActivity(nameof(GenerateReceiptActivity), _receiptAddress, new
            {
                ShipmentId = createReceipt.ShipmentId,
                Data = createReceipt.ReceiptData
            });    
        }
    }
}