using System;
using System.Threading.Tasks;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using RestSharp;

namespace DiplomaProject.Services.ApiServices;

public class DefectService : IDefectService, IDisposable
{
    private readonly RestClientExtended _restClient;

    public DefectService(RestClientExtended restClient)
    {
        _restClient = restClient;
    }

    public async Task<Response<Defect>> CreateNewDefect(Defect defect, string projectCode)
    {
        var request = new RestRequest("/v1/defect/{code}", Method.Post)
            .AddUrlSegment("code", projectCode)
            .AddJsonBody(defect);

        return await _restClient.ExecuteAsync<Response<Defect>>(request);
    }

    public async Task<Response<Defect>> GetSpecificDefect(string defectId, string projectCode)
    {
        var request = new RestRequest("/v1/defect/{code}/{id}")
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", defectId);

        return await _restClient.ExecuteAsync<Response<Defect>>(request);
    }

    public async Task<Response<GroupSelection<Defect>>> GetAllDefects(string projectCode)
    {
        var request = new RestRequest("/v1/defect/{code}")
            .AddUrlSegment("code", projectCode);

        return await _restClient.ExecuteAsync<Response<GroupSelection<Defect>>>(request);
    }

    public async Task<Response<Defect>> UpdateDefect(Defect defect, string projectCode)
    {
        var request = new RestRequest("/v1/defect/{code}/{id}", Method.Patch)
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", defect.Id)
            .AddBody(defect);

        return await _restClient.ExecuteAsync<Response<Defect>>(request);
    }

    public async Task<Response<Defect>> DeleteDefect(string defectId, string projectCode)
    {
        var request = new RestRequest("/v1/defect/{code}/{id}", Method.Delete)
            .AddUrlSegment("code", projectCode)
            .AddUrlSegment("id", defectId);

        return await _restClient.ExecuteAsync<Response<Defect>>(request);
    }

    public void Dispose()
    {
        _restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
