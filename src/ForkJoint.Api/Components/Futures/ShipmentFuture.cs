using ForkJoint.Domain.Leg;
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

        SendRequests<Leg, OrderLeg>(x => x.Legs, x =>
            {
                x.UsingRequestInitializer(MapOrderLeg);
                x.TrackPendingRequest(message => message.ShipmentLineId);
            })
            .OnResponseReceived<LegCompleted>(x => x.CompletePendingRequest(message => message.ShipmentLineId));

        
        // SendRequests<Fry, OrderFry>(x => x.Fries, x =>
        //     {
        //         x.UsingRequestInitializer(MapOrderFry);
        //         x.TrackPendingRequest(message => message.OrderLineId);
        //     })
        //     .OnResponseReceived<FryCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));
        //
        // SendRequests<Shake, OrderShake>(x => x.Shakes, x =>
        //     {
        //         x.UsingRequestInitializer(MapOrderShake);
        //         x.TrackPendingRequest(message => message.OrderLineId);
        //     })
        //     .OnResponseReceived<ShakeCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));
        //
        // SendRequests<FryShake, OrderFryShake>(x => x.FryShakes, x =>
        //     {
        //         x.UsingRequestInitializer(MapOrderFryShake);
        //         x.TrackPendingRequest(message => message.OrderLineId);
        //     })
        //     .OnResponseReceived<FryShakeCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));


        WhenAllCompleted(r => r.SetCompletedUsingInitializer(context => new
        {
            LinesCompleted = context.Saga.Results.Select(x => context.ToObject<ShipmentLineCompleted>(x.Value)).ToDictionary(x => x.ShipmentLineId),
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