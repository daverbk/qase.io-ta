using System.Threading.Tasks;
using DiplomaProject.Models;

namespace DiplomaProject.Services.ApiServices;

public interface IProjectService
{
    Task<Response<Project>> CreateNewProject(Project project);

    Task<Response<Project>> GetProjectByCode(string projectCode);

    Task<Response<Project>> DeleteProjectByCode(string projectCode);
}
