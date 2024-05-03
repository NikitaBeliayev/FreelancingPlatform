using FreelancingPlatform.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FreelancingPlatform.Middleware
{
    public class GlobalWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalWrapperMiddleware> _logger;

        public GlobalWrapperMiddleware(RequestDelegate next, ILogger<GlobalWrapperMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();

            try
            {
                context.Response.Body = responseBody;
                await _next(context);

                context.Response.Body = originalBodyStream;

                var isJsonResponse = context.Response.ContentType?.Contains("application/json");

                if (context.Request.Path.ToString().Contains("swagger"))
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
                else if (isJsonResponse.GetValueOrDefault())
                {
                    var bodyAsObject = await ReadResponseStream(responseBody);
                    var requestUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.Path.Value}";
                    var finalResponse = WrapResponse(context.Response, bodyAsObject, requestUrl);

                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = finalResponse.StatusCode;
                    await context.Response.WriteAsJsonAsync(finalResponse);
                }
                else
                {
                    var finalResponse = WrapResponse(context.Response);

                    context.Response.StatusCode = finalResponse.StatusCode;
                    await context.Response.WriteAsJsonAsync(finalResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = ApiResponse.Failure(
                        "An internal server error occurred.",
                            new() { "Internal Server Error" });

                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private ApiResponse<object> WrapResponse(HttpResponse response, object? body = null, string? requsetUrl = null)
        {
            if (body is not null)
            {
                var jsonObject = JsonNode.Parse(body.ToString())!.AsObject();
       
                var data = jsonObject["value"] != null ? JsonNode.Parse(jsonObject["value"].DeepClone().ToJsonString()).AsObject() : null;

                if (data != null && data.TryGetPropertyValue("next", out var _next))
                {
                    if (data["next"] != null)
                    {
                        data["next"].ReplaceWith($"{requsetUrl}{data["next"].GetValue<string>()}");
                    }
                    if (data["previous"] != null) 
                    {
                        data["previous"].ReplaceWith($"{requsetUrl}{data["previous"].GetValue<string>()}");
                    }
                }

                bool isSuccess = jsonObject["isSuccess"].GetValue<bool>();
                var errorInfo = jsonObject["error"].DeepClone();
                Shared.Error error = new Shared.Error(errorInfo["code"].GetValue<string>(), errorInfo["message"].GetValue<string>(), errorInfo["statusCode"].GetValue<Int32>());

                return ApiResponse<object>.FromResponseData(data, isSuccess, error, response.StatusCode);
            }
            return new ApiResponse<object>(null, response.StatusCode == StatusCodes.Status200OK ? true : false, "", null, response.StatusCode);
        }

        private async Task<object> ReadResponseStream(MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            object? bodyAsObject = await JsonSerializer.DeserializeAsync<object>(responseBody);
            responseBody.Seek(0, SeekOrigin.Begin);
            return bodyAsObject ?? new object();
        }
    }

    public static class GlobalWrapperMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalWrapperMiddleware>();
        }
    }
}