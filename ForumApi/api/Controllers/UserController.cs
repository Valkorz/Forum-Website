using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;

namespace api.Controllers
{
    //Create a task item class to be used as a base for the SQL table
    public class User
    {
        public int Id {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
    }

    /* 
        DbContext class for representing a database session for querying and saving instances of each entity.
    */
    public class UserContext : DbContext
    {
        //Pass context options to base constructor
        public UserContext(DbContextOptions<UserContext> opts) : base(opts) {}
        
        public DbSet<User> Users {get; set;}
    }

    
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context; //Reference the database context the controller is reffering to

        public UserController(UserContext context){
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(User user){
            user.Id = _context.Users.Count() + 1;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Content($"Created: {user.ToString()}");
        }

        [HttpGet("Exists")]
        public async Task<bool> HasUser([FromQuery]int id){
            var database = await _context.Users.ToListAsync();    
            return database.Any(x => x.Id == id);
        }

        [HttpGet("GetUserList")]
        public async Task<List<User>> GetUserList(){
            return await _context.Users.ToListAsync();
        }

        
    }
}