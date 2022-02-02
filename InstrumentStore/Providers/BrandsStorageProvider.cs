using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstrumentStore.Models.DTO;

namespace InstrumentsStore.Providers
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
            var brands = await _db.Brands.GetAll();
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }

        public async Task<BrandDTO> GetById(Guid id)
        {
            var brand = await _db.Brands.GetById(id);
            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task<BrandDTO> Insert(BrandDTO model)
        {
            var brand = _mapper.Map<Brand>(model);
            await _db.Brands.Add(brand);
            await _db.Save();
            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task Replace(BrandDTO model)
        {
            var brand = _mapper.Map<Brand>(model);
            _db.Brands.Edit(brand);
            await _db.Save();
        }

        public async Task Delete(Guid id)
        {
            var brand = await _db.Brands.GetById(id);
            _db.Brands.Remove(brand);
            await _db.Save();
        }
    }
}
