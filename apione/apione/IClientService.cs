using System.Threading.Tasks;

namespace apione
{
    public interface IClientService
    {
        Task<string> GetClientName(int clientId);
    }
}