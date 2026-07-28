using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.Json;
using App.Models.Pokemon;
using App.Services.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace App.Services.API;

public sealed class PokeAPI(HttpClient _client)
{
    private readonly HttpClient client = _client;
    private readonly Utils.Logger<PokeAPI> logger = new();

    private class Wrapper<T>
    {
        // The data property holding the generic type
        public T? Data { get; set; } = default!;

        public List<GraphQLErrorResponse>? Errors { get; set; } = [];

        // Constructor for easy instantiation
        public Wrapper(T data)
        {
            Data = data;
        }

        // Parameterless constructor for serialization engines (System.Text.Json, Newtonsoft.Json)
        public Wrapper()
        {
            Data = default!;
        }
    }

    private class GraphQLErrorResponse
    {
        public string Message { get; set; } = string.Empty;
    }

    private async Task<ReturnType<T>> FetchPokeGraphQl<T>(
        string Query,
        HttpOptions.ParamDictionary Variables,
        CancellationToken CancelToken = default
    )
    {
        var requestBody = new { query = Query, variables = Variables };

        try
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "",
                requestBody,
                CancelToken
            );
            response.EnsureSuccessStatusCode();
            Wrapper<T> body =
                await response.Content.ReadFromJsonAsync<Wrapper<T>>(CancelToken)
                ?? throw new InvalidOperationException("Body returned null");
            if (body.Data is not null)
            {
                return ReturnType<T>.Success(body.Data);
            }
            if (body.Errors is { Count: > 0 })
            {
                string errorMessages = string.Join(
                    ", ",
                    body.Errors.Select(error => error.Message)
                );

                return ReturnType<T>.Failure(errorMessages, ErrorStatusCode.BadRequest);
            }
            return ReturnType<T>.Failure(
                "GraphQL returned neither data nor errors.",
                ErrorStatusCode.InternalServerError
            );
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            logger.Log(LogLevel.Error, $"The requested resource was not found");
            return ReturnType<T>.Failure(
                "The requested resource was not found",
                ErrorStatusCode.NotFound
            );
        }
        catch (HttpRequestException e)
        {
            logger.Log(LogLevel.Error, $"{e.Message}");
            return ReturnType<T>.Failure(e.Message, ErrorStatusCode.BadRequest);
        }
        catch (TaskCanceledException)
        {
            logger.Log(LogLevel.Error, "The request timed out.");
            return ReturnType<T>.Failure("The request timed out", ErrorStatusCode.GatewayTimeout);
        }
        catch (InvalidOperationException ex)
        {
            logger.Log(LogLevel.Error, ex.Message);
            return ReturnType<T>.Failure(ex.Message, ErrorStatusCode.InternalServerError);
        }
    }
}
