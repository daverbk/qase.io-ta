using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace DiplomaProject.Clients;

public class QaseApiAuthentication : AuthenticatorBase
{
    private readonly string _token;
    
    public QaseApiAuthentication(string token) : base("")
    {
        _token = token;
    }

    protected async override ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        return new HeaderParameter("Token", _token);
    }
}
