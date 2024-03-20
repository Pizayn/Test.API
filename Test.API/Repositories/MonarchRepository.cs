using Vienna.Test.API.Data;
using Vienna.Test.API.Entites;

namespace Vienna.Test.API.Repositories
{
    public class MonarchRepository : RepositoryBase<Monarch>, IMonarchRepository
    {
        public MonarchRepository(MonarchContext dbContext) : base(dbContext)
        {
        }
    }
}
