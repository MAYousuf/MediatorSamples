using AspNetCoreSample.Application;
using AspNetCoreSample.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Riok.Mapperly.Abstractions;

namespace AspNetCoreSample.Api;

public static class TodoApi
{
    public static WebApplication MapTodoApi(this WebApplication app)
    {
        app.MapGet(
                "/api/todos",
                async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var todos = await mediator.Send(new GetTodoItems(), cancellationToken);
                    return Results.Ok(todos.Select(t => TodoItemMapper.MapTodoItem(t)));
                }
            )
            .WithName("Get todos");

        app.MapPost(
                "/api/todos",
                async ([FromBody] AddTodoItemDto todo, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var addTodo = TodoItemMapper.MapAddTodoItem(todo);
                        var result = await mediator.Send<Result<TodoItem,ValidationFailed>>(addTodo, cancellationToken);
                        //return Results.Ok(null);
                        //return Results.Ok(TodoItemMapper.MapTodoItem(result));
                        //return Results.Ok(TodoItemMapper.MapTodoItem(result.Value));
                        return result.Match<IResult>(
                            success => Results.Ok(success),
                            failed => Results.BadRequest(failed.errors));
                    }
                    catch (ValidationException ex)
                    {
                        return Results.BadRequest(ex.ValidationError);
                    }
                }
            )
            .WithName("Add todo");

        return app;
    }
}

public sealed record TodoItemDto(string Title, string Text, bool Done);

public sealed record AddTodoItemDto(string Title, string Text);

[Mapper]
public static partial class TodoItemMapper
{
    public static partial TodoItemDto MapTodoItem(this TodoItem todo);

    public static partial AddTodoItemCommand MapAddTodoItem(this AddTodoItemDto todo);
}
