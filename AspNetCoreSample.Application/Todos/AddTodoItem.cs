using AspNetCoreSample.Domain;
using FluentValidation;

using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreSample.Application;

public class AddTodoItemValidator : AbstractValidator<AddTodoItemCommand>
{
    public AddTodoItemValidator()
    {
        RuleFor(x => x.Title).Length(1, 40);
        RuleFor(x => x.Text).Length(1, 150);
    }
}

public sealed record AddTodoItemCommand(string Title, string Text) : IRequest<Result<TodoItem>>, IValidate
{
    //public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    //{
    //    var validator = new AddTodoItemValidator();
    //    var result = validator.Validate(this);
    //    if (result.IsValid)
    //        error = null;
    //    else
    //        error = new ValidationError(result.Errors.Select(e => e.ErrorMessage).ToArray());

    //    return result.IsValid;
    //}
}

public sealed class TodoItemCommandHandler : IRequestHandler<AddTodoItemCommand, Result<TodoItem>>
{
    private readonly ITodoItemRepository _repository;

    public TodoItemCommandHandler(ITodoItemRepository repository) => _repository = repository;

    public async Task<Result<TodoItem>> Handle(AddTodoItemCommand command, CancellationToken cancellationToken)
    {
        var item = new TodoItem(Guid.NewGuid(), command.Title, command.Text, false);

         return Result<TodoItem>.Success(await _repository.AddItem(item, cancellationToken));

    }
}
