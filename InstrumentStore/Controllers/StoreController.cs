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
    [Authorize(Roles = UserRoles.Admin)]
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
            var userId = await _userManager.GetUserIdByNameAsync(User.Identity.Name);
            var userStoreItems = _storeService.GetUserStoreAsync(userId);
            return View(userStoreItems);
        }

        [HttpPost]
        public async Task<IActionResult> InsertItemToStore(InstrumentDTO instrument)
        {
            var userId = await _userManager.GetUserIdByNameAsync(User.Identity.Name);
            await _storeService.InsertInstrumentAsync(userId, instrument);
            return RedirectToAction("Index", "Instruments");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItemFromStore(int instrumentId)
        {
            var userId = await _userManager.GetUserIdByNameAsync(User.Identity.Name);
            try
            {
                await _storeService.RemoveInstrumentFromStoreAsync(userId, instrumentId);
                return RedirectToAction("Index");
            }
            catch(Exception _)
            {
                return RedirectToAction("Index");
            }
        }
    }
}