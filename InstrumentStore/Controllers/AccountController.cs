using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Models.DTO;
using InstrumentStore.Models.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IUnitOfWork context,
            IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Users()
        {
            var users = _mapper.Map<IEnumerable<UserDTO>>(_context.GetAllUsers().ToList());
            return View(users);
        }

        public IActionResult Login() => View(new UserCredentials());

        [HttpPost]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            if (!ModelState.IsValid) return View(credentials);

            var user = await _userManager.FindByEmailAsync(credentials.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, credentials.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, credentials.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Instruments");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(credentials);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(credentials);
        }


        public IActionResult Register() => View(new UserRegister());

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var user = await _userManager.FindByEmailAsync(userRegister.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(userRegister);
            }

            var newUser = new ApplicationUser()
            {
                FullName = userRegister.FullName,
                Email = userRegister.EmailAddress,
                UserName = userRegister.EmailAddress,
                Orders = new List<Order>(),
                Store = new Store()
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, userRegister.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Instruments");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
