using ASAPTask.Applications.Common.Dtos;
using ASAPTask.Applications.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Handlers
{
    public class Result : IResult
    {
        public Result()
        {
        }

        public IList<ResultMessageDto> Messages { get; set; } = new List<ResultMessageDto>();
        public int HttpCode { get; set; } = 500;
        public bool Succeeded { get; set; }

        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }

        public static IResult Fail(ResultMessageDto message)
        {
            return new Result
            {
                Succeeded = false,
                Messages = new List<ResultMessageDto> { message }
            };
        }

        public static Result Fail(IList<ResultMessageDto> messages)
        {
            return new Result
            {
                Succeeded = false,
                Messages = messages
            };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(ResultMessageDto message)
        {
            return Task.FromResult(Fail(message));
        }
        public static Task<Result> FailAsync(IList<ResultMessageDto> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }

        public static IResult Success(ResultMessageDto message)
        {
            return new Result
            {
                Succeeded = true,
                Messages = new List<ResultMessageDto> { message }
            };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(ResultMessageDto message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public Result()
        {
        }

        public T Data { get; set; }

        public static new Result<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }
     
        public static new Result<T> Fail(ResultMessageDto message)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = new List<ResultMessageDto> { message }
            };
        }

        public static new Result<T> Fail(IList<ResultMessageDto> messages)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = messages
            };
        }

        public static new Task<Result<T>> FailAsync()
        {
            var result = Fail();
            result.HttpCode = 500;
            return Task.FromResult(result);
        }
        public static new Task<Result<T>> FailAsync(ResultMessageDto message)
        {
            var result = Fail(message);
            result.HttpCode = 400;
            return Task.FromResult(result);
        }

        public static new Task<Result<T>> FailAsync(IList<ResultMessageDto> messages)
        {
            var result = Fail(messages);
            result.HttpCode = 400;
            return Task.FromResult(Fail(messages));
        }


        public static new Result<T> Success()
        {
            return new Result<T> { Succeeded = true };
        }

        public static new Result<T> Success(ResultMessageDto message)
        {
            return new Result<T>
            {
                Succeeded = true,
                Messages = new List<ResultMessageDto> { message }
            };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }
        public static Result<T> SuccessSearch(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }
        public static Result<T> Success(T data, ResultMessageDto message)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = new List<ResultMessageDto> { message } };
        }

        public static Result<T> Success(T data, List<ResultMessageDto> messages)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = messages };
        }

        public static new Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static new Task<Result<T>> SuccessAsync(ResultMessageDto message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }
        public static Task<Result<T>> SuccessSearchAsync(T data)
        {
            return Task.FromResult(SuccessSearch(data));

        }
        public static Task<Result<T>> SuccessAsync(T data, ResultMessageDto message)
        {
            return Task.FromResult(Success(data, message));
        }

        public static Task<Result<T>> SuccessWithCountAsync(T data)
        {
            return Task.FromResult(new Result<T> { Succeeded = true, Data = data });
        }
    }

}
