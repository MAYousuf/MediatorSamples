using MediatR;

namespace AspNetCoreSample.Application;

public sealed class MessageValidatorBehaviour<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IValidate
{
    public Task<TResponse> Handle(TMessage request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    //protected override Task Handle(TMessage message, CancellationToken cancellationToken)
    //{
    //    if (!message.IsValid(out var validationError))
    //        throw new ValidationException(validationError);

    //    return default;
    //}
}