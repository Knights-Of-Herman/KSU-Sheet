using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Types
{
    /// <summary>
    /// Class that represents the results of an operation with an error message if failed
    /// </summary>
    public class TryResult
    {
        /// <summary>
        /// Whether the Get was successful
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Error Message Associated with a failure
        /// </summary>
        public string ErrorMessage { get; set; } = "";


        public TryResult()
        {

        }

        /// <summary>
        /// Creates a successful TryResult
        /// </summary>
        /// <returns></returns>
        public static TryResult Success()
        {
            var result = new TryResult();
            result.IsSuccess = true;
            result.ErrorMessage = string.Empty;
            return result;
        }
        
        /// <summary>
        /// Creates a Failed TryResult with an unknown error
        /// </summary>
        /// <returns></returns>
        public static TryResult Fail()
        {
            var result = new TryResult();
            result.IsSuccess = false;
            result.ErrorMessage = "Unknown Error";
            return result;
        }

        /// <summary>
        /// Creates a Failed TryResult with a known error
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <returns></returns>
        public static TryResult Fail(string message)
        {
            var result = new TryResult();
            result.IsSuccess = false;
            result.ErrorMessage = message;
            return result;
        }
    }


    /// <summary>
    /// Type used for Async methods that can fail
    /// </summary>
    /// <typeparam name="T">Type of the value to get</typeparam>
    public class TryResult<T> : TryResult
    {
        /// <summary>
        /// Object fetched, default if unsuccessful
        /// </summary>
        public T? Value { get; set; }

        public TryResult() { }

        /// <summary>
        /// Creates a successful TryResult
        /// </summary>
        /// <param name="value">Value associated with the TryGet</param>
        /// <returns>A Successful TryGetResult</returns>
        public static TryResult<T> Success(T value)
        {
            var result = new TryResult<T>();
            result.Value = value;
            result.IsSuccess = true;
            return result;
        }

        /// <summary>
        /// Creates a failed TryResult
        /// </summary>
        public new static TryResult<T> Fail()
        {
            var result = new TryResult<T>();
            result.Value = default;
            result.IsSuccess = false;
            result.ErrorMessage = "Unknown Error";
            return result;
        }


        /// <summary>
        /// Creates a failed TryResult
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <returns></returns>
        public static new TryResult<T> Fail(string message)
        {
            var result = new TryResult<T>();
            result.Value = default;
            result.IsSuccess = false;
            result.ErrorMessage = message;
            return result;
        }
    }
}
