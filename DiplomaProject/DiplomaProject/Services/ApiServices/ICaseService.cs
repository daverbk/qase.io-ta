using System.Threading.Tasks;
using DiplomaProject.Models;

namespace DiplomaProject.Services.ApiServices;

public interface ICaseService
{
    Task<Response<TestCase>> CreateNewTestCase(TestCase testCase, string projectCode);

    Task<Response<TestCase>> GetSpecificTestCase(string testCaseId, string projectCode);
}
