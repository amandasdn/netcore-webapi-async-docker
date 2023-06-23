using System.Text.Json.Serialization;

namespace Blog.Domain.Models
{
    public abstract class AResult
    {
        [JsonPropertyOrder(1)]
        public DateTimeOffset ExecutionDate
        {
            get => TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
        }

        [JsonPropertyOrder(2)]
        public bool Success { get; private set; } = true;

        [JsonPropertyOrder(4), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; private set; }

        public void AddErrorMessage(string error)
        {
            InitError();
            Errors?.Add(error);
        }

        public void AddErrorMessage(List<string> errors)
        {
            InitError();
            Errors?.AddRange(errors);
        }

        private void InitError()
        {
            if (Errors == null)
            {
                Success = false;
                Errors = new List<string>();
            }
        }
    }

    public class Result : AResult
    {
        public Result() { }
    }

    public class Result<T> : AResult
    {
        public Result(T content)
        {
            Content = content;
        }

        [JsonPropertyOrder(3)]
        public T Content { get; private set; }
    }
}
