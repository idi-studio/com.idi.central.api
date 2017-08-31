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

        [JsonProperty("code")]
        public string Code { get; set; } = "200";

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("details")]
        public Dictionary<string, object> Details { get; set; } = new Dictionary<string, object>();

        public Result()
        {
            this.Details = new Dictionary<string, object>();
        }

        public static Result Success(string message)
        {
            return new Result { Status = ResultStatus.Success, Code = "200", Message = message };
        }

        public static Result<T> Success<T>(T data, string message = "")
        {
            return new Result<T> { Data = data, Status = ResultStatus.Success, Code = "200", Message = message };
        }

        public static Result Fail(string message, string code = "400")
        {
            return new Result { Status = ResultStatus.Fail, Message = message, Code = code };
        }

        public static Result<T> Fail<T>(string message, string code = "400")
        {
            return new Result<T> { Status = ResultStatus.Fail, Message = message, Code = code };
        }

        public static Result Error(string message, string code = "500")
        {
            return new Result { Status = ResultStatus.Error, Message = message, Code = code };
        }

        public static Result Error(Exception exception, string message = null)
        {
            var result = new Result { Status = ResultStatus.Error, Message = message ?? exception.Message, Code = "500" };

            result.Details.Add("stacktrace", exception.StackTrace);

            return result;
        }

        public static Result<T> Error<T>(string message, List<string> errors = null)
        {
            var result = new Result<T> { Status = ResultStatus.Error, Message = message, Code = "500" };

            result.Details.Add("errors", errors ?? new List<string>());

            return result;
        }

        public static Result<T> Error<T>(Exception exception)
        {
            var result = new Result<T> { Status = ResultStatus.Error, Code = "500", Message = exception.Message };

            result.Details.Add("stacktrace", exception.StackTrace);

            return result;
        }

        public Result Attach(string key, object value)
        {
            this.Details.Add(key, value);

            return this;
        }
    }

    public class Result<T> : Result
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        public new Result<T> Attach(string key, object value)
        {
            this.Details.Add(key, value);

            return this;
        }
    }
}
