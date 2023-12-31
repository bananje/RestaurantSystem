﻿using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace LuckyFoodSystem.Application.Common.Errors
{
    public class LuckyFoodProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        public LuckyFoodProblemDetailsFactory(IOptions<ApiBehaviorOptions> options) 
            => _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, 
                                                            int? statusCode = null, 
                                                            string? title = null, 
                                                            string? type = null, 
                                                            string? detail = null,
                                                            string? instance = null)
        {
            statusCode ??= 500;
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            var errors = httpContext?.Items["errors"] as List<Error>;
            if (errors is not null)
            {
                problemDetails.Extensions.Add("errors", errors.Select(u => u.Code));
            }
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            return new ValidationProblemDetails
            {
                Title = title ?? "One or more validation errors occurred.",
                Status = statusCode ?? StatusCodes.Status400BadRequest,
                Detail = detail ?? "Please refer to the errors property for additional details.",
                Instance = instance ?? httpContext.Request.Path
            };
        }
    }
}
