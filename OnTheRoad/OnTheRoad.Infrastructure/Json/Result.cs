using OnTheRoad.Infrastructure.Enums;

namespace OnTheRoad.Infrastructure.Json
{
    public class Result
    {
        public Result(string displayMessage, ResponseStatus status, string content = null)
        {
            this.DisplayMessage = displayMessage;
            this.Status = status;
            this.Content = content;
        }

        public string DisplayMessage { get; }

        public ResponseStatus Status { get; }

        public string Content { get; set; }
    }
}
