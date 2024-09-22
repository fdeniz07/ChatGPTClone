using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Email;
using Resend;
using System.Web;

namespace ChatGPTClone.Infrastructure.Services
{
    public class ResendEmailManager : IEmailService
    {
        private readonly IResend _resend;

        private static string? _emailTemplate;

        public ResendEmailManager(IResend resend, IEnvironmentService environmentService)
        {
            _resend = resend;

            if (string.IsNullOrEmpty(_emailTemplate))
                _emailTemplate = File.ReadAllText(Path.Combine(environmentService.WebRootPath, "email-templates", "EmailVerification.html")); //Path.Combine, bulunan isletim sistemine göre path'leri dogru sekilde verir.

            /* NOT:
                File.ReadAllText,File.WriteAllText gibi islemlerde dikkatli olmak gerekir.
                Multithread uygulamalarda islemleri keseceginde diger threadlerin islemlerinde hata firlatir.
                Bu yüzden stream benzeri yapilar kullanilir. Biz ise burada singleton yapida bir defa olusturdugumuz icin sorun teskil etmez.
            */
        }

        public Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken)
        {
            string html = _emailTemplate;

            var emailTitle = "E-Posta Dogrulama Islemi - ChatGPTClone";

            html = html.Replace("{{title}}", emailTitle);

            html = html.Replace("{{message}}", "E-Posta dogrulama islemlerinizi tamamlamak icin asagidaki linke tiklayiniz.");

            html = html.Replace("{{verifyButtonText}}", "E-Posta Dogrulama");


            var token = HttpUtility.UrlEncode(emailVerificationDto.Token);

            var emailVerificationUrl = $"www.google.com.tr/verify-email?email={emailVerificationDto.Email}&token={token}";

            html = html.Replace("{{verifyButtonlink}}", emailVerificationUrl);


            var message = new EmailMessage();
            message.From = "noreply@yazilimacademy.com";
            message.To.Add(emailVerificationDto.Email);
            message.Subject = emailTitle;
            message.HtmlBody = html;

            return _resend.EmailSendAsync(message);
        }
    }
}
