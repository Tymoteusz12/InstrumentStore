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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _ordersService = service ?? throw new ArgumentNullException(nameof(service));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                    var userId = await _userManager.GetUserIdByNameAsync(User.Identity.Name);
                    var orders = await _ordersService.GetUserOrders(userId);
                    return View(orders);
                }
            }
            catch (Exception e)
            {
                return View("CustomError", e);
            }
            return RedirectToAction("AccessDenied", "Account");
        }


        public IActionResult CreateOrderView(string store)
        {
            var storeDto = JsonConvert.DeserializeObject<StoreDTO>(store);
            var orderModel = new OrderDTO()
            {
                UserId = storeDto.UserId,
                Price = storeDto.FinalPrice,
                OrderDate = DateTime.Now,
                OrderedItems = _mapper.Map<IEnumerable<OrderItemDTO>>(storeDto.StoreItems).ToList()
            };
            if (!storeDto.StoreItems.Any())
            {
                return RedirectToAction("Index", "Store");
            }
            return View(orderModel);
        }

        public async Task<IActionResult> Create(string orderString)
        {
            var order = JsonConvert.DeserializeObject<OrderDTO>(orderString, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
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
