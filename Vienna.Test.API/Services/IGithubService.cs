using Vienna.Test.API.Entites;

namespace Vienna.Test.API.Services
{
    public interface IGithubService
    {
        Task<IEnumerable<Monarch>> GetAllStats();
    }
}
