using ASAPTask.Applications.Common.Dtos;
using ASAPTask.Applications.Common.Exceptions;
using ASAPTask.Applications.Common.Handlers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;


namespace ASAPTask.API.Middlewares
{
    public class GeneralResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GeneralResponseMiddleware> _logger;
        public GeneralResponseMiddleware(RequestDelegate next, ILogger<GeneralResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            var originBody = response.Body;
            using var newBody = new MemoryStream();
            response.Body = newBody;

            try
            {
                await _next(context);

                await ModifyResponseAsync(response);

                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originBody);
                response.Body = originBody;
            }
            catch (BusinessException ex)
            {
                 _logger.LogError(JsonConvert.SerializeObject(ex));

                await HandleExceptionAsync(context, response, ex.Keys, HttpStatusCode.OK, originBody, newBody, _logger);
            }
            catch (Applications.Common.Exceptions.ValidationException ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                await HandleExceptionAsync(context, response, ex.Keys, HttpStatusCode.BadRequest, originBody, newBody, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                // Add default exception key

                await HandleExceptionAsync(context, response, new List<string> { JsonConvert.SerializeObject(ex) }, HttpStatusCode.InternalServerError, originBody, newBody, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpResponse response, IList<string> exceptionKeys, HttpStatusCode httpStatus, Stream originBody, MemoryStream newBody, ILogger<GeneralResponseMiddleware> logger)
        {

            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatus;

            await ModifyResponseAsync(response, exceptionKeys);

            newBody.Seek(0, SeekOrigin.Begin);
            await newBody.CopyToAsync(originBody);
            response.Body = originBody;
        }

        private async Task ModifyResponseAsync(HttpResponse response, IList<string> exceptionKeys = null)
        {
            var stream = response.Body;

            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream, leaveOpen: true);
            string originalResponse = await reader.ReadToEndAsync();

            Result<object> responseData = null;

            if (response.StatusCode == (int)HttpStatusCode.OK && exceptionKeys == null)
            {
                responseData = await Result<object>.SuccessAsync(JsonConvert.DeserializeObject(originalResponse));
                responseData.HttpCode = (int)HttpStatusCode.OK;
            }
            else if (response.StatusCode == (int)HttpStatusCode.OK && exceptionKeys != null)
            {
                responseData = await Result<object>.FailAsync(new ResultMessageDto() { ErrorCode = 400, Message = string.Join(Environment.NewLine, exceptionKeys) });
                responseData.HttpCode = (int)HttpStatusCode.BadRequest;
            }
            else if (response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                var res = JsonConvert.DeserializeObject<dynamic>(originalResponse);

                var errors = new List<ResultMessageDto>();

                foreach (var item in res.errors)
                {
                    errors.Add(new ResultMessageDto { Message = item.ToString() });
                }

                responseData = await Result<object>.FailAsync(errors);
                responseData.HttpCode = (int)HttpStatusCode.BadRequest;

            }
            else if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                responseData = await Result<object>.FailAsync(new ResultMessageDto()
                {
                    Message = string.Join(Environment.NewLine, exceptionKeys),
                    ErrorCode = response.StatusCode,            
                });
                responseData.HttpCode = (int)HttpStatusCode.InternalServerError;
            }

            var modifiedResponse = JsonConvert.SerializeObject(responseData, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            stream.SetLength(0);
            using var writer = new StreamWriter(stream, leaveOpen: true);
            await writer.WriteAsync(modifiedResponse);
            await writer.FlushAsync();

            response.ContentLength = stream.Length;
        }

    }
}
