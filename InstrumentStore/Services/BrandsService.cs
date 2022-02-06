using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using InstrumentStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services
{
    public class BrandsService : IBrandsService
    {
        private readonly IBrandsStorageProvider _brandsProvider;
        public BrandsService(IBrandsStorageProvider brandsProvider)
        {
            _brandsProvider = _brandsProvider ?? throw new ArgumentNullException(nameof(brandsProvider));
        }
        public void EditBrand(BrandDTO model)
        {
            _brandsProvider.Replace(model);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            return await _brandsProvider.GetAll();
        }

        public async Task<BrandDTO> GetBrandByIdAsync(int id)
        {
            var brand = await _brandsProvider.GetById(id);

            if (brand == null)
            {
                throw new ArgumentNullException("Invalid brand id");
            }

            return brand;
        }

        public async Task<BrandDTO> InsertBrandAsync(BrandDTO model)
        {
            return await _brandsProvider.Insert(model);
        }

        public async Task RemoveBrandAsync(int id)
        {
            await GetBrandByIdAsync(id);
            await _brandsProvider.Delete(id);
        }
    }
}
