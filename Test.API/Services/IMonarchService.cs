using Vienna.Test.API.Entites;

namespace Vienna.Test.API.Services
{
    public interface IMonarchService
    {
        Task<IEnumerable<Monarch>> GetAll();
        Task<IEnumerable<Monarch>> GetAllFromDb();

    }
}
