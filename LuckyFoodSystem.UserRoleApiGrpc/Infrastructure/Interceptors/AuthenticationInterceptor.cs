using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckyFoodSystem.UserRoleManagementService.Infrastructure.Interceptors
{
    public class AuthenticationInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>
            (TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
             AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var headers = context.Options.Headers ?? new Metadata();

            if (!headers.Any(h => h.Key == "authorization"))
            {
                throw new RpcException(new Status(StatusCode.Cancelled, "Missing authorization header."));
            }

            var options = context.Options.WithHeaders(headers);
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                           context.Method,
                           context.Host,
                           options);

            return continuation(request, newContext);
        }
    }
}
