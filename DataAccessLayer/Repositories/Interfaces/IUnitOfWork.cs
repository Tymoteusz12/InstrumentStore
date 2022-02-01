using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBrandRepository Brands { get; }
        IInstrumentsRepository Instruments { get; }
        IOrderRepository Orders { get; }

        Task Save();
    }
}
