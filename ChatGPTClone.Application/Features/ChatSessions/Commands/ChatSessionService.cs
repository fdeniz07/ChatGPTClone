using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands
{
    public class ChatSessionService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ChatSessionService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        DbSet<ChatSession> ChatSessions { get; set; }

        public async Task CreateChatSessionAsync(ChatSession chatSession, CancellationToken cancellationToken)
        {
            _applicationDbContext.ChatSessions.Add(chatSession);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
