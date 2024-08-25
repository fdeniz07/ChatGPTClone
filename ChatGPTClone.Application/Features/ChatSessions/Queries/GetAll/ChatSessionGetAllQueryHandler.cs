using System.Security.Cryptography.X509Certificates;
using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll
{
    public class ChatSessionGetAllQueryHandler : IRequestHandler<ChatSessionGetAllQuery, List<ChatSessionGetAllDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public ChatSessionGetAllQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public Task<List<ChatSessionGetAllDto>> Handle(ChatSessionGetAllQuery request, CancellationToken cancellationToken)
        {
            return _dbContext
                .ChatSessions
                .AsNoTracking()
                .Where(c => c.AppUserId == _currentUserService.UserId)
                .OrderByDescending(cs => cs.CreatedOn)
                .Select(x => ChatSessionGetAllDto.MapFromChatSession(x))
                .ToListAsync(cancellationToken);
        }
    }
}
