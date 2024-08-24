using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public sealed class ChatSessionGetByIdQueryValidator: AbstractValidator<ChatSessionGetByIdQuery>
    {
        private readonly  IApplicationDbContext _dbContext;

        public ChatSessionGetByIdQueryValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .MustAsync(BeValidIdAsync)
                .WithMessage("The selected Chat wasn't found.");
        }

        private Task<bool> BeValidIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbContext
                .ChatSessions
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
