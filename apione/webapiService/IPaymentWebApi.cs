using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace webapiService
{
    //[HttpHost("http://client")]
    public interface IPaymentWebApi:IHttpApi
    {
        [HttpGet("api/values/{id}")]
        Task<string> GetClientName(int id);
        [HttpGet("api/values")]
        Task<string> GetClientName();
    }
}