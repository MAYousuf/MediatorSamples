using AspNetCoreSample.Domain;
using MediatR;

namespace AspNetCoreSample.Application;

public sealed record GetTodoItems() : IRequest<IEnumerable<TodoItem>>;

public sealed class TodoItemQueryHandler : IRequestHandler<GetTodoItems, IEnumerable<TodoItem>>
{
    private readonly ITodoItemRepository _repository;

    public TodoItemQueryHandler(ITodoItemRepository repository) => _repository = repository;

    public Task<IEnumerable<TodoItem>> Handle(GetTodoItems query, CancellationToken cancellationToken) =>
        _repository.GetItems(cancellationToken);
}
