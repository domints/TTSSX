using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using TTSSX.Interfaces;
using Square.OkHttp3;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(TTSSX.Droid.Implementation.HttpService))]
namespace TTSSX.Droid.Implementation
{
    class HttpService : IHttpService
    {
        OkHttpClient _httpClient;
        public HttpService()
        {
            _httpClient = new OkHttpClient();
        }

        public async Task<TOut> Get<TOut>(string URL)
        {
            Request request = new Request.Builder()
                .Url(URL)
                .BuildHeaders()
                .Build();
            return GetResponse<TOut>(await Execute(request));            
        }

        public async Task Post<TIn>(string URL, TIn postValue)
        {
            Request request = new Request.Builder()
                .Url(URL)
                .Post(GetBody(postValue))
                .BuildHeaders()
                .Build();
            await Execute(request);
        }

        public async Task<TOut> Post<TIn, TOut>(string URL, TIn postValue)
        {
            Request request = new Request.Builder()
                .Url(URL)
                .Post(GetBody(postValue))
                .BuildHeaders()
                .Build();
            return GetResponse<TOut>(await Execute(request));
        }

        public async Task Put<TIn>(string URL, TIn putValue)
        {
            Request request = new Request.Builder()
                 .Url(URL)
                 .Put(GetBody(putValue))
                 .BuildHeaders()
                 .Build();
            await Execute(request);
        }

        public async Task<TOut> Put<TIn, TOut>(string URL, TIn putValue)
        {
            Request request = new Request.Builder()
                .Url(URL)
                .Put(GetBody(putValue))
                .BuildHeaders()
                .Build();
            return GetResponse<TOut>(await Execute(request));
        }

        private TOut GetResponse<TOut>(Response response)
        {
            using (StreamReader sr = new StreamReader(response.Body().ByteStream()))
            using (JsonTextReader jtr = new JsonTextReader(sr))
            {
                JsonSerializer ser = new JsonSerializer();
                return ser.Deserialize<TOut>(jtr);
            }
        }

        private RequestBody GetBody<TIn>(TIn value)
        {
            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            using (JsonTextWriter jtw = new JsonTextWriter(sw))
            {
                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(jtw, value);
                jtw.Flush();
                return RequestBody.Create(
                    MediaType.Parse("application/json; charset=utf-8"),
                    ms.GetBuffer(), 0, (int)ms.Length);
            }
        }

        private async Task<Response> Execute(Request request)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();        
                Response resp = await _httpClient.NewCall(request).ExecuteAsync();
                sw.Stop();
                Console.WriteLine("Response time: " + sw.ElapsedMilliseconds);
                return resp;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                throw;
            }            
        }        
    }

    public static class HttpExtensions
    {
        public static Request.Builder BuildHeaders(this Request.Builder builder)
        {
            return builder.Header("User-Agent", "TTSSX-Android, okhttp3");
        }
    }
}