using ChatGPTClone.Application.Common.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Create
{
    public class ChatSessionCreateCommandValidator : AbstractValidator<ChatSessionCreateCommand>
    {
        public ChatSessionCreateCommandValidator(IStringLocalizer<CommonLocalization> localizer)
        {
            RuleFor(m => m.Model)
               .NotEmpty().WithMessage(m => localizer[CommonLocalizationKeys.ValidationIsRequiered, nameof(m.Model)])
               .IsInEnum().WithMessage(m => localizer[CommonLocalizationKeys.ValidationIsInvalid, nameof(m.Model)]);

            RuleFor(c => c.Content)
                .NotEmpty().WithMessage(c => localizer[CommonLocalizationKeys.ValidationIsRequiered, nameof(c.Content)])
                .Length(5, 4000).WithMessage(c => localizer[CommonLocalizationKeys.ValidationMustBeBetween, nameof(c.Content), 5, 4000]);

            // Old Using before Localizations
            //RuleFor(x=>x.Model)
            //    .NotEmpty().WithMessage("Model is required.")
            //    .IsInEnum().WithMessage("Model is invalid.");

            //RuleFor(x=>x.Content)
            //    .NotEmpty().WithMessage("Content is required.")
            //    .Length(5, 4000).WithMessage("Content must be between 5 and 4000 characters.");
        }
    }
}
