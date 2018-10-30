using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using to_do_app.Models;

namespace to_do_app.Controllers
{
    [Route("api/[controller]")]
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("{role}")]
        public async Task<IActionResult> Get(string role) //temporary for adding roles to users
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            AppUser user = await _userManager.FindByEmailAsync("ykravtsov1@gmail.com");

            if (!User.IsInRole("Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Json(await _userManager.GetRolesAsync(user));
        }

        // POST api/appUser
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                {
                    ModelState.TryAddModelError(e.Code, e.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("Account created");
        }
    }
}
