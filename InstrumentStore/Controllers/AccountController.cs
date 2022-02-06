using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Models.DTO;
using InstrumentStore.Models.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Users()
        {
            var users = _context.GetAllUsers().ToList();
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
                        return RedirectToAction("Index", "Books");
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
                UserName = userRegister.EmailAddress
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
            return RedirectToAction("Index", "Books");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
