using ChatGPTClone.Application.Common.Models.General;
using ChatGPTClone.Domain.ValueObjects;
using ChatGPTClone.Domain.Enums;
using MediatR;


namespace ChatGPTClone.Application.Features.ChatMessages.Commands.Create
{
    public class ChatMessageCreateCommand : IRequest<ResponseDto<List<ChatMessage>>>, IBaseRequest
    {
        public Guid ChatSessionId { get; set; }
        public string? ThreadId { get; set; }
        public string Content { get; set; }
        public GptModelType Model { get; set; }
    }
}
