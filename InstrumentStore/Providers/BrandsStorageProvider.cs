using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstrumentStore.Models.DTO;

namespace InstrumentStore.Providers
{
    public class BrandsStorageProvider : IBrandsStorageProvider
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        public BrandsStorageProvider(
            IUnitOfWork db,
            IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IEnumerable<BrandDTO>> GetAll()
        {
            var brands = await _db.Brands.GetAllAsync(x => x.Instruments);
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }

        public async Task<BrandDTO> GetById(int id)
        {
            var brand = await _db.Brands.GetByIdAsync(id, x => x.Instruments);
            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task<BrandDTO> Insert(BrandDTO model)
        {
            var brand = _mapper.Map<Brand>(model);
            await _db.Brands.Add(brand);
            return _mapper.Map<BrandDTO>(brand);
        }

        public void Replace(BrandDTO model)
        {
            var brand = _mapper.Map<Brand>(model);
            _db.Brands.Edit(brand);
        }

        public async Task Delete(int id)
        {
            var brand = await _db.Brands.GetByIdAsync(id);
            _db.Brands.Remove(brand);
        }
    }
}
