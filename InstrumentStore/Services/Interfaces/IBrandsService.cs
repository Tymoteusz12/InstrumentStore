using InstrumentStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IBrandsService
    {
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<BrandDTO> InsertBrandAsync(BrandDTO model);

        Task EditBrandAsync(BrandDTO model);

        Task RemoveBrandAsync(Guid id);
    }
}
