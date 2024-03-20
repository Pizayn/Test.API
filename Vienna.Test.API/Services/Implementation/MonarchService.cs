using Vienna.Test.API.Entites;
using Vienna.Test.API.Repositories;

namespace Vienna.Test.API.Services.Implementation
{
    public class MonarchService : IMonarchService
    {
        private readonly IGithubService _githubService;
        private IMonarchRepository _monarchRepository;
        public MonarchService(IGithubService githubService, IMonarchRepository monarchRepository) 
        {
            _githubService = githubService;
            _monarchRepository = monarchRepository;
        }
        public async Task<IEnumerable<Monarch>> GetAll()
        {
            var list = await _githubService.GetAllStats();
            return list;
        }

        public async Task<IEnumerable<Monarch>> GetAllFromDb()
        {
            return await _monarchRepository.GetAllAsync();
        }
    }
}
