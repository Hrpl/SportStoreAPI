using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportStore.Models;

namespace API.Controllers
{
    public class UserController
    {
        [Route("api/")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly ApplicationContext _context;

            public UsersController(ApplicationContext context)
            {
                _context = context;
            }


            // GET: api/Users
            [HttpGet]
            public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            {
                return await _context.User.ToListAsync();
            }

            // GET: api/Users/login/password
            //[HttpGet("{login}/{password}")]
            //public async Task<ActionResult<List<User>>> GetUser(string login, string password)
            //{
            //    var user = _context.User.Include(u => u.RoleNavigation).ToList();
            // user.FirstOrDefault(u => u.Login == login && u.Password == password);

            //  if (user == null)
            // {
            //     return NotFound();
            //}

            //return user;
        }
    }
}
