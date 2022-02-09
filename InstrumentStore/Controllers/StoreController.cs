using AutoMapper;
using DataAccessLayer.Models;
using InstrumentsShop.Models.DTO;
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
        private readonly IMapper _mapper;

        public StoreController(IStoreService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _storeService = service ?? throw new ArgumentNullException(nameof(service));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            await _storeService.InsertInstrumentAsync(user.StoreId, _mapper.Map<StoreItemDTO>(instrument));
            return RedirectToAction("Index", "Instruments");
        }

        public async Task<IActionResult> DeleteItemFromStore(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                await _storeService.RemoveInstrumentFromStoreAsync(user.StoreId, id);
                return RedirectToAction("Index");
            }
            catch(Exception _)
            {
                return RedirectToAction("Index");
            }
        }
    }
}