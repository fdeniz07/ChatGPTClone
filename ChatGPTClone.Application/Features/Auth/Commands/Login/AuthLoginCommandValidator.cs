using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Identity;
using FluentValidation;

namespace ChatGPTClone.Application.Features.Auth.Commands.Login
{
    public class AuthLoginCommandValidator : AbstractValidator<AuthLoginCommand>
    {
        private readonly IIdentityService _identityService;

        public AuthLoginCommandValidator(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public AuthLoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .Length(5, 100)
                .WithMessage("Email must be between 5 and 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Length(6, 50)
                .WithMessage("Password must be between 6 and 50 characters");

            RuleFor(x => x)
                .MustAsync(BeValidUser)
                .WithMessage("Invalid email or password");
        }

        private Task<bool> BeValidUser(AuthLoginCommand model, CancellationToken cancellationToken)
        {
            var request = new IdentityAuthenticateRequest(model.Email, model.Password);

            return _identityService.AuthenticateAsync(request, cancellationToken);
        }

    }
}
