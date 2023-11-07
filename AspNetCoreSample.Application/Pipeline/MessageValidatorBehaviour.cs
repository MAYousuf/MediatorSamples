using AspNetCoreSample.Domain;
using FluentValidation;
using MediatR;

namespace AspNetCoreSample.Application;

//public sealed class MessageValidatorBehaviour<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
//    where TMessage : IValidate
//{
//    public Task<TResponse> Handle(TMessage request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    //protected override Task Handle(TMessage message, CancellationToken cancellationToken)
//    //{
//    //    if (!message.IsValid(out var validationError))
//    //        throw new ValidationException(validationError);

//    //    return default;
//    //}
//}

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IValidate // Constrained to IMessage, or constrain to IBaseCommand or any custom interface you've implemented
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehaviour(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var res = new ValidationFailed(validationResult.Errors);
            //return res;
            return Result<TResponse>.Failure(validationResult.Errors.Select(x=>x.ErrorMessage).ToArray());
        }

        return await next();
    }
}

