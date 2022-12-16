using System;
using ForkJoint.Domain.Leg;
using ForkJoint.Domain.Receipt;
using ForkJoint.Domain.Shipment;
using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Api.Components.Futures;

using System.Linq;
using MassTransit;

public class ShipmentFuture :
    Future<ProcessShipment, ProcessShipmentCompleted, ProcessShipmentFaulted>
{
    public ShipmentFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentId));

        SendRequests<Leg, CreateLegLabel>(x => x.Legs, x =>
            {
                x.UsingRequestInitializer(MapOrderLeg);
                x.TrackPendingRequest(message => message.ShipmentLineId);
            })
            .OnResponseReceived<LegLabelCompleted>(x => x.CompletePendingRequest(message => message.ShipmentLineId));


        WhenAllCompleted(r => r.SetCompletedUsingInitializer(context => 
        {
            var shipmentId = context.Saga.CorrelationId;
            var labels = context.Saga.Results.Select(x => context.ToObject<LegLabelCompleted>(x.Value))
                .ToDictionary(x => x.Leg?.LegId);
            //
            // context.Publish<CreateReceipt>(new
            // {
            //     ShipmentId = shipmentId,
            //     ShipmentLineId = Guid.NewGuid(),
            //     ReceiptData = "adsd",
            //     Quantity = 1
            // });

            
            
            return new
            {
                LinesCompleted = context.Saga.Results.Select(x => context.ToObject<ShipmentLineCompleted>(x.Value))
                    .ToDictionary(x => x.ShipmentLineId),
                Labels = labels
            };
        }));

        WhenAnyFaulted(f => f.SetFaultedUsingInitializer(MapShipmentFaulted));
    }

    // static object MapOrderFryShake(BehaviorContext<FutureState, FryShake> context)
    // {
    //     return new
    //     {
    //         OrderId = context.Saga.CorrelationId,
    //         OrderLineId = context.Message.FryShakeId,
    //         context.Message.Size,
    //         context.Message.Flavor
    //     };
    // }
    //
    // static object MapOrderShake(BehaviorContext<FutureState, Shake> context)
    // {
    //     return new
    //     {
    //         OrderId = context.Saga.CorrelationId,
    //         OrderLineId = context.Message.ShakeId,
    //         context.Message.Size,
    //         context.Message.Flavor
    //     };
    // }
    //
    // static object MapOrderFry(BehaviorContext<FutureState, Fry> context)
    // {
    //     return new
    //     {
    //         OrderId = context.Saga.CorrelationId,
    //         OrderLineId = context.Message.FryId,
    //         context.Message.Size,
    //     };
    // }

    static object MapOrderLeg(BehaviorContext<FutureState, Leg> context)
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