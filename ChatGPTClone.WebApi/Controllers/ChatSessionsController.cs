using ChatGPTClone.Application.Features.ChatSessions.Commands.Create;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;
using MediatR;

namespace ChatGPTClone.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;


public class ChatSessionsController : ApiControllerBase
{

    public ChatSessionsController(ISender mediatr) : base(mediatr)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await Mediatr.Send(new ChatSessionGetAllQuery(), cancellationToken));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return Ok(await Mediatr.Send(new ChatSessionGetByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(ChatSessionCreateCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediatr.Send(command, cancellationToken));
    }
}