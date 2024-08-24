using ChatGPTClone.Domain.Entities;
using ChatGPTClone.Domain.Enums;
using ChatGPTClone.Domain.ValueObjects;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public sealed class ChatSessionGetByIdDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public GptModelType Model { get; set; }

        public List<ChatThread> Threads { get; set; } = [];

        public Guid AppUserId { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public static ChatSessionGetByIdDto MapFromChatSession(ChatSession chatSessions)
        {
            return new ChatSessionGetByIdDto
            {
                Id = chatSessions.Id,
                Title = chatSessions.Title,
                Model = chatSessions.Model,
                Threads = chatSessions.Threads,
                AppUserId = chatSessions.AppUserId,
                CreatedOn = chatSessions.CreatedOn
            };
        }
    }
}
