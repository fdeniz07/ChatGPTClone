using ChatGPTClone.Application.Features.ChatSessions.Commands.Create;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;
using MediatR;
using ChatGPTClone.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;



namespace ChatGPTClone.WebApi.Controllers;
public class ChatSessionsController : ApiControllerBase
{

    private readonly IStringLocalizer<GlobalExceptionFilter> _localizer;
    public ChatSessionsController(ISender mediatr, IStringLocalizer<GlobalExceptionFilter> localizer) : base(mediatr)
    {
        _localizer = localizer;
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