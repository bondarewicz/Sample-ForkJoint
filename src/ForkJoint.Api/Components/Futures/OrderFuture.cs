namespace ForkJoint.Api.Components.Futures;

using System.Linq;
using MassTransit;
using Domain.Burger;
using Domain.Fry;
using Domain.FryShake;
using Domain.Order;
using Domain.OrderLine;
using Domain.Shake;

public class OrderFuture :
    Future<SubmitOrder, OrderCompleted, OrderFaulted>
{
    public OrderFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.OrderId));

        SendRequests<Burger, OrderBurger>(x => x.Burgers, x =>
            {
                x.UsingRequestInitializer(MapOrderBurger);
                x.TrackPendingRequest(message => message.OrderLineId);
            })
            .OnResponseReceived<BurgerCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));

        SendRequests<Fry, OrderFry>(x => x.Fries, x =>
            {
                x.UsingRequestInitializer(MapOrderFry);
                x.TrackPendingRequest(message => message.OrderLineId);
            })
            .OnResponseReceived<FryCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));

        SendRequests<Shake, OrderShake>(x => x.Shakes, x =>
            {
                x.UsingRequestInitializer(MapOrderShake);
                x.TrackPendingRequest(message => message.OrderLineId);
            })
            .OnResponseReceived<ShakeCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));

        SendRequests<FryShake, OrderFryShake>(x => x.FryShakes, x =>
            {
                x.UsingRequestInitializer(MapOrderFryShake);
                x.TrackPendingRequest(message => message.OrderLineId);
            })
            .OnResponseReceived<FryShakeCompleted>(x => x.CompletePendingRequest(message => message.OrderLineId));


        WhenAllCompleted(r => r.SetCompletedUsingInitializer(context => new
        {
            LinesCompleted = context.Saga.Results.Select(x => context.ToObject<OrderLineCompleted>(x.Value)).ToDictionary(x => x.OrderLineId),
        }));

        WhenAnyFaulted(f => f.SetFaultedUsingInitializer(MapOrderFaulted));
    }

    static object MapOrderFryShake(BehaviorContext<FutureState, FryShake> context)
    {
        return new
        {
            OrderId = context.Saga.CorrelationId,
            OrderLineId = context.Message.FryShakeId,
            context.Message.Size,
            context.Message.Flavor
        };
    }

    static object MapOrderShake(BehaviorContext<FutureState, Shake> context)
    {
        return new
        {
            OrderId = context.Saga.CorrelationId,
            OrderLineId = context.Message.ShakeId,
            context.Message.Size,
            context.Message.Flavor
        };
    }

    static object MapOrderFry(BehaviorContext<FutureState, Fry> context)
    {
        return new
        {
            OrderId = context.Saga.CorrelationId,
            OrderLineId = context.Message.FryId,
            context.Message.Size,
        };
    }

    static object MapOrderBurger(BehaviorContext<FutureState, Burger> context)
    {
        return new
        {
            OrderId = context.Saga.CorrelationId,
            OrderLineId = context.Message.BurgerId,
            Burger = context.Message
        };
    }

    static object MapOrderFaulted(BehaviorContext<FutureState> context)
    {
        var faults = context.Saga.Faults.ToDictionary(x => x.Key, x => context.ToObject<Fault>(x.Value));

        return new
        {
            LinesCompleted = context.Saga.Results.ToDictionary(x => x.Key, x => context.ToObject<OrderLineCompleted>(x.Value)),
            LinesFaulted = faults,
            Exceptions = faults.SelectMany(x => x.Value.Exceptions).ToArray()
        };
    }
}