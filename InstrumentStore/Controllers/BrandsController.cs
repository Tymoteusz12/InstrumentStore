﻿using InstrumentStore.Models.DTO;
using InstrumentStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InstrumentsStore.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandsService _brandService;

        public BrandsController(IBrandsService brandService)
        {
            _brandService = brandService ?? throw new ArgumentNullException();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return View(brands);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var brandDetails = await _brandService.GetBrandByIdAsync(id);
            if (brandDetails == null) return View("NotFound");
            return View(brandDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("BrandName, BrandDetails, Comment, LogoURL")] BrandDTO brand)
        {
            if (!ModelState.IsValid) return View(brand);

            await _brandService.InsertBrandAsync(brand);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brandDetails = await _brandService.GetBrandByIdAsync(id);
            if (brandDetails == null) return View("NotFound");
            return View(brandDetails);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id, BrandName, BrandDetails, Comment, LogoURL")] BrandDTO brand)
        {
            if (!ModelState.IsValid) return View(brand);

            _brandService.EditBrand(brand);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var brandDetails = await _brandService.GetBrandByIdAsync(id);
                return View(brandDetails);
            }
            catch (Exception _)
            {
                return View("NotFound");
            }
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                await _brandService.RemoveBrandAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception _)
            {
                return View("NotFound");
            }
        }
    }
}
