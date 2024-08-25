using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll
{
    public sealed class ChatSessionGetAllQuery:IRequest<List<ChatSessionGetAllDto>>
    {
        public ChatSessionGetAllQuery()
        {
            
        }
    }
}
