using System.Threading.Tasks;

namespace testCheckHealth
{
    public interface IClientService
    {
        Task<string> GetClientName(int clientId);
    }
}