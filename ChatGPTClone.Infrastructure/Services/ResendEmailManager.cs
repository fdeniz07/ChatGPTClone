using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Email;
using Resend;

namespace ChatGPTClone.Infrastructure.Services
{
    public class ResendEmailManager : IEmailService
    {
        private readonly IResend _resend;

        public ResendEmailManager(IResend resend)
        {
            _resend = resend;
        }

        public async Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
