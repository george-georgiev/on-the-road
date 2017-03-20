using OnTheRoad.Infrastructure.Enums;

namespace OnTheRoad.Infrastructure.Json
{
    public class Result
    {
        public Result(string displayMessage, ResponseStatus status)
        {
            this.DisplayMessage = displayMessage;
            this.Status = status;
        }

        public string DisplayMessage { get; }

        public ResponseStatus Status { get; }
    }
}
