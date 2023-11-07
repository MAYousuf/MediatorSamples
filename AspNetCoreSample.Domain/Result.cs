using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreSample.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public string[] Errors { get; private set; }

        private Result() { }

        public static Result<T> Success(T value) => new Result<T>() { IsSuccess = true, Value = value };
        public static Result<T> Failure(params string[] errors) => new Result<T>() { IsSuccess = false, Errors = errors };
    }

    //public readonly struct Result<TValue, TError>
    //{
    //    public bool IsError { get; }
    //    public bool IsSuccess => !IsError;

    //    public readonly TValue? Value => _value;

    //    private readonly TValue? _value;
    //    private readonly TError? _error;

    //    private Result(TValue value)
    //    {
    //        IsError = false;
    //        _value = value;
    //        _error = default;
    //    }

    //    private Result(TError error)
    //    {
    //        IsError = true;
    //        _error = error;
    //        _value = default;
    //    }

    //    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    //    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    //    public TResult Match<TResult>(
    //            Func<TValue, TResult> success,
    //            Func<TError, TResult> failure) =>
    //        !IsError ? success(_value!) : failure(_error!);

    //}
}
