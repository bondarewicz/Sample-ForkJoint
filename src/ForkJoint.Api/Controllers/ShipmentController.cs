using ForkJoint.Domain.Receipt;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Controllers;

using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Domain.Future;
using Domain.Shipment;

[ApiController]
[Route("[controller]")]
public class ShipmentController :
    ControllerBase
{
    private readonly IRequestClient<ProcessShipment> _labelsClient;
    private readonly IRequestClient<CreateReceipt> _receiptClient;
    readonly ILogger<ShipmentController> _logger;

    public ShipmentController(
        IRequestClient<ProcessShipment> labelsClient, 
        IRequestClient<CreateReceipt> receiptClient,
        ILogger<ShipmentController> logger)
    {
        _labelsClient = labelsClient;
        _receiptClient = receiptClient;
        _logger = logger;
        
    }

    /// <summary>
    /// Submits an shipment
    /// <param name="shipment">The shipment model</param>
    /// <response code="200">The shipment has been completed</response>
    /// <response code="202">The shipment has been accepted but not yet completed</response>
    /// <response code="400">The shipment could not be completed</response>
    /// </summary>
    /// <param name="shipment"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(Shipment shipment)
    {
        _logger.LogDebug("Got ShipmentId {0}", shipment.ShipmentId);
        
        try
        {
            var response = await _labelsClient.GetResponse<ProcessShipmentCompleted, ProcessShipmentFaulted>(new
            {
                shipment.ShipmentId,
                shipment.Legs
            });
            
            
            // var response2 = await _receiptClient.GetResponse<ReceiptCompleted, ReceiptFaulted>(new
            // {
            //     shipment.ShipmentId,
            //     Quantity = 1
            // });

            
            if (response.Is(out Response<ProcessShipmentFaulted> faulted))
            {
                // var id = faulted.Message.ShipmentId
                return BadRequest(new
                {
                    faulted.Message.ShipmentId,
                    faulted.Message.Created,
                    faulted.Message.Faulted,
                    LinesCompleted = faulted.Message.LinesCompleted.ToDictionary(x => x.Key, x => new
                    {
                        x.Value.Created,
                        x.Value.Completed,
                        x.Value.Description,
                    }),
                    LinesFaulted = faulted.Message.LinesFaulted.ToDictionary(x => x.Key, x => new
                    {
                        Faulted = x.Value.Timestamp,
                        Reason = x.Value.GetExceptionMessages(),
                    })
                });
            }

            if (response.Is(out Response<ProcessShipmentCompleted> completed))
            {
                return Ok(new
                {
                    completed.Message.ShipmentId,
                    completed.Message.Created,
                    completed.Message.Completed,
                    // Labels = completed.Message.Labels.ToDictionary(x => x.Key, x => new
                    // {
                    //     Zpl = x.Value.Leg.LabelData
                    // }),
                    LinesCompleted = completed.Message.LinesCompleted.ToDictionary(x => x.Key, x => new
                    {
                        x.Value.Created,
                        x.Value.Completed,
                        x.Value.Description,
                    })
                });
            }
            
            return BadRequest();

            // response switch
            // {
            //     (_, ProcessShipmentCompleted completed) => Ok(new
            //         {
            //             completed.ShipmentId,
            //             completed.Created,
            //             completed.Completed,
            //             Labels = completed.Labels.ToDictionary(x => x.Key, x => new
            //             {
            //                 Zpl = x.Value.Leg.LabelData
            //             }),
            //             LinesCompleted = completed.LinesCompleted.ToDictionary(x => x.Key, x => new
            //             {
            //                 x.Value.Created,
            //                 x.Value.Completed,
            //                 x.Value.Description,
            //             })
            //         })
            //     (_, ProcessShipmentFaulted faulted) => BadRequest(new
            //     {
            //         faulted.ShipmentId,
            //         faulted.Created,
            //         faulted.Faulted,
            //         LinesCompleted = faulted.LinesCompleted.ToDictionary(x => x.Key, x => new
            //         {
            //             x.Value.Created,
            //             x.Value.Completed,
            //             x.Value.Description,
            //         }),
            //         LinesFaulted = faulted.LinesFaulted.ToDictionary(x => x.Key, x => new
            //         {
            //             Faulted = x.Value.Timestamp,
            //             Reason = x.Value.GetExceptionMessages(),
            //         })
            //     }),
            //     _ => BadRequest()
        }
        catch (RequestTimeoutException)
        {
            return Accepted(new
            {
                shipment.ShipmentId,
                Accepted = shipment.Legs.Select(x => x.LegId).ToArray()
            });
        }
    }
}