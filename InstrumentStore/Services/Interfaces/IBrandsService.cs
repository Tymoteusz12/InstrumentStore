using InstrumentStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IBrandsService
    {
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<BrandDTO> GetBrandByIdAsync(int id);
        Task<BrandDTO> InsertBrandAsync(BrandDTO model);

        void EditBrand(BrandDTO model);

        Task RemoveBrandAsync(int id);
    }
}
