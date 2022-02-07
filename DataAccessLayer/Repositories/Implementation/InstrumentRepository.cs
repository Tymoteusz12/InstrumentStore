using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer.Repositories.Implementation
{
    public class InstrumentRepository : Repository<Instrument>, IInstrumentsRepository
    {
        public InstrumentRepository(ApplicationDbContext context) : base(context) 
        {
        }

    }
}
