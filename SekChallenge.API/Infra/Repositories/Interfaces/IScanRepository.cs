using SekChallenge.API.Entities;

namespace SekChallenge.API.Infra.Repositories.Interfaces
{
    public interface IScanRepository : IRepository<Scan>
    {
        Task<List<Scan> >GetScanUnfishedAsync();
    }
}
