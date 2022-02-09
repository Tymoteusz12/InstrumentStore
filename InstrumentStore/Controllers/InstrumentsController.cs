﻿using InstrumentStore.Models.DTO;
using InstrumentStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentsStore.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly IInstrumentsService _instrumentService;
        private readonly IBrandsService _brandsService;
        public InstrumentsController(IInstrumentsService instrumentService, IBrandsService brandsService)
        {
            _instrumentService = instrumentService ?? throw new ArgumentNullException(nameof(instrumentService));
            _brandsService = brandsService ?? throw new ArgumentNullException(nameof(brandsService));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var instruments = await _instrumentService.GetAllInstrumentsAsync();
            return View(instruments);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var instrumentDetails = await _instrumentService.GetByIdAsync(id);
            if (instrumentDetails == null) return View("NotFound");
            return View(instrumentDetails);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchString)
        {
            var allInstruments = await _instrumentService.GetAllInstrumentsAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allInstruments
                    .Where(instrument => instrument.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allInstruments);
        }

        public async Task<IActionResult> Create()
        {
            var brands = await _brandsService.GetAllBrandsAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Price, ImageURL, Description, InstrumentTypeValue, BrandId")] InstrumentDTO instrument)
        {
            if (!ModelState.IsValid)
            {
                var brands = await _brandsService.GetAllBrandsAsync();
                ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
                return View(instrument);
            }
            
            await _instrumentService.InsertInstrumentAsync(instrument);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var instrumentDetails = await _instrumentService.GetByIdAsync(id);
            if (instrumentDetails == null) return View("NotFound");
            var brands = await _brandsService.GetAllBrandsAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
            return View(instrumentDetails);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Name, Price, ImageURL, Description, InstrumentTypeValue, BrandId, Id")] InstrumentDTO instrument)
        {
            if (!ModelState.IsValid) return View(instrument);

            _instrumentService.EditInstrument(instrument);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteInstrument(int id)
        {
            try
            {
                await _instrumentService.RemoveInstrumentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception _)
            {
                return View("NotFound");
            }
        }
    }
}
