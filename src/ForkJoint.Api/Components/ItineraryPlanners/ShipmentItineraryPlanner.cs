namespace ForkJoint.Api.Components.ItineraryPlanners;

using System;
using System.Threading.Tasks;
using Activities;
using MassTransit;
using ForkJoint.Domain.Leg;

public class ShipmentItineraryPlanner :
    IItineraryPlanner<OrderLeg>
{
    // readonly Uri _invoiceAddress;
    private readonly Uri _labelAddress;

    public ShipmentItineraryPlanner(IEndpointNameFormatter formatter)
    {
        _labelAddress = new Uri($"exchange:{formatter.ExecuteActivity<GenerateLabelZplActivity, GenerateLabelZplArguments>()}");
        // _invoiceAddress = new Uri($"exchange:{formatter.ExecuteActivity<GenerateInvoiceActivity, GenerateInvoiceArguments>()}");
    }

    public async Task PlanItinerary(BehaviorContext<FutureState, OrderLeg> context, IItineraryBuilder builder)
    {
        var orderLeg = context.Message;

        builder.AddVariable(nameof(OrderLeg.ShipmentId), orderLeg.ShipmentId);
        builder.AddVariable(nameof(OrderLeg.ShipmentLineId), orderLeg.ShipmentLineId);

        var leg = orderLeg.Leg;

        if (leg.Labels)
        {
            builder.AddActivity(nameof(GenerateLabelZplActivity), _labelAddress, new
            {
                leg.Labels,
                leg.Invoice,
                leg.Receipt,
                leg.LegData
            });    
        }

        if (leg.Invoice)
        {
            // builder.AddActivity(nameof(GenerateInvoiceActivity), _invoiceAddress, new
            // {
            //     leg.Labels,
            //     leg.Invoice,
            //     leg.Receipt,
            //     leg.LegData
            // }); 
        }
        

        // todo 
        // builder.AddActivity(nameof(CombineDocumentActivity), _combineAddress, new
        // {
        //     leg.Labels,
        //     leg.Invoice,
        //     leg.Receipt
        // });
    }
}