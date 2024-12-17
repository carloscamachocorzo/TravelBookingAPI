namespace TravelBooking.Application.Common
{
    /// <summary>
    /// A class representing the result of a request, including status, messages, and the actual result of type T.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the request.</typeparam>
    public sealed class RequestResult<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request was successful.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there was an error with the request.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the error message, if any.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the collection of messages (e.g., validation errors or informational messages).
        /// </summary>
        public IEnumerable<string>? Messages { get; set; }

        /// <summary>
        /// Gets or sets the actual result of the request.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult{T}"/> class.
        /// </summary>
        public RequestResult()
        {
            Messages = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResult{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="isSuccessful">Indicates whether the request was successful.</param>
        /// <param name="isError">Indicates whether there was an error with the request.</param>
        /// <param name="errorMessage">The error message, if any.</param>
        /// <param name="messages">A collection of messages (e.g., validation errors).</param>
        /// <param name="result">The actual result of the request.</param>
        private RequestResult(bool isSuccessful, bool isError, string? errorMessage, IEnumerable<string> messages, T result)
        {
            IsSuccessful = isSuccessful;
            IsError = isError;
            ErrorMessage = errorMessage;
            Messages = messages ?? new List<string>();
            Result = result;
        }

        /// <summary>
        /// Creates a successful result with the given result data and optional messages.
        /// </summary>
        /// <param name="result">The result of the request.</param>
        /// <param name="messages">Optional collection of messages related to the request.</param>
        /// <returns>A new instance of <see cref="RequestResult{T}"/> with success status.</returns>
        public static RequestResult<T> CreateSuccessful(T result, IEnumerable<string>? messages = null)
        {
            return new RequestResult<T>(isSuccessful: true, isError: false, errorMessage: null, messages: messages ?? new List<string>(), result: result);
        }

        /// <summary>
        /// Creates an unsuccessful result, indicating that the request did not succeed but there was no error.
        /// </summary>
        /// <param name="messages">A collection of messages explaining why the request was unsuccessful.</param>
        /// <returns>A new instance of <see cref="RequestResult{T}"/> with failure status and no error.</returns>
        public static RequestResult<T> CreateUnsuccessful(IEnumerable<string> messages)
        {
            return new RequestResult<T>(isSuccessful: false, isError: false, errorMessage: null, messages: messages, result: default(T));
        }

        /// <summary>
        /// Creates a result with an error message, indicating that an error occurred during the request.
        /// </summary>
        /// <param name="errorMessage">The error message describing what went wrong.</param>
        /// <returns>A new instance of <see cref="RequestResult{T}"/> with error status.</returns>
        public static RequestResult<T> CreateError(string errorMessage)
        {
            return new RequestResult<T>(isSuccessful: false, isError: true, errorMessage: errorMessage, messages: null, result: default(T));
        }
    }


}
