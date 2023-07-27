using Microsoft.EntityFrameworkCore;
using SekChallenge.API.Entities;
using SekChallenge.API.Infra.Repositories.Interfaces;

namespace SekChallenge.API.Infra.Repositories
{
    public class ScanRepository : RepositoryBase<Scan>, IScanRepository
    {
        public ScanRepository(SekContext sekContext) : base(sekContext)
        {
        }

        public async Task<List<Scan>> GetScanUnfishedAsync()
        {
            return await Set.Where(x => x.Finished == false).ToListAsync();
        }
    }
}
