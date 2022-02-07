using DataAccessLayer.Models;
using InstrumentStore.Extensions;
using InstrumentStore.Models.DTO;
using InstrumentStore.Models.Static;
using InstrumentStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public StoreController(IStoreService service, UserManager<ApplicationUser> userManager)
        {
            _storeService = service ?? throw new ArgumentNullException(nameof(service));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userStoreItems = await _storeService.GetStoreAsync(user.StoreId);
            return View(userStoreItems);
        }

        [HttpPost]
        public async Task<IActionResult> InsertItemToStore(InstrumentDTO instrument)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _storeService.InsertInstrumentAsync(user.StoreId, instrument);
            return RedirectToAction("Index", "Instruments");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItemFromStore(int instrumentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                await _storeService.RemoveInstrumentFromStoreAsync(user.Store.Id, instrumentId);
                return RedirectToAction("Index");
            }
            catch(Exception _)
            {
                return RedirectToAction("Index");
            }
        }
    }
}