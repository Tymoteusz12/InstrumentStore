using InstrumentStore.Models.DTO;
using InstrumentStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentsStore.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly IInstrumentsService _instrumentService;

        public InstrumentController(IInstrumentsService instrumentService)
        {
            _instrumentService = instrumentService ?? throw new ArgumentNullException(nameof(instrumentService));
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Price, ImageURL, Description, InstrumentTypeValue, BrandId")] InstrumentDTO instrument)
        {
            if (!ModelState.IsValid) return View(instrument);

            await _instrumentService.InsertInstrumentAsync(instrument);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var authorDetails = await _instrumentService.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Name, Price, ImageURL, Description, InstrumentTypeValue, BrandId")] InstrumentDTO instrument)
        {
            if (!ModelState.IsValid) return View(instrument);

            await _instrumentService.EditInstrumentAsync(instrument);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var authorDetails = await _instrumentService.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        [HttpPost]
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
