using ForkJoint.Domain.Shipment;

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
    private readonly IRequestClient<ProcessShipment> _client;

    public ShipmentController(IRequestClient<ProcessShipment> client)
    {
        _client = client;
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
        try
        {
            Response response = await _client.GetResponse<ProcessShipmentCompleted, ProcessShipmentFaulted>(new
            {
                shipment.ShipmentId,
                shipment.Labels,
                shipment.Invoices
            });

            return response switch
            {
                (_, ProcessShipmentCompleted completed) => Ok(new
                {
                    completed.ShipmentId,
                    completed.Created,
                    completed.Completed,
                    LinesCompleted = completed.LinesCompleted.ToDictionary(x => x.Key, x => new
                    {
                        x.Value.Created,
                        x.Value.Completed,
                        x.Value.Description,
                    })
                }),
                (_, ProcessShipmentFaulted faulted) => BadRequest(new
                {
                    faulted.ShipmentId,
                    faulted.Created,
                    faulted.Faulted,
                    LinesCompleted = faulted.LinesCompleted.ToDictionary(x => x.Key, x => new
                    {
                        x.Value.Created,
                        x.Value.Completed,
                        x.Value.Description,
                    }),
                    LinesFaulted = faulted.LinesFaulted.ToDictionary(x => x.Key, x => new
                    {
                        Faulted = x.Value.Timestamp,
                        Reason = x.Value.GetExceptionMessages(),
                    })
                }),
                _ => BadRequest()
            };
        }
        catch (RequestTimeoutException)
        {
            return Accepted(new
            {
                shipment.ShipmentId,
                Accepted = shipment.Labels.Select(x => x.LabelId).ToArray()
            });
        }
    }
}