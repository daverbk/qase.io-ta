using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using DiplomaProject.Configuration;
using NLog;
using RestSharp;

namespace DiplomaProject.Clients;

public class RestClientExtended
{
    private readonly RestClient _client;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public static RestResponse LastCallResponse { get; private set; } = null!;

    public RestClientExtended()
    {
        var options = new RestClientOptions(Configurator.AppSettings.BaseUrl ??
                                            throw new InvalidOperationException(
                                                "Base url can't be null. Check appsettings.json before the next restart."));

        _client = new RestClient(options);
        _client.Authenticator = new QaseApiAuthentication(Configurator.Admin.Token);
    }

    public async Task<T> ExecuteAsync<T>(RestRequest request)
    {
        LogRequest(request);
        
        var response = await _client.ExecuteAsync<T>(request);
        
        LogResponse(response);
        UpdateLastCallResponse(response);

        return response.Data ??
               throw new SerializationException(
                   "Response deserialization error. Debug with breakpoints on model's properties for more information.",
                   response.ErrorException);
    }

    public async Task<RestResponse> ExecuteAsync(RestRequest request)
    {
        LogRequest(request);
        
        var response = await _client.ExecuteAsync(request);
        LogResponse(response);

        return response;
    }

    private void LogRequest(RestRequest request)
    {
        _logger.Debug($"{request.Method} request to: {request.Resource}");

        var body = request.Parameters
            .FirstOrDefault(parameter => parameter.Type == ParameterType.RequestBody)?.Value;

        if (body != null)
        {
            _logger.Debug($"body : {body}");
        }
    }

    private void LogResponse(RestResponse response)
    {
        if (response.ErrorException != null)
        {
            _logger.Error(
                $"Error retrieving response. Check inner details for more info. Error message: {response.ErrorException.Message}");
        }

        _logger.Debug($"Request responded with status code : {response.StatusCode}");

        if (!string.IsNullOrEmpty(response.Content))
        {
            _logger.Debug(response.Content);
        }
    }

    private void UpdateLastCallResponse(RestResponse lastCallResponse)
    {
        LastCallResponse = lastCallResponse;
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
