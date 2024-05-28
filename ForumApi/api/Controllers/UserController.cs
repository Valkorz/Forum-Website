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
            var database = await _context.Users.ToListAsync();
            try{
                if(database.Any(x => x.Email == user.Email)){
                    throw new Exception($"Could not create: {user.Email} is already assigned to an existing account.");
                }
                user.Id = _context.Users.Count() + 1;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Content($"Created: {user.ToString()}");
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }     
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string email, string password){
            try {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null) {
                    return BadRequest("Invalid email or password.");
                }

                // Verify the password
                if (user.Password == password) {
                    return BadRequest("Invalid email or password.");
                }

                // Create a token or session here and send it back to the client

                return Ok("Login successful.");
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }    
        }

        [HttpGet("Exists")]
        public async Task<bool> HasUser([FromQuery]int id){
            var database = await _context.Users.ToListAsync();    
            return database.Any(x => x.Id == id);
        }

        [HttpGet("GetUserList")]
        public async Task<IEnumerable<User>> GetUserList(){ 
            return _context.Users;
        }    
    }
}