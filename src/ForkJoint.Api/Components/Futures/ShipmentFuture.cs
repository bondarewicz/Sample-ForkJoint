using System;
using ForkJoint.Domain.Invoice;

namespace ForkJoint.Api.Components.Futures;

using System.Linq;
using MassTransit;
using ForkJoint.Domain.Leg;
using ForkJoint.Domain.Receipt;
using ForkJoint.Domain.Shipment;
using ForkJoint.Domain.ShipmentLine;

public class ShipmentFuture :
    Future<ProcessShipment, ProcessShipmentCompleted, ProcessShipmentFaulted>
{
    public ShipmentFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentId));

        
        SendRequests<Leg, RequestLabelGeneration>(x => x.Legs, x =>
            {
                x.UsingRequestInitializer(MapCreateLeg);
                x.TrackPendingRequest(message => message.ShipmentLineId);
            })
            .OnResponseReceived<LegLabelCompleted>(x => x.CompletePendingRequest(message => message.ShipmentLineId));

        SendRequest<RequestReceiptGeneration>(x =>
            {
                x.UsingRequestInitializer(context => new
                {
                    ShipmentId = context.Saga.CorrelationId,
                    ShipmentLineId = Guid.NewGuid(),
                    Quantity = 1
                });
        
                x.TrackPendingRequest(message => message.ShipmentLineId);
            })
            .OnResponseReceived<ReceiptCompleted>(x =>
            {
                x.CompletePendingRequest(message => message.ShipmentLineId);
            });
        
        SendRequest<RequestInvoiceGeneration>(x =>
            {
                x.UsingRequestInitializer(context => new
                {
                    ShipmentId = context.Saga.CorrelationId,
                    ShipmentLineId = Guid.NewGuid(),
                    Quantity = 5
                });
        
                x.TrackPendingRequest(message => message.ShipmentLineId);
            })
            .OnResponseReceived<InvoiceCompleted>(x =>
            {
                x.CompletePendingRequest(message => message.ShipmentLineId);
            });
        
        WhenAllCompleted(r => r.SetCompletedUsingInitializer(context => 
        {
            // var shipmentId = context.Saga.CorrelationId;
            
            // todo we now have multiple events on completion, try to get LegLabelCompleted and assign the labels
            // then do the same for ReceiptCompleted
            // var labels = context.Saga.Results.Select(x => context.ToObject<LegLabelCompleted>(x.Value))
            //     .ToDictionary(x => x.Leg?.LegId);

            return new
            {
                LinesCompleted = context.Saga.Results.Select(x => context.ToObject<ShipmentLineCompleted>(x.Value))
                    .ToDictionary(x => x.ShipmentLineId),
                // Labels = labels
            };
        }));

        WhenAnyFaulted(f => f.SetFaultedUsingInitializer(MapShipmentFaulted));
    }

    static object MapCreateLeg(BehaviorContext<FutureState, Leg> context)
    {
        return new
        {
            ShipmentId = context.Saga.CorrelationId,
            ShipmentLineId = context.Message.LegId,
            Leg = context.Message
        };
    }

    static object MapShipmentFaulted(BehaviorContext<FutureState> context)
    {
        var faults = context.Saga.Faults.ToDictionary(x => x.Key, x => context.ToObject<Fault>(x.Value));

        return new
        {
            LinesCompleted = context.Saga.Results.ToDictionary(x => x.Key, x => context.ToObject<ShipmentLineCompleted>(x.Value)),
            LinesFaulted = faults,
            Exceptions = faults.SelectMany(x => x.Value.Exceptions).ToArray()
        };
    }
}