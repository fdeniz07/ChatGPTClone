using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.General;
using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Create
{
    public sealed class ChatSessionCreateCommandHandler:IRequestHandler<ChatSessionCreateCommand, ResponseDto<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public ChatSessionCreateCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ResponseDto<Guid>> Handle(ChatSessionCreateCommand request, CancellationToken cancellationToken)
        {
           var chatSession = request.ToChatSession(_currentUserService.UserId);

           // Use IOpenAIService to send the message to the OpenAI API

           _dbContext.ChatSessions.Add(chatSession);

           await _dbContext.SaveChangesAsync(cancellationToken);

           var chatMessages = chatSession.Threads.SelectMany(t => t.Messages).ToList();

           return new ResponseDto<Guid>(chatSession.Id, "A new chat session was created successfully.");
        }

    }
}
