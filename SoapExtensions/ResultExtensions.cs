namespace SoapExtensions;

/// <summary>
/// Represents an abstract record for an error with a message.
/// </summary>
/// <param name="Message">The error message.</param>
public abstract record Error(string Message);

/// <summary>
/// Represents a result of an operation, containing either a value or an error.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
/// <typeparam name="TE">The type of the error, derived from <see cref="Error"/>.</typeparam>
public readonly record struct Result<T, TE>(T? Value, Error? Error) where TE : Error
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess => this is { Value: not null, Error: null };

    /// <summary>
    /// Implicitly converts a value to a successful <see cref="Result{T, TE}"/>.
    /// </summary>
    /// <param name="val">The value to convert.</param>
    /// <returns>A successful result containing the value.</returns>
    public static implicit operator Result<T, TE>(T val)
    {
        return new Result<T, TE>(val, null);
    }

    /// <summary>
    /// Implicitly converts an error to a failed <see cref="Result{T, TE}"/>.
    /// </summary>
    /// <param name="standardError">The error to convert.</param>
    /// <returns>A failed result containing the error.</returns>
    public static implicit operator Result<T, TE>(Error standardError)
    {
        return new Result<T, TE>(default, standardError);
    }
}

/// <summary>
/// Represents a standard error with a message and an optional exception.
/// </summary>
/// <param name="Message">The error message.</param>
/// <param name="Exception">The associated exception, if any.</param>
public record StandardError(string Message, Exception? Exception = null) : Error(Message);