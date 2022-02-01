using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.Implementation
{
    public class InstrumentsRepository : Repository<Instrument>, IInstrumentsRepository
    {
        public InstrumentsRepository(ApplicationDbContext context) : base(context) { }

    }
}
