using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public sealed class ChatSessionGetByIdQueryHandler : IRequestHandler<ChatSessionGetByIdQuery, ChatSessionGetByIdDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        private const string CacheKey = "ChatSessionGetById_";
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public ChatSessionGetByIdQueryHandler(IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                .SetPriority(CacheItemPriority.High);
        }

        public async Task<ChatSessionGetByIdDto> Handle(ChatSessionGetByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CacheKey}{request.Id}";

            if (_memoryCache.TryGetValue(cacheKey, out ChatSessionGetByIdDto cachedResult))
                return cachedResult;



            var chatSession = await _context.ChatSessions
                  .AsNoTracking()
                  .Select(x=> ChatSessionGetByIdDto.MapFromChatSession(x))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            _memoryCache.Set(cacheKey, chatSession, _cacheOptions);


            return chatSession;
        }

        //public Task<ChatSessionGetByIdDto> Handle(ChatSessionGetByIdQuery request, CancellationToken cancellationToken)
        //{
        //   return _context.ChatSessions
        //        .AsNoTracking()
        //        .Select(x=> ChatSessionGetByIdDto.MapFromChatSession(x))
        //        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        //}
    }
}
