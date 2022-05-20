using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace DiplomaProject.Clients;

public class QaseApiAuthentication : IAuthenticator
{
    private readonly string _token;

    private string Token => _token;

    public QaseApiAuthentication(string token)
    {
        _token = token;
    }

    public ValueTask Authenticate(RestClient client, RestRequest request)
    {
        request.AddHeader("Token", Token);

        return ValueTask.CompletedTask;
    }
}
