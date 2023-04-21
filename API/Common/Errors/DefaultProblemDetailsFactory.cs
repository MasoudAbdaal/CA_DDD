using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using FluentResults;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc;

public class DefaultProblemDetailsFactory : ProblemDetailsFactory
{
    private class MyCustomErrorProperty : ProblemDetails
    {
        public IError? ValidationErrors { get; set; }
        public string MASAAGE { get; set; } = string.Empty;
    }



    private readonly ILogger<DefaultProblemDetailsFactory> _logger;
    private readonly IDictionary<int, string> _defaultTitles = new Dictionary<int, string>()
        {
            {400, "Bad Request"},
            {401, "Unauthorized"},
            {402, "Payment Required"},
            {403, "Forbidden"},
            {404, "Not Found"},
            {405, "Method Not Allowed"},
            {406, "Not Acceptable"},
            {407, "Proxy Authentication Required"},
            {408, "Request Timeout"},
            {409, "Conflict"},
            {410, "Gone"},
            {411, "Length Required"},
            {412, "Precondition Failed"},
            {413, "Payload Too Large"},
            {414, "URI Too Long"},
            {415, "Unsupported Media Type"},
            {416, "Range Not Satisfiable"},
            {417, "Expectation Failed"},
            {422, "Unprocessable Entity"},
            {423, "Locked"},
            {424, "Failed Dependency"},
            {425, "Too Early"},
            {426, "Upgrade Required"},
            {428, "Precondition Required"},
            {429, "Too Many Requests"},
            {431, "Request Header Fields Too Large"},
            {451, "Unavailable For Legal Reasons"},

            {500, "Internal Server Error"},
            {501, "Not Implemented"},
            {502, "Bad Gateway"},
            {503, "Service Unavailable"},
            {504, "Gateway Timeout"},
            {505, "HTTP Version Not Supported"},
            {506, "Variant Also Negotiates"},
            {507, "Insufficient Storage"},
            {508, "Loop Detected"},
            {509, "Bandwidth Limit Exceeded"},
            {510, "Not Extended"},
            {511, "Network Authentication Required"}
        };

    public DefaultProblemDetailsFactory(ILogger<DefaultProblemDetailsFactory> logger)
    {
        _logger = logger;
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null
        )
    {
        var problemDetails = new MyCustomErrorProperty
        {
            MASAAGE = "MASSAGEmeee",
            // Title = GetDefaultTitle(statusCode),
            Title = title,
            Status = statusCode,
            Detail = detail,
            Instance = instance
        };

        if (httpContext == null || httpContext.Request == null || httpContext.Request.Headers == null)
        {
            _logger?.LogWarning(JsonSerializer.Serialize(problemDetails));
            return problemDetails;
        }

        problemDetails.Type = $"https://httpstatuses.com/{statusCode ?? 500}";
        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        if (httpContext.Request.Headers.TryGetValue("Accept", out var accept) &&
            accept.Contains("application/json"))
        {
            problemDetails.Extensions["errors"] = GetErrors(httpContext);
        }

        return problemDetails;
    }


    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Title = GetDefaultTitle(statusCode),
            Status = statusCode,
            Detail = detail,
            Instance = instance
        };

        if (httpContext == null || httpContext.Request == null || httpContext.Request.Headers == null)
        {
            _logger?.LogWarning(JsonSerializer.Serialize(problemDetails));
            return problemDetails;
        }

        problemDetails.Type = $"https://httpstatuses.com/{statusCode ?? 400}";
        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        if (httpContext.Request.Headers.TryGetValue("Accept", out var accept) &&
            accept.Contains("application/json"))
        {
            problemDetails.Extensions["modelState"] = modelStateDictionary;

            problemDetails.Extensions["errors"] = GetErrors(httpContext);
        }

        return problemDetails;
    }


    private string GetDefaultTitle(int? statusCode)
    {
        if (_defaultTitles.TryGetValue(statusCode ?? 500, out var title))
        {
            return title;
        }

        throw new ArgumentOutOfRangeException(
            nameof(statusCode),
            statusCode,
            $"No default title exists for status code {statusCode}.");
    }

    private object GetErrors(HttpContext httpContext)
    {
        var modelStateDictionary = httpContext.Features.Get<ValidationProblemDetails>()!
            .Extensions["modelState"] as ModelStateDictionary;
        var errors = modelStateDictionary!.Keys
            .SelectMany(key =>
                modelStateDictionary[key]!.Errors.Select(x => new ValidationError
                {
                    FieldName = key,
                    Message = x.ErrorMessage
                }));

        return new { errors };
    }

    private class ValidationError
    {
        public string FieldName { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
