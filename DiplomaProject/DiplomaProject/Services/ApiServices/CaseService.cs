using System;
using System.Threading.Tasks;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using RestSharp;

namespace DiplomaProject.Services.ApiServices;

public class CaseService : ICaseService, IDisposable
{
    private readonly RestClientExtended _restClient;

    public CaseService(RestClientExtended restClient)
    {
        _restClient = restClient;
    }

    public Task<Response<TestCase>> CreateNewTestCase(TestCase testCase, string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}", Method.Post)
            .AddUrlSegment("code", projectCode)
            .AddJsonBody(testCase);

        return _restClient.ExecuteAsync<Response<TestCase>>(request);
    }

    public Task<Response<TestCase>> GetSpecificTestCase(string testCaseId, string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}/{id}")
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", testCaseId);

        return _restClient.ExecuteAsync<Response<TestCase>>(request);
    }

    public void Dispose()
    {
        _restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
