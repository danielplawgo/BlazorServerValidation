using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorServerValidation.Shared;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorServerValidation.Client
{
    public class HttpService : IHttpService
    {
        private readonly string _baseAddress;

        public HttpService(IWebAssemblyHostEnvironment hostEnvironment)
        {
            _baseAddress = hostEnvironment.BaseAddress;
        }

        public async Task<Result<T>> Get<T>(string path, CancellationToken cancellationToken)
        {
            try
            {
                return await CreateRequest(path)
                    .GetJsonAsync<Result<T>>(cancellationToken);
            }
            catch (FlurlHttpException ex)
            {
                return await HandleException<T>(ex);
            }
            catch (Exception ex)
            {
                return await HandleException<T>(ex);
            }
        }

        public async Task<Result<T>> Post<T>(string path, object data, CancellationToken cancellationToken)
        {
            try
            {
                return await CreateRequest(path)
                    .PostJsonAsync(data, cancellationToken)
                    .ReceiveJson<Result<T>>();
            }
            catch (FlurlHttpException ex)
            {
                return await HandleException<T>(ex);
            }
            catch (Exception ex)
            {
                return await HandleException<T>(ex);
            }
        }

        public async Task<Result<T>> Put<T>(string path, object data, CancellationToken cancellationToken)
        {
            try
            {
                return await CreateRequest(path)
                    .PutJsonAsync(data, cancellationToken)
                    .ReceiveJson<Result<T>>();
            }
            catch (FlurlHttpException ex)
            {
                return await HandleException<T>(ex);
            }
            catch (Exception ex)
            {
                return await HandleException<T>(ex);
            }
        }

        public async Task<Result> Delete<T>(string path, CancellationToken cancellationToken)
        {
            try
            {
                return await CreateRequest(path)
                    .DeleteAsync(cancellationToken)
                    .ReceiveJson<Result>();
            }
            catch (FlurlHttpException ex)
            {
                return await HandleException<T>(ex);
            }
            catch (Exception ex)
            {
                return await HandleException<T>(ex);
            }
        }

        private IFlurlRequest CreateRequest(string path)
        {
            var url = _baseAddress
                .AppendPathSegment(path);

            return new FlurlRequest(url);
        }

        private static async Task<Result<T>> HandleException<T>(FlurlHttpException ex)
        {
            var result = await ex.GetResponseJsonAsync<Result>();

            if (result != null && result.Success == false)
            {
                return Result.Failure<T>(result.Errors);
            }

            return await HandleException<T>(ex as Exception);
        }

        public static Task<Result<T>> HandleException<T>(Exception ex)
        {
            return Task.FromResult(Result.Failure<T>("Przepraszamy nastąpił błąd..."));
        }
    }

    public interface IHttpService
    {
        Task<Result<T>> Get<T>(string path, CancellationToken cancellationToken);
        Task<Result<T>> Post<T>(string path, object data, CancellationToken cancellationToken);
        Task<Result<T>> Put<T>(string path, object data, CancellationToken cancellationToken);
        Task<Result> Delete<T>(string path, CancellationToken cancellationToken);
    }
}
