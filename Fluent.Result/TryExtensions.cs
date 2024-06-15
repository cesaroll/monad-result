/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;

namespace Fluent.Result;

public static class ResultExtensions
{
    public static Result<TOut> Try<TIn, TOut>(
        this Result<TIn> input,
        Func<TIn, TOut> func,
        Func<TIn, Exception, Error> handle)
    {
        try {
            return input.IsSuccess
                ? Result<TOut>.Success(func(input.Value))
                : Result<TOut>.Failure(input.Error);
        } catch (Exception ex) {
            return Result<TOut>.Failure(handle(input.Value, ex));
        }
    }

    public static async Task< Result<TOut> > Try<TIn, TOut>(
        this Task< Result<TIn> > input,
        Func<TIn, TOut> func,
        Func<TIn, Exception, Error> handle) =>
        Try(await input, func, handle);

    public static async Task<Result<TOut>> Try<TIn, TOut>(
        this Result<TIn> input,
        Func<TIn, Task<TOut>> funcAsync,
        Func<TIn, Exception, Error> handle)
    {
        try {
            return input.IsSuccess
                ? Result<TOut>.Success(await funcAsync(input.Value))
                : Result<TOut>.Failure(input.Error);
        } catch (Exception ex) {
            return Result<TOut>.Failure(handle(input.Value, ex));
        }
    }

    public static async Task<Result<TOut>> Try<TIn, TOut>(
        this Task< Result<TIn> > input,
        Func<TIn, Task<TOut>> funcAsync,
        Func<TIn, Exception, Error> handle) =>
        await Try(await input, funcAsync, handle);
}
