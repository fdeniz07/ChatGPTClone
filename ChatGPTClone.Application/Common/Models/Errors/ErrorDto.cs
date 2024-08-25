namespace ChatGPTClone.Application.Common.Models.Errors
{
    public sealed class ErrorDto
    {
        // Email
        public string PropertyName { get; set; }


        // The email address is not valid.
        // The email address is required.
        // The email address must be at most 100 characters long.
        public IReadOnlyList<string> Messages { get; set; }


        public ErrorDto(string propertyName, List<string> messages)
        {
            PropertyName = propertyName;
            Messages = messages;
        }
    }
}
