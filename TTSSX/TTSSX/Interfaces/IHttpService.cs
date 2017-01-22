using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSSX.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Posts value to URL and returns value.
        /// </summary>
        /// <typeparam name="TIn">Input type</typeparam>
        /// <typeparam name="TOut">Output type</typeparam>
        /// <param name="URL">URL</param>
        /// <param name="postValue">Value to post</param>
        /// <returns>Value got from response</returns>
        Task<TOut> Post<TIn, TOut>(string URL, TIn postValue);
        /// <summary>
        /// Posts value to URL without returning value.
        /// </summary>
        /// <typeparam name="TIn">Input type</typeparam>
        /// <param name="URL">URL</param>
        /// <param name="postValue">Value to post</param>
        Task Post<TIn>(string URL, TIn postValue);

        /// <summary>
        /// Gets the value from URL
        /// </summary>
        /// <typeparam name="TOut">Output type</typeparam>
        /// <param name="URL">URL</param>
        /// <returns>Value got from URL</returns>
        Task<TOut> Get<TOut>(string URL);

        /// <summary>
        /// Puts value to URL and returns response value
        /// </summary>
        /// <typeparam name="TIn">Input type</typeparam>
        /// <typeparam name="TOut">Output type</typeparam>
        /// <param name="URL">URL</param>
        /// <param name="putValue">Value to put</param>
        /// <returns>Value from response</returns>
        Task<TOut> Put<TIn, TOut>(string URL, TIn putValue);
        /// <summary>
        /// Puts value to URL without returning response
        /// </summary>
        /// <typeparam name="TIn">Input type</typeparam>
        /// <param name="URL">URL</param>
        /// <param name="putValue">value to put</param>
        Task Put<TIn>(string URL, TIn putValue);
    }
}
