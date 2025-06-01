using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;

namespace kAI_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Models.Userscontext _context;

        public UsersController(Models.Userscontext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/Users/Create")]
        public IActionResult ThemUser(string name, string password, string fullname, string email, int phone, string address) // Added 'email' parameter
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email)) // Validate 'email'
            {
                return BadRequest("Invalid input parameters.");
            }

            var user = new Models.Users
            {
                Username = name,
                Password = password,
                Fullname = fullname,
                Email = email,
                Phone = phone,
                Address = address
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User created successfully.");
        }
        [HttpPost]
        [Route("/Users/Update")]
        public IActionResult CapnhatUser(string name, string password, string fullname, string email, int phone, string address) // Added 'email' parameter
        {
            var user = new Models.Users
            {
                Username = name,
                Password = password,
                Fullname = fullname,
                Email = email,
                Phone = phone,
                Address = address
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User updated successfully.");
        }
        [HttpPost]
        [Route("/Users/Delete")]
        public IActionResult XoaUser(int id_user)
        {
            if (id_user <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            var user = _context.Users.Find(id_user);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("User deleted successfully.");
        }
    }
}
