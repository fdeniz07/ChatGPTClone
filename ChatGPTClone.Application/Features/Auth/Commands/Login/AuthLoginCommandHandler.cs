using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.General;
using ChatGPTClone.Application.Common.Models.Identity;
using MediatR;

namespace ChatGPTClone.Application.Features.Auth.Commands.Login
{
    public class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, ResponseDto<AuthLoginDto>>
    {
        private readonly IIdentityService _identityService;

        public AuthLoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ResponseDto<AuthLoginDto>> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.LoginAsync(new IdentityLoginRequest(request.Email, request.Password), cancellationToken);

            return new ResponseDto<AuthLoginDto>(AuthLoginDto.FromIdentityLoginResponse(response));
        }
    }
}
