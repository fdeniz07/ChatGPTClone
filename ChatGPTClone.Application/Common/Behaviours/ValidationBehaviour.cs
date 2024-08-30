using FluentValidation;
using MediatR;

namespace ChatGPTClone.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // Doğrulayıcıları dependency injection yoluyla al
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Doğrulayıcı yoksa sonraki işleme geç
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request); //from FluentValidation

            // Tüm doğrulayıcıları paralel olarak çalıştır
            var validationTasks = _validators.Select(v => v.ValidateAsync(context, cancellationToken));

            var validationResults = await Task.WhenAll(validationTasks);

            // WhenAll : Birden fazla islemi asenkron olarak bekler ve tamamlandiginda devam eder.

            // Hataları topla
            var failures = validationResults
                .Where(f => f.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            // Hata varsa exception fırlat
            if (failures.Any())
                throw new ValidationException(failures);

            // Error Property: Email
            // Error Message: Email must not be empty.
            // Error Code: 400
            // Error Source: ModelState
            // Error Type: Validation
            // Error Details: The Email field is required.
            // Error Instance: /api/v1/chat-sessions

            // Hata yoksa sonraki işleme geç
            return await next();
        }
    }
}
