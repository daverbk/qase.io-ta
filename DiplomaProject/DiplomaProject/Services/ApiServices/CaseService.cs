using System;
using System.Threading.Tasks;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using RestSharp;

namespace DiplomaProject.Services.ApiServices;

public class CaseService : IDisposable
{
    private readonly RestClientExtended _restClient;

    public CaseService(RestClientExtended restClient)
    {
        _restClient = restClient;
    }

    public async Task<Response<TestCase>> CreateNewTestCase(TestCase testCase, string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}", Method.Post)
            .AddUrlSegment("code", projectCode)
            .AddJsonBody(testCase);

        return await _restClient.ExecuteAsync<Response<TestCase>>(request);
    }

    public async Task<Response<TestCase>> GetSpecificTestCase(string testCaseId, string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}/{id}")
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", testCaseId);

        return await _restClient.ExecuteAsync<Response<TestCase>>(request);
    }

    public async Task<Response<GroupSelection<TestCase>>> GetAllTestCases(string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}")
            .AddUrlSegment("code", projectCode);

        return await _restClient.ExecuteAsync<Response<GroupSelection<TestCase>>>(request);
    }

    public async Task<Response<TestCase>> UpdateTestCase(TestCase testCase, string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}/{id}", Method.Patch)
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", testCase.Id)
            .AddBody(testCase);

        return await _restClient.ExecuteAsync<Response<TestCase>>(request);
    }

    public async Task<Response<TestCase>> DeleteTestCase(string testCaseId, string projectCode)
    {
        var request = new RestRequest("/v1/case/{code}/{id}", Method.Delete)
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", testCaseId);

        return await _restClient.ExecuteAsync<Response<TestCase>>(request);
    }

    public void Dispose()
    {
        _restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
