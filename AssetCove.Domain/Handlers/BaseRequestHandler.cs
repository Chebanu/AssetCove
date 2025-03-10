﻿using MediatR;

using Microsoft.Extensions.Logging;

namespace AssetCove.Domain.Handlers;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<BaseRequestHandler<TRequest, TResponse>> _logger;

    public BaseRequestHandler(ILogger<BaseRequestHandler<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling {@Name}. Request: {@Request}", GetType().Name, request);
            TResponse result = await HandleInternal(request, cancellationToken);
            _logger.LogInformation("Handled {@Name}. Request: {@Request}. Result: {@Result}", GetType().Name, request, result);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error handling {@Name}. Request: {@Request}", GetType().Name, request);
            throw;
        }
    }

    protected abstract Task<TResponse> HandleInternal(TRequest request, CancellationToken cancellationToken);
}