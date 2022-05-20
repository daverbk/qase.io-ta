using System.Threading.Tasks;
using DiplomaProject.Models;

namespace DiplomaProject.Services.ApiServices;

public interface IDefectService
{
    Task<Response<Defect>> CreateNewDefect(Defect defect, string projectCode);

    Task<Response<Defect>> GetSpecificDefect(string defectId, string projectCode);

    Task<Response<GroupSelection<Defect>>> GetAllDefects(string projectCode);

    Task<Response<Defect>> UpdateDefect(Defect defect, string projectCode);

    Task<Response<Defect>> DeleteDefect(string defectId, string projectCode);
}
