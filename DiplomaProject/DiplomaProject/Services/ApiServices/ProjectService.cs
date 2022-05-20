using System;
using System.Threading.Tasks;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using RestSharp;

namespace DiplomaProject.Services.ApiServices;

public class ProjectService : IProjectService, IDisposable
{
    private readonly RestClientExtended _restClient;

    public ProjectService(RestClientExtended restClient)
    {
        _restClient = restClient;
    }

    public Task<Response<Project>> CreateNewProject(Project project)
    {
        var request = new RestRequest("/v1/project", Method.Post)
            .AddJsonBody(project);

        return _restClient.ExecuteAsync<Response<Project>>(request);
    }

    public Task<Response<Project>> GetProjectByCode(string projectCode)
    {
        var request = new RestRequest("/v1/project/{code}")
            .AddUrlSegment("code", projectCode);

        return _restClient.ExecuteAsync<Response<Project>>(request);
    }

    public Task<Response<Project>> DeleteProjectByCode(string projectCode)
    {
        var request = new RestRequest("/v1/project/{code}", Method.Delete)
            .AddUrlSegment("code", projectCode);

        return _restClient.ExecuteAsync<Response<Project>>(request);
    }

    public void Dispose()
    {
        _restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
