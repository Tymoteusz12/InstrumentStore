using DataAccessLayer.Models;
using InstrumentStore.Extensions;
using InstrumentStore.Models.DTO;
using InstrumentStore.Models.Static;
using InstrumentStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IOrdersService service, UserManager<ApplicationUser> userManager)
        {
            _ordersService = service ?? throw new ArgumentNullException(nameof(service));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.IsInRole(UserRoles.Admin))
                {
                    var allOrders = _ordersService.GetAllOrdersAsync();
                    return View(allOrders);
                }
                else if (User.IsInRole(UserRoles.User))
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                    return View(user.Orders);
                }
            }
            catch (Exception e)
            {
                return View("CustomError", e);
            }
            return RedirectToAction("AccessDenied", "Account");
        }


        public IActionResult Create(StoreDTO store)
        {
            var orderModel = new OrderDTO()
            {
                Price = store.FinalPrice,
                OrderDate = DateTime.Now,
                OrderedItems = store.StoreItems
            };
            if (!store.StoreItems.Any())
            {
                return RedirectToAction("Index", "Store");
            }
            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("OrderDate, OrderDetails, Price, Street, ApartamentNumber, BuildingNumber, PostalCode, PhoneNumber, Comment, OrderedItems")] OrderDTO order)
        {
            if (!ModelState.IsValid) return View(order);

            order.UserId = await _userManager.GetUserIdByNameAsync(User.Identity.Name);
            await _ordersService.MakeOrderAsync(order);

            return RedirectToAction(nameof(Summary));
        }

        [AllowAnonymous]
        public IActionResult Summary()
        {
            return View();
        }
    }
}
