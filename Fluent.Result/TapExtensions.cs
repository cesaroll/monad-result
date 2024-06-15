/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Fluent.Result;

public static class TapExtensions
{
    public static Result<TIn> Tap<TIn>(this Result<TIn> input, Action<TIn> action)
    {
        if(input.IsSuccess)
            action(input.Value);
        return input;
    }

    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> inputTask, Action<TIn> action)
    {
        var input = await inputTask;
        return Tap(input, action);
    }

    public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> input, Func<TIn, Task> funcAsync)
    {
        if(input.IsSuccess)
            await funcAsync(input.Value);
        return input;
    }

    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> inputTask, Func<TIn, Task> funcAsync)
    {
        var input = await inputTask;
        return await Tap(input, funcAsync);
    }
}
