using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IDI.Core.Common
{
    public class Result
    {
        [JsonProperty("status")]
        public ResultStatus Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("details")]
        public Dictionary<string, object> Details { get; set; }

        public Result()
        {
            this.Details = new Dictionary<string, object>();
        }

        public static Result Success(string message, List<string> details = null)
        {
            return new Result { Status = ResultStatus.Success, Message = message, Details = new Dictionary<string, object> { { "info", details ?? new List<string>() } } };
        }

        public static Result<T> Success<T>(T data, string message = "", List<string> details = null)
        {
            return new Result<T> { Data = data, Status = ResultStatus.Success, Message = message, Details = new Dictionary<string, object> { { "info", details ?? new List<string>() } } };
        }

        public static Result Fail(string message, List<string> details = null)
        {
            return new Result { Status = ResultStatus.Fail, Message = message, Details = new Dictionary<string, object> { { "info", details ?? new List<string>() } } };
        }

        public static Result<T> Fail<T>(string message, List<string> details = null)
        {
            return new Result<T> { Status = ResultStatus.Fail, Message = message, Details = new Dictionary<string, object> { { "info", details ?? new List<string>() } } };
        }

        public static Result Error(string message, List<string> details = null)
        {
            return new Result { Status = ResultStatus.Error, Message = message, Details = new Dictionary<string, object> { { "info", details ?? new List<string>() } } };
        }

        public static Result Error(Exception exception)
        {
            return new Result
            {
                Status = ResultStatus.Error,
                Message = exception.Message,
                Details = new Dictionary<string, object> { { "StackTrace", exception.StackTrace } }
            };
        }

        public static Result<T> Error<T>(string message, List<string> details = null)
        {
            return new Result<T> { Status = ResultStatus.Error, Message = message, Details = new Dictionary<string, object> { { "info", details ?? new List<string>() } } };
        }

        public static Result<T> Error<T>(Exception exception)
        {
            return new Result<T>
            {
                Status = ResultStatus.Error,
                Message = exception.Message,
                Details = new Dictionary<string, object> { { "StackTrace", exception.StackTrace } }
            };
        }
    }

    public class Result<T> : Result
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
