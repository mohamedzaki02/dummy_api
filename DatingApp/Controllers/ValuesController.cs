using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Data.DataContext _context;
        public ValuesController(Data.DataContext ctx)
        {
            _context = ctx;

        }
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            return Ok(await _context.Values.ToListAsync());
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> GetValues(int id)
        {
            return Ok(await _context.Values.FirstOrDefaultAsync(v => v.Id == id));
        }
    }
}