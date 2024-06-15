/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Fluent.Result;

public static class BindExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> input, Func< TIn, Result<TOut> > bind) =>
        input.IsSuccess
            ? bind(input.Value)
            : Result<TOut>.Failure(input.Error);

    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> inputAsync, Func< TIn, Result<TOut> > bind) =>
        Bind(await inputAsync, bind);

    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> input, Func< TIn, Task<Result<TOut>> >  bindAsync) =>
        input.IsSuccess
            ? await bindAsync(input.Value)
            : Result<TOut>.Failure(input.Error);

    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task< Result<TIn> > inputAsync, Func< TIn, Task<Result<TOut>> >  bindAsync) =>
        await Bind(await inputAsync, bindAsync);
}
