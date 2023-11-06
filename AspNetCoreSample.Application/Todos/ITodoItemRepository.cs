using AspNetCoreSample.Domain;

namespace AspNetCoreSample.Application;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetItems(CancellationToken cancellationToken);

    Task<TodoItem> AddItem(TodoItem item, CancellationToken cancellationToken);
}
