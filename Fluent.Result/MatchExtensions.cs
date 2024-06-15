/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Fluent.Result;

public static class MatchExtensions
{
    public static TOut Match<TIn, TOut>(
        this Result<TIn> input,
        Func<TIn, TOut> success,
        Func<Error, TOut> failure) =>
        input.IsSuccess
            ? success(input.Value)
            : failure(input.Error);

    public static async Task<TOut> Match<TIn, TOut>(
        this Task< Result<TIn> > input,
        Func<TIn, TOut> success,
        Func<Error, TOut> failure) =>
        Match(await input, success, failure);

    public static async Task<TOut> Match<TIn, TOut>(
        this Result<TIn> input,
        Func<TIn, Task<TOut>> success,
        Func<Error, Task<TOut>> failure) =>
        input.IsSuccess
            ? await success(input.Value)
            : await failure(input.Error);

    public static async Task<TOut> Match<TIn, TOut>(
        this Task< Result<TIn> > input,
        Func<TIn, Task<TOut>> success,
        Func<Error, Task<TOut>> failure) =>
        await Match(await input, success, failure);
}
